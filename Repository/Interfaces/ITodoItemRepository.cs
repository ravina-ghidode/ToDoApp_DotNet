using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITodoItemRepository : IRepositoryBase<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetAllToDoItemsAsync(); 
        Task<TodoItem> GetTodoByIdAsync(int id);
        Task<TodoItem> AddTodoItemAsync(TodoItem item);
        TodoItem UpdateTodoItem(TodoItem item);
        TodoItem DeleteTodoItem(TodoItem item);

        Task<IEnumerable<TodoItem>> GetPagedDataAsync(int pageNumber, int pageSize);

    }
}
