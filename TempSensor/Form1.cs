using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TempSensor
{
    public partial class Form1 : Form
    {

        //통신
        private TcpClient? client;
        private NetworkStream? stream;
        private CancellationTokenSource? cts;
        private String ip = "192.168.0.172";
        private int port = 502;
        private String myIp = "192.168.0.80";
        private readonly byte[] ADU = new byte[]
        {
            0, 1, 0, 0, 0, 6, 1, 4, 0, 48, 0, 2
        };
        private bool kakaoLoggedIn = false;
        private bool alterSent = false;

        //DB
        String SqlString = @"Server=localhost;database=TempSensorDB;uid=sa;pwd=std001";
        SqlConnection? conn;

        private float criticalTemperature = 50.0f;

        private KakaoService kakao = new KakaoService();
        private bool graphInitialized = false;

        public Form1()
        {
            InitializeComponent();
            lb_temp.Text = "-- ℃";
        }

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            if (cts != null)
                return;

            btn_connect.Enabled = false;
            lb_temp.Text = "연결 중...";
            cts = new CancellationTokenSource();

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(ip, port, cts.Token);
                stream = client.GetStream();
                btn_connect.Text = "연결됨";

                await ReadTemperatureAsync(cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show("연결이 종료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Disconnect();
                if (!IsDisposed && !Disposing)
                {
                    btn_connect.Enabled = true;
                    btn_connect.Text = "연결";
                    lb_temp.Text = "-- ℃";
                }

            }
        }

        private async Task ReadTemperatureAsync(CancellationToken token)
        {
            byte[] response = new byte[13];

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await stream!.WriteAsync(ADU, token);
                    await stream.ReadExactlyAsync(response, 0, 13, token);

                    float temp = ParseTemp(response);
                    lb_temp.Text = temp.ToString("F1") + " ℃";

                    await Save_Temp(temp);
                    Show_Temp();

                    await CheckTemp(temp);

                    await Task.Delay(2000, token);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private float ParseTemp(byte[] response)
        {
            byte[] data = new byte[4];

            response.AsSpan(9, 4).CopyTo(data);
            Array.Reverse(data);

            return BitConverter.ToSingle(data, 0);
        }
        private void Disconnect()
        {
            cts?.Cancel();
            stream?.Dispose();
            client?.Dispose();
            cts?.Dispose();

            stream = null;
            client = null;
            cts = null;

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private async Task Save_Temp(float temp)
        {
            DateTime now = DateTime.Now;
            conn = new SqlConnection(SqlString);

            try
            {
                await conn.OpenAsync();

                SqlCommand com = new SqlCommand("temp_insert", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@MeasurementDate", SqlDbType.Date).Value = now.Date;
                com.Parameters.Add("@MeasurementTime", SqlDbType.Time).Value = now.TimeOfDay;
                com.Parameters.Add("@Temp", SqlDbType.Float).Value = (double)temp;

                await com.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn?.Dispose();
            }
        }

        private void Show_Temp()
        {
            dataGridView1.Rows.Clear();

            List<DateTime> times = new List<DateTime>();
            List<double> temperatures = new List<double>();

            try
            {
                conn = new SqlConnection(SqlString);
                conn.Open();

                SqlCommand com = new SqlCommand("temp_select", conn);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    DateTime date = dr.GetDateTime(0);
                    TimeSpan time = dr.GetTimeSpan(1);
                    double temp = dr.GetDouble(2);

                    times.Add(date.Date + time);
                    temperatures.Add(temp);

                    int row = dataGridView1.Rows.Add(
                        date.ToString("yyyy-MM-dd"),
                        time.ToString(@"hh\:mm\:ss"),
                        temp.ToString("F1")
                        );

                    if(temp >= criticalTemperature)
                    {
                        dataGridView1.Rows[row].Cells[2].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[row].Cells[2].Style.SelectionForeColor = Color.Red;
                    }
                }
                dr.Close();
                times.Reverse();
                temperatures.Reverse();

                var limits = formsPlot1.Plot.Axes.GetLimits();

                formsPlot1.Plot.Clear();

                if (times.Count > 0)
                {
                    formsPlot1.Plot.Add.Scatter(
                        times.ToArray(),
                        temperatures.ToArray());
                }

                formsPlot1.Plot.Axes.DateTimeTicksBottom();
                formsPlot1.Plot.XLabel("Time");
                formsPlot1.Plot.YLabel("Temperature (°C)");

                formsPlot1.Plot.Axes.Bottom.TickLabelStyle.FontName = "Malgun Gothic";

                formsPlot1.Plot.Axes.Left.TickGenerator =
                    new ScottPlot.TickGenerators.NumericFixedInterval(10);

                if (!graphInitialized && times.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    double currentTemp = temperatures[temperatures.Count - 1];

                    formsPlot1.Plot.Axes.SetLimits(
                        now.AddMinutes(-30).ToOADate(),
                        now.AddMinutes(30).ToOADate(),
                        0,
                        150
                    );

                    graphInitialized = true;
                }
                else if (graphInitialized)
                {
                    formsPlot1.Plot.Axes.SetLimits(limits);
                }

                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn?.Dispose();
            }
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            btn_login.Enabled = false;

            try
            {
                await kakao.LoginAsync();
                kakaoLoggedIn = true;
                btn_login.Text = "로그인 완료";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn_login.Text = "카카오 로그인";
                btn_login.Enabled = true;
            }
        }

        private async Task CheckTemp(float temp)
        {
            if (temp <= criticalTemperature)
            {
                alterSent = false;
                return;
            }

            try
            {
                await kakao.SendAsync("임계온도를 초과했습니다.\n"
                    + "현재온도 : " + temp.ToString("F1") + "℃\n"
                    + "임계온도 : " + criticalTemperature.ToString("F1")+ "℃");

                alterSent = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
    }
}
