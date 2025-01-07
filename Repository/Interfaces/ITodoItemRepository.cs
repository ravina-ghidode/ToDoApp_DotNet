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
        Task<IEnumerable<TodoItem>> GetPagedDataAsync(int pageNumber, int pageSize);

    }
}
