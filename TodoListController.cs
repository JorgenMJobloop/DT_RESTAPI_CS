using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {

        private readonly ILogger<TodoListController> _logger;

        private List<Todos> _todoList;

        public TodoListController(ILogger<TodoListController> logger)
        {
            _logger = logger;
            _todoList = new List<Todos>();
        }
        // using the HTTP GET method to fetch data that already exists on the API's route
        [HttpGet]
        public IEnumerable<Todos> Get()
        {
            return _todoList;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Todos todos)
        {
            if (todos == null)
            {
                return BadRequest("A Todo item cannot be null");
            }

            _todoList.Add(todos);
            return CreatedAtAction(nameof(Get), new { id = todos.Id }, todos);
        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] Todos updatedTodos)
        {
            var existingTodos = _todoList.FirstOrDefault(t => t.Id == id);
            if (existingTodos == null)
            {
                return NotFound();
            }

            existingTodos.Description = updatedTodos.Description;
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var removeTodo = _todoList.FirstOrDefault(t => t.Id == id);
            if (removeTodo == null)
            {
                return NotFound();
            }
            _todoList.Remove(removeTodo);
            return NoContent();
        }
    }
}

