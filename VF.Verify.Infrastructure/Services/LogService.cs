using System.Net.Http.Json;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Services
{
    public class LogService(HttpClient httpClient) : ILogService
    {
        private readonly HttpClient _httpClient = httpClient;
        private static readonly object logLock = new();
        public async Task SaveLogsMessagesAsync(string messages)

        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("", messages);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error al enviar el log al endpoint");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al enviar el log: {ex.Message}");
            }

        }

        public void SaveLogsMessages(string messages)
        {
            lock (logLock)
            {
                Console.WriteLine($"[{DateTime.Now}] [{messages}");
            }
        }

        public void SaveInfoMessages(string messages)
        {
            lock (logLock)
            {
                Console.WriteLine($"[{DateTime.Now}] [{messages}");
            }
        }

    }
}
