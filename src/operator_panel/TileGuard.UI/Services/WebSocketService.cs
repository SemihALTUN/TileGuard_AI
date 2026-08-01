using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TileGuard.UI.DTOs; // DTO katmanımız

namespace TileGuard.UI.Services
{
    public class WebSocketService
    {
        private ClientWebSocket? _webSocket;
        private readonly Uri _serverUri = new Uri("ws://127.0.0.1:8000/ws/inspect");

        public event Action<InspectionResultDto>? OnResultReceived;
        public event Action<string>? OnStatusChanged;

        public bool IsConnected => _webSocket != null && _webSocket.State == WebSocketState.Open;

        public async Task ConnectAsync()
        {
            try
            {
                _webSocket = new ClientWebSocket();
                OnStatusChanged?.Invoke("Python AI Servisine baglaniliyor...");
                await _webSocket.ConnectAsync(_serverUri, CancellationToken.None);
                OnStatusChanged?.Invoke("Baglanti Basarili! Servis Hazir.");

                _ = ReceiveLoopAsync();
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke($"Baglanti Hatasi: {ex.Message}");
            }
        }

        public async Task SendImageAsync(Image image)
        {
            if (!IsConnected) return;

            try
            {
                using MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                string base64Image = Convert.ToBase64String(ms.ToArray());

                byte[] bytes = Encoding.UTF8.GetBytes(base64Image);
                await _webSocket!.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke($"Gonderim Hatasi: {ex.Message}");
            }
        }

        private async Task ReceiveLoopAsync()
        {
            byte[] buffer = new byte[1024 * 32];

            while (IsConnected)
            {
                try
                {
                    WebSocketReceiveResult result = await _webSocket!.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        CancellationToken.None
                    );

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Kapatildi", CancellationToken.None);
                        OnStatusChanged?.Invoke("Baglanti Kapatildi.");
                        break;
                    }

                    string jsonMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    InspectionResultDto? resultDto = JsonConvert.DeserializeObject<InspectionResultDto>(jsonMessage);

                    if (resultDto != null)
                    {
                        OnResultReceived?.Invoke(resultDto);
                    }
                }
                catch (Exception ex)
                {
                    OnStatusChanged?.Invoke($"Okuma Hatasi: {ex.Message}");
                    break;
                }
            }
        }

        public async Task DisconnectAsync()
        {
            if (IsConnected)
            {
                await _webSocket!.CloseAsync(WebSocketCloseStatus.NormalClosure, "Kullanici kapatti", CancellationToken.None);
                _webSocket.Dispose();
            }
        }
    }
}