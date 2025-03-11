using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleToDoApp.Core.Infrastructure;
using SimpleToDoApp.Core.Services;
using SimpleToDoApp.Wpf.ViewModels;

namespace SimpleToDoApp.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var repository = new InMemoryTodoRepository();
        var todoService = new TodoService(repository);
        DataContext = new MainWindowViewModel(todoService);
    }
}