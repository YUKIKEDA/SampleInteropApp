using System.IO.Pipes;
using System.Text;

namespace SimpleToDoApp.Wpf.IpcServer
{
    public class IpcServerManager
    {
        private const string PipeName = "SimpleToDoAppPipe";
        private readonly Action<string> _messageHandler;
        private bool _isRunning;

        public IpcServerManager(Action<string> messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _isRunning = true;
            while (_isRunning && !cancellationToken.IsCancellationRequested)
            {
                using var pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1);
                try
                {
                    await pipeServer.WaitForConnectionAsync(cancellationToken);
                    using var reader = new StreamReader(pipeServer);
                    string? message = await reader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(message))
                    {
                        _messageHandler(message);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in pipe server: {ex.Message}");
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
