using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultyController : ControllerBase
{
    private readonly IFacultyRepository _facultyRepository;

    // Injecting Repository via Constructor
    public FacultyController(IFacultyRepository facultyRepository)
    {
        _facultyRepository = facultyRepository;
    }

    // 1. GET ALL FACULTIES
    // GET: api/Faculty
    [HttpGet]
    public async Task<IActionResult> GetAllFaculty()
    {
        try
        {
            var result = await _facultyRepository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while retrieving faculty records.",
                error = ex.Message
            });
        }
    }

    // 2. GET FACULTY BY ID
    // GET: api/Faculty/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetfacultyById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid faculty ID." });
            }

            var faculty = await _facultyRepository.GetById(id);
            if (faculty == null)
            {
                return NotFound(new { message = $"Faculty with ID {id} not found." });
            }

            return Ok(faculty);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while retrieving faculty with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 3. ADD NEW FACULTY
    // POST: api/Faculty
    [HttpPost]
    public async Task<IActionResult> AddFaculty([FromBody] Faculty faculty)
    {
        try
        {
            if (faculty == null)
            {
                return BadRequest(new { message = "Invalid Faculty data." });
            }

            var createdFaculty = await _facultyRepository.Add(faculty);
            return CreatedAtAction(
                nameof(GetfacultyById),
                new { id = createdFaculty.FacultyId },
                createdFaculty
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while creating faculty record.",
                error = ex.Message
            });
        }
    }

    // 4. UPDATE FACULTY BY ID
    // PUT: api/Faculty/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Updatefaculty(int id, [FromBody] Faculty updatedFaculty)
    {
        try
        {
            if (updatedFaculty == null || id <= 0)
            {
                return BadRequest(new { message = "Invalid Faculty data or ID." });
            }

            var faculty = await _facultyRepository.Update(id, updatedFaculty);
            if (faculty == null)
            {
                return NotFound(new { message = $"Faculty with ID {id} not found." });
            }

            return Ok(new
            {
                message = "Faculty updated successfully.",
                faculty
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while updating faculty with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 5. DELETE FACULTY BY ID
    // DELETE: api/Faculty/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaculty(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid faculty ID." });
            }

            var success = await _facultyRepository.Delete(id);
            if (!success)
            {
                return NotFound(new { message = $"Faculty with ID {id} not found." });
            }

            return Ok(new { message = $"Faculty with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while deleting faculty with ID {id}.",
                error = ex.Message
            });
        }
    }
}