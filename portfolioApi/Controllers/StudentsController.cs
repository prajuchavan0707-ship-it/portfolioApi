using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    // Injecting Repository via Constructor
    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    // 1. GET ALL STUDENTS
    // GET: api/Students
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        try
        {
            var result = await _studentRepository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while retrieving student records.",
                error = ex.Message
            });
        }
    }

    // 2. GET STUDENT BY ID
    // GET: api/Students/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentsById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid student ID." });
            }

            var student = await _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound(new { message = $"Student with ID {id} not found." });
            }
            return Ok(student);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while retrieving student with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 3. POST NEW STUDENT
    // POST: api/Students
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Student student)
    {
        try
        {
            if (student == null)
            {
                return BadRequest(new { message = "Invalid student data." });
            }

            var createdStudent = await _studentRepository.Add(student);
            return CreatedAtAction(nameof(GetStudentsById), new { id = createdStudent.Prn }, createdStudent);//CreatedAtAction =It returns an HTTP response with status:
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while creating the student record.",
                error = ex.Message
            });
        }
    }

    // 4. PUT (UPDATE) STUDENT BY ID
    // PUT: api/Students/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        try
        {
            if (updatedStudent == null || id <= 0)
            {
                return BadRequest(new { message = "Invalid student data or ID." });
            }

            updatedStudent.Prn = id;
            var student = await _studentRepository.Update(updatedStudent);
            if (student == null)
            {
                return NotFound(new { message = $"Student with ID {id} not found." });
            }

            return Ok(new { message = "Student updated successfully.", student });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while updating student with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 5. DELETE STUDENT BY ID
    // DELETE: api/Students/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid student ID." });
            }

            var success = await _studentRepository.Delete(id);
            if (!success)
            {
                return NotFound(new { message = $"Student with ID {id} not found." });
            }

            return Ok(new { message = $"Student with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while deleting student with ID {id}.",
                error = ex.Message
            });
        }
    }
}
