using System.Configuration;
using System.Data;
using System.Windows;
using SimpleToDoApp.Wpf.IpcServer;

namespace SimpleToDoApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IpcServerManager? _ipcServer;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        StartIpcServer();
    }

    private void StartIpcServer()
    {
        _ipcServer = new IpcServerManager(HandleIpcMessage);
        Task.Run(() => _ipcServer.StartAsync());
    }

    private void HandleIpcMessage(string message)
    {
        // TODO: メッセージに応じてWPFアプリケーションの機能を実行
        Dispatcher.Invoke(() =>
        {
            MessageBox.Show($"Received message: {message}");
        });
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _ipcServer?.Stop();
        base.OnExit(e);
    }
}

