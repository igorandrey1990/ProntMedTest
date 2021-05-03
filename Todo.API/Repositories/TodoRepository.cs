using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.API.Repositories.Interfaces;
using Todo.API.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Todo.API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;
        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoModel>> Get()
        {
            return await _context
                            .Todos
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<TodoModel> Get(long id)
        {
            FilterDefinition<TodoModel> filter = Builders<TodoModel>.Filter.Eq(m => m.Id, id);
            return _context
                    .Todos
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(TodoModel todo)
        {
            await _context.Todos.InsertOneAsync(todo);
        }
        public async Task<bool> Update(TodoModel todo)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Todos
                        .ReplaceOneAsync(
                            filter: g => g.Id == todo.Id,
                            replacement: todo);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<TodoModel> filter = Builders<TodoModel>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Todos
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
