using SimpleToDoApp.Core.Models;
using SimpleToDoApp.Core.Services;
using SimpleToDoApp.Wpf.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Linq;

namespace SimpleToDoApp.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TodoService _todoService;
        private string? _newTodoTitle;
        private ObservableCollection<TodoItem> _todos = new();

        public MainWindowViewModel(TodoService todoService)
        {
            _todoService = todoService;
            AddCommand = new RelayCommand(async _ => await AddTodo(), _ => !string.IsNullOrWhiteSpace(NewTodoTitle));
            DeleteCommand = new RelayCommand(async param => await DeleteTodo(param as TodoItem));
            LoadTodosAsync();
        }

        public ObservableCollection<TodoItem> Todos
        {
            get => _todos;
            set => SetProperty(ref _todos, value);
        }

        public string? NewTodoTitle
        {
            get => _newTodoTitle;
            set
            {
                if (SetProperty(ref _newTodoTitle, value))
                {
                    (AddCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        private async Task AddTodo()
        {
            if (string.IsNullOrWhiteSpace(NewTodoTitle)) return;

            var newTodo = await _todoService.AddTodoAsync(NewTodoTitle);
            Todos.Add(newTodo);
            NewTodoTitle = string.Empty;
        }

        private async Task DeleteTodo(TodoItem? todoItem)
        {
            if (todoItem == null) return;

            await _todoService.DeleteTodoAsync(todoItem.Id);
            Todos.Remove(todoItem);
        }

        private async void LoadTodosAsync()
        {
            var todos = await _todoService.GetAllTodosAsync();
            Todos = new ObservableCollection<TodoItem>(todos);
        }
    }
} 