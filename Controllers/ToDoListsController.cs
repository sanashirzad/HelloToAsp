using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloToAsp.Data;
using HelloToAsp.Contracts;

namespace HelloToAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoListsController(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        // GET: api/ToDoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetToDoLists()
        {
            return await _toDoListRepository.GetAllAsync();
        }

        // GET: api/ToDoLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoList>> GetToDoList(int id)
        {
            var toDoList = await _toDoListRepository.GetAsync(id);

            if (toDoList == null)
            {
                return NotFound();
            }

            return toDoList;
        }

        // PUT: api/ToDoLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoList(int id, ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return BadRequest();
            }

            try
            {
                await _toDoListRepository.UpdateAsync(toDoList);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ToDoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoLists
        [HttpPost]
        public async Task<ActionResult<ToDoList>> PostToDoList(ToDoList toDoList)
        {
            _toDoListRepository.AddAsync(toDoList);

            return CreatedAtAction("GetToDoList", new { id = toDoList.Id }, toDoList);
        }

        // DELETE: api/ToDoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(int id)
        {
            var toDoList = await _toDoListRepository.GetAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            await _toDoListRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> ToDoListExists(int id)
        {
            return await _toDoListRepository.Exists(id);
        }
    }
}
