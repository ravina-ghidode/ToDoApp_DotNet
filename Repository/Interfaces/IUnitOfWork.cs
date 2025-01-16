using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        //IDeveloperRepository DeveloperRepository { get; }
        Task CompleteAsync();
    }
}
