using AutoMapper;
using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.Dtos.ToDoList;
using HelloToAsp.Exceptions;
using HelloToAsp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloToAsp.Controllers.v2
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    [ApiVersion("2.0")]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public ToDoListsController(
            IToDoListRepository toDoListRepository,
            IAuthorizationService authorizationService,
            IMapper mapper
            )
        {
            _toDoListRepository = toDoListRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        // GET: api/ToDoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListDto>>> Get()
        {
            var userId = User.GetAuthUserId();

            var data = await _toDoListRepository.GetAllAsync(userId);

            return Ok(data);
        }

        // GET: api/ToDoLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListGetDetailsDto>> Get(int id)
        {
            var userId = User.GetAuthUserId(); // fetch auth id globally

            var toDoList = await _toDoListRepository.GetAsync(userId, id) ?? throw new NotFoundException(nameof(Get), id);


            var authResult = await _authorizationService.AuthorizeAsync(User, toDoList, "ToDoListOwner");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var mappedToDoList = _mapper.Map<ToDoListGetDetailsDto>(toDoList);

            return mappedToDoList;
        }

        // PUT: api/ToDoLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ToDoListUpdateDto toDoList)
        {
            if (id != toDoList.Id)
            {
                return BadRequest();
            }

            var existingTask = await _toDoListRepository.GetAsync(id) ?? throw new NotFoundException(nameof(Put), id);

            var authResult = await _authorizationService.AuthorizeAsync(User, existingTask, "ToDoListOwner");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(toDoList, existingTask);

            try
            {
                await _toDoListRepository.UpdateAsync(existingTask);
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
        public async Task<ActionResult> Post([FromForm] ToDoListCreateDto toDoList)
        {
            var newTask = _mapper.Map<ToDoList>(toDoList);

            newTask.UserId = User.GetAuthUserId();

            await _toDoListRepository.AddAsync(newTask);

            return NoContent();
        }

        // DELETE: api/ToDoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDoList = await _toDoListRepository.GetAsync(id) ?? throw new NotFoundException(nameof(Delete), id);

            var authResult = await _authorizationService.AuthorizeAsync(User, toDoList, "ToDoListOwner");

            if (!authResult.Succeeded)
            {
                return Forbid();
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
