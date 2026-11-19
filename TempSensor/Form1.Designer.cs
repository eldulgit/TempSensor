namespace TempSensor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btn_login = new Button();
            lb_temp = new Label();
            label2 = new Label();
            btn_exit = new Button();
            btn_connect = new Button();
            label1 = new Label();
            tabPage2 = new TabPage();
            dataGridView1 = new DataGridView();
            col_date = new DataGridViewTextBoxColumn();
            col_time = new DataGridViewTextBoxColumn();
            col_temp = new DataGridViewTextBoxColumn();
            tabPage3 = new TabPage();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(1, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1191, 828);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btn_login);
            tabPage1.Controls.Add(lb_temp);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(btn_exit);
            tabPage1.Controls.Add(btn_connect);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1183, 790);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "현재 온도";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_login
            // 
            btn_login.Location = new Point(964, 19);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(199, 81);
            btn_login.TabIndex = 5;
            btn_login.Text = "카카오 로그인";
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_Click;
            // 
            // lb_temp
            // 
            lb_temp.Font = new Font("맑은 고딕", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lb_temp.Location = new Point(595, 483);
            lb_temp.Name = "lb_temp";
            lb_temp.Size = new Size(413, 188);
            lb_temp.TabIndex = 4;
            lb_temp.Text = "temp";
            lb_temp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("맑은 고딕", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label2.Location = new Point(201, 483);
            label2.Name = "label2";
            label2.Size = new Size(413, 188);
            label2.TabIndex = 3;
            label2.Text = "현재 온도 :";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_exit
            // 
            btn_exit.Font = new Font("맑은 고딕", 20F);
            btn_exit.Location = new Point(689, 297);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new Size(344, 131);
            btn_exit.TabIndex = 2;
            btn_exit.Text = "종료";
            btn_exit.UseVisualStyleBackColor = true;
            btn_exit.Click += btn_exit_Click;
            // 
            // btn_connect
            // 
            btn_connect.Font = new Font("맑은 고딕", 20F);
            btn_connect.Location = new Point(157, 297);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(344, 131);
            btn_connect.TabIndex = 1;
            btn_connect.Text = "연결";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // label1
            // 
            label1.Font = new Font("맑은 고딕", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(385, 39);
            label1.Name = "label1";
            label1.Size = new Size(413, 188);
            label1.TabIndex = 0;
            label1.Text = "온도센서";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1183, 790);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "DB조회";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col_date, col_time, col_temp });
            dataGridView1.Location = new Point(9, 10);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1174, 777);
            dataGridView1.TabIndex = 0;
            // 
            // col_date
            // 
            col_date.HeaderText = "날짜";
            col_date.MinimumWidth = 8;
            col_date.Name = "col_date";
            col_date.Width = 150;
            // 
            // col_time
            // 
            col_time.HeaderText = "시간";
            col_time.MinimumWidth = 8;
            col_time.Name = "col_time";
            col_time.Width = 150;
            // 
            // col_temp
            // 
            col_temp.HeaderText = "온도";
            col_temp.MinimumWidth = 8;
            col_temp.Name = "col_temp";
            col_temp.Width = 150;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(formsPlot1);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1183, 790);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "그래프";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // formsPlot1
            // 
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(3, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1177, 784);
            formsPlot1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1194, 826);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "온도센서";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label1;
        private Label lb_temp;
        private Label label2;
        private Button btn_exit;
        private Button btn_connect;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn col_date;
        private DataGridViewTextBoxColumn col_time;
        private DataGridViewTextBoxColumn col_temp;
        private TabPage tabPage3;
        private Button btn_login;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}
