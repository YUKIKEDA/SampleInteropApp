using SimpleToDoApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleToDoApp.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> AddAsync(TodoItem todoItem);
        Task DeleteAsync(int id);
    }
} 