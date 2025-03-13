using System.IO.Pipes;
using System.Text;

namespace SimpleToDoApp.Wpf.IpcServer
{
    public class IpcServerManager : IDisposable
    {
        private const string PipeName = "SimpleToDoAppPipe";
        private readonly Action<string> _messageHandler;
        private bool _isRunning;
        private bool _disposed;
        private CancellationTokenSource? _cts;

        public IpcServerManager(Action<string> messageHandler)
        {
            _messageHandler = messageHandler;
            _cts = new CancellationTokenSource();
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            _isRunning = true;

            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts!.Token, cancellationToken);

            while (_isRunning && !linkedCts.Token.IsCancellationRequested)
            {
                using var pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1);
                try
                {
                    await pipeServer.WaitForConnectionAsync(linkedCts.Token);
                    using var reader = new StreamReader(pipeServer);
                    string? message = await reader.ReadLineAsync(linkedCts.Token);
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
            ThrowIfDisposed();
            _isRunning = false;
            _cts?.Cancel();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();
                    _cts?.Dispose();
                    _cts = null;
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
                throw new ObjectDisposedException(nameof(IpcServerManager));
            }
        }
    }
}
