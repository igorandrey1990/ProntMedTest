using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.API.Models;

namespace Todo.API.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoModel>> Get();
        Task<TodoModel> Get(long id);
        Task Create(TodoModel todo);
        Task<bool> Update(TodoModel todo);
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}
