using System.IO.Pipes;
using System.Text;

namespace SimpleToDoApp.ConsoleApp.Services
{
    public class IpcClient
    {
        private const string PipeName = "SimpleToDoAppPipe";

        public async Task SendMessageAsync(string message)
        {
            using var pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
            try
            {
                await pipeClient.ConnectAsync(5000); // 5秒のタイムアウト
                using var writer = new StreamWriter(pipeClient);
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                throw;
            }
        }
    }
} 