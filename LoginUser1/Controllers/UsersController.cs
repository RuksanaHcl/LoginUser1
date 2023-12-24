using LoginUser.Models;
using LoginUser.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = await _userRepository.GetAllUsersAsync();

                if (users == null || !users.Any())
                {
                    return NoContent(); //Returns 204 status code when no users are found in database
                }

                var usersData = users.Select(user => new
                {
                    user.UserName,
                    user.Name,
                    user.MobileNumber
                });

                return Ok(usersData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (user == null)
                {
                    return BadRequest("Invalid user data");
                }

                UserAddResult result = await _userRepository.AddUserAsync(user);

                switch (result)
                {
                    case UserAddResult.Success:
                        return Ok("User created successfully");

                    case UserAddResult.UsernameExists:
                        return BadRequest("Username already exists");

                    case UserAddResult.Error:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");

                    default:
                        return BadRequest("Invalid operation result");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
