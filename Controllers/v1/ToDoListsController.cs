using HelloToAsp.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelloToAsp.Controllers.v1
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class ToDoListsController : ControllerBase
    {
        private static List<ToDoList> tasks = new List<ToDoList>
        {
            new ToDoList { Id = 1, Task = "learn C#", Duration = 4 },
            new ToDoList { Id = 2, Task = "learn asp.net core 6", Duration = 10 },
        };

        // GET: api/<ToDoListStaticController>
        [HttpGet]
        public ActionResult<IEnumerable<ToDoList>> Get()
        {
            // IEnumerable this interface just can be read despite of IList and ICollection
            return Ok(tasks);
        }

        // GET api/<ToDoListStaticController>/5
        [HttpGet("{id}")]
        public ActionResult<ToDoList> Get(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST api/<ToDoListStaticController>
        [HttpPost]
        public ActionResult<ToDoList> Post([FromBody] ToDoList value)
        {
            if (tasks.Any(t => t.Id == value.Id))
            {
                return BadRequest("The task is already existed!!");
            }

            tasks.Add(value);

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT api/<ToDoListStaticController>/5
        [HttpPut("{id}")]
        public ActionResult<ToDoList> Put(int id, [FromBody] ToDoList value)
        {
            var existedTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existedTask == null)
            {
                return NotFound();
            }

            existedTask.Id = value.Id;
            existedTask.Task = value.Task;
            existedTask.Duration = value.Duration;

            return Ok(existedTask);
        }

        // DELETE api/<ToDoListStaticController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existedTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existedTask == null)
            {
                return NotFound();
            }

            tasks.Remove(existedTask);

            return NoContent();
        }
    }
}
