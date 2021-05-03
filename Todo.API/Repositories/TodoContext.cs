using MongoDB.Driver;
using Todo.API.Repositories.Interfaces;
using Todo.API.Models;
using Todo.API.Configs;

namespace Todo.API.Repositories
{
    public class TodoContext : ITodoContext
    {
        private readonly IMongoDatabase _db;
        public TodoContext(MongoDBConfig config)
        {
            var client = new MongoClient("mongodb://mongo:27017");
            _db = client.GetDatabase("TodoDb");
        }
        public IMongoCollection<TodoModel> Todos => _db.GetCollection<TodoModel>("Todos");
    }
}
