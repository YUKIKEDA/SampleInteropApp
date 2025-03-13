using System.IO.Pipes;
using System.Text;
using SimpleToDoApp.Wpf.IpcServer.Models;

namespace SimpleToDoApp.ConsoleApp.Services
{
    public class IpcClient : IDisposable
    {
        private const string PipeName = "SimpleToDoAppPipe";
        private bool _disposed;
        private readonly SemaphoreSlim _sendLock;

        public IpcClient()
        {
            _sendLock = new SemaphoreSlim(1, 1);
        }

        public async Task SendMessageAsync(IpcMessage message)
        {
            ThrowIfDisposed();

            await _sendLock.WaitAsync();
            try
            {
                using var pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
                try
                {
                    await pipeClient.ConnectAsync(5000); // 5秒のタイムアウト
                    using var writer = new StreamWriter(pipeClient);
                    await writer.WriteLineAsync(message.Serialize());
                    await writer.FlushAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending message: {ex.Message}");
                    throw;
                }
            }
            finally
            {
                _sendLock.Release();
            }
        }

        public async Task AddTodoAsync(string title)
        {
            var message = IpcMessage.Create("AddTodo", new Dictionary<string, string>
            {
                ["title"] = title
            });
            await SendMessageAsync(message);
        }

        public async Task DeleteTodoAsync(int id)
        {
            var message = IpcMessage.Create("DeleteTodo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            await SendMessageAsync(message);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sendLock.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(IpcClient));
            }
        }
    }
} 