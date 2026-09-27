using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    // Injecting Repository via Constructor
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // 1. GET ALL USERS
    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()        //Task represents that ongoing operation.   //"Start the database operation and wait for its result without blocking."
    {
        try
        {
            var result = await _userRepository.GetAll(); // Call the repository method to get all users 
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while retrieving user records.",
                error = ex.Message
            });
        }
    }

    // 2. GET USER BY ID
    // GET: api/User/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new 
                    { 
                        message = "Invalid user ID." 
                    });
            }

            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound(new 
                {
                     message = $"User with ID {id} not found." 
                });
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while retrieving user with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 3. POST NEW USER
    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest(new 
                {
                     message = "Invalid User data." 
                });
            }

            var createdUser = await _userRepository.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while creating user record.",
                error = ex.Message
            });
        }
    }

    // 4. PUT (UPDATE) USER BY ID
    // PUT: api/User/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Updateuser(int id, [FromBody] User updatedUser)
    {
        try
        {
            if (updatedUser == null || id <= 0)
            {
                return BadRequest(new { message = "Invalid User data or ID." });
            }

            var user = await _userRepository.Update(id, updatedUser);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(new { message = "User updated successfully.", user });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while updating user with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 5. DELETE USER BY ID
    // DELETE: api/User/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new 
                {
                     message = "Invalid user ID." 
                });
            }

            var success = await _userRepository.Delete(id);
            if (!success)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(new { message = $"User with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while deleting user with ID {id}.",
                error = ex.Message
            });
        }
    }
}
