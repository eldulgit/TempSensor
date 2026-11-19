using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TempSensor
{
    public class KakaoService
    {
        private string kakaoKey = "aac86fcaea4c631f8a717f902faf63b5";
        private string kakaoSecret = "GDK4l8bceAh3c9jNyxOIQ6s90g2KsoA0";
        private string redirectUri = "http://localhost:8080/";
        private string? accessToken;

        public async Task LoginAsync()
        {
            accessToken = null;
            using HttpListener listener = new HttpListener();

            listener.Prefixes.Add(redirectUri);
            listener.Start();

            string url = "https://kauth.kakao.com/oauth/authorize"
                + "?client_id=" + Uri.EscapeDataString(kakaoKey)
                + "&redirect_uri=" + Uri.EscapeDataString(redirectUri)
                + "&response_type=code";

            ProcessStartInfo info = new ProcessStartInfo(url);
            info.UseShellExecute = true;
            Process.Start(info);

            HttpListenerContext context = await listener.GetContextAsync();

            try
            {
                string? code = context.Request.QueryString["code"];

                if (string.IsNullOrEmpty(code))
                    throw new Exception("로그인이 취소되었거나 실패했습니다.");

                await GetToken(code);

                byte[] message = Encoding.UTF8.GetBytes(
                    "로그인 완료! 이 창을 닫고 온도센서 프로그램으로 돌아가세요.");

                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.OutputStream.WriteAsync(message);
            }
            finally
            {
                context.Response.Close();
            }
        }

        private async Task GetToken(string code)
        {
            using HttpClient http = new HttpClient();

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("grant_type", "authorization_code");
            data.Add("client_id", kakaoKey);
            data.Add("redirect_uri", redirectUri);
            data.Add("code", code);

            if (kakaoSecret != "")
                data.Add("client_secret", kakaoSecret);

            using FormUrlEncodedContent content = new FormUrlEncodedContent(data);
            using HttpResponseMessage response = await http.PostAsync(
                "https://kauth.kakao.com/oauth/token", content);

            string result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("토큰 발급 실패: " + result);

            using JsonDocument json = JsonDocument.Parse(result);
            accessToken = json.RootElement.GetProperty("access_token").GetString();
        }

        public async Task SendAsync(string message)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new Exception("카카오 로그인을 먼저 해주세요.");

            using HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            string template = JsonSerializer.Serialize(new
            {
                object_type = "text",
                text = message,
                link = new
                {
                    web_url = "http://localhost:8080/",
                    mobile_web_url = "http://localhost:8080/"
                },
                button_title = "확인"
            });

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("template_object", template);

            using FormUrlEncodedContent content = new FormUrlEncodedContent(data);
            using HttpResponseMessage response = await http.PostAsync(
                "https://kapi.kakao.com/v2/api/talk/memo/default/send", content);

            string result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("카카오톡 전송 실패: " + result);
        }
    }
}
