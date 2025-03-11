using SimpleToDoApp.Core.Interfaces;
using SimpleToDoApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleToDoApp.Core.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem> AddTodoAsync(string title)
        {
            var todoItem = new TodoItem(title);
            return await _repository.AddAsync(todoItem);
        }

        public async Task DeleteTodoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
} 