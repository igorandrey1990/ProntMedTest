using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.API.Repositories.Interfaces;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TodosController : Controller
    {
        private readonly ITodoRepository _repo;
        public TodosController(ITodoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoModel>>> Get()
        {
            return new ObjectResult(await _repo.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> Get(long id)
        {
            var todo = await _repo.Get(id);

            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] TodoModel todo)
        {
            todo.Id = await _repo.GetNextId();

            await _repo.Create(todo);

            return new OkObjectResult(todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoModel>> Put(long id, [FromBody] TodoModel todo)
        {
            var todoFromDb = await _repo.Get(todo.Id);

            if (todoFromDb == null)
                return new NotFoundResult();

            await _repo.Update(todo);

            return new OkObjectResult(todo);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.Get(id);

            if (post == null)
                return new NotFoundResult();

            await _repo.Delete(id);

            return new OkResult();
        }
    }
}
