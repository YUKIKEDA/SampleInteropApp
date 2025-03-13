using System.Configuration;
using System.Data;
using System.Windows;
using SimpleToDoApp.Core.Services;
using SimpleToDoApp.Core.Infrastructure;
using SimpleToDoApp.Wpf.IpcServer;
using SimpleToDoApp.Wpf.IpcServer.Models;
using SimpleToDoApp.Wpf.ViewModels;

namespace SimpleToDoApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IpcServerManager? _ipcServer;
    private IpcMessageHandler? _messageHandler;
    private TodoService? _todoService;
    private MainWindowViewModel? _mainViewModel;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // サービスの初期化
        var repository = new InMemoryTodoRepository();
        _todoService = new TodoService(repository);
        _mainViewModel = new MainWindowViewModel(_todoService);

        // メインウィンドウの設定
        var mainWindow = new MainWindow
        {
            DataContext = _mainViewModel
        };
        mainWindow.Show();

        // IPCサーバーの開始
        StartIpcServer();
    }

    private void StartIpcServer()
    {
        if (_todoService == null) return;

        _messageHandler = new IpcMessageHandler(_todoService);
        _ipcServer = new IpcServerManager(HandleIpcMessage);
        Task.Run(() => _ipcServer.StartAsync());
    }

    private async void HandleIpcMessage(string message)
    {
        if (_messageHandler == null) return;

        await Dispatcher.InvokeAsync(async () =>
        {
            await _messageHandler.HandleMessageAsync(message);
            // ViewModelの更新
            if (_mainViewModel != null)
            {
                await _mainViewModel.RefreshTodosAsync();
            }
        });
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _ipcServer?.Stop();
        base.OnExit(e);
    }
}

