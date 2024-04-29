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

        [HttpGet("placeholder")]
        public IActionResult GetPlaceHolderData()
        {
            var placeholderData = new List<Todos>
            {
                new Todos { Id = 1, Description = "Coding assignment!", Date = "29.04.2024", Priority = "High", Completed = false},
                new Todos {Id = 2, Description = "Finished coding assignment", Date = "20.04.2024", Priority = "Medium" ,Completed = true}

            };
            return Ok(placeholderData);
        }

        [HttpGet("filter")]
        public IEnumerable<Todos> GetFiltered([FromQuery] bool completed)
        {
            return _todoList.Where(todo => todo.Completed == completed);
        }
        [HttpGet("page")]
        public IEnumerable<Todos> GetPage([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return _todoList.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        [HttpGet("sort")]
        public IEnumerable<Todos> GetSort([FromQuery] string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "date":
                    return _todoList.OrderBy(todo => todo.Date);
                case "priority":
                    return _todoList.OrderBy(todo => todo.Priority);
                default:
                    return _todoList;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Todos todos)
        {
            if (todos == null)
            {
                return BadRequest("A Todo item cannot be null");
            }
            if (string.IsNullOrEmpty(todos.Description))
            {
                return BadRequest("A Todo item must have a description!");
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
                updatedTodos.Id = id;
                _todoList.Add(updatedTodos);
                return CreatedAtAction(nameof(Get), new { id = updatedTodos.Id }, updatedTodos);
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

