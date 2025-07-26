using AutoMapper;
using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloToAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(
            IUsersRepository usersRepository,
            IMapper mapper
            )
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDto>>> GetUsers()
        {
            var users = await _usersRepository.GetAllAsync();
            var mappedUsers = _mapper.Map<List<GetDto>>(users);

            return Ok(mappedUsers);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDetailsDto>> GetUser(int id)
        {
            //var user = await _context.Users.FindAsync(id);

            var user = await _usersRepository.GetDetails(id);

            if (user == null)
            {
                return NotFound();
            }

            var mappedUser = _mapper.Map<GetDetailsDto>(user);

            return mappedUser;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromForm] UpdateDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            // this is efficient more than the below one this just update the desired values 
            // not all of the properties
            //_context.Entry(user).CurrentValues.SetValues(value);

            //_context.Entry(user).State = EntityState.Modified;

            var existingUser = await _usersRepository.GetAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Map the DTO to the existing entity
            _mapper.Map(user, existingUser);

            try
            {
                await _usersRepository.UpdateAsync(existingUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> PostUser([FromForm] CreateDto user)
        {
            // should use below code if do not use auto mapper
            //var newUser = new User
            //{
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //};

            var newUser = _mapper.Map<User>(user);

            await _usersRepository.AddAsync(newUser);

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _usersRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _usersRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> UserExists(int id)
        {
            return await _usersRepository.Exists(id);
        }
    }
}
