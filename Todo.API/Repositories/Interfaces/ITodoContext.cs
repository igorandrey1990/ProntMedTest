using MongoDB.Driver;
using Todo.API.Models;

namespace Todo.API.Repositories.Interfaces
{
    public interface ITodoContext
    {
        IMongoCollection<TodoModel> Todos { get; }
    }
}
