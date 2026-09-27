using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectRepository _subjectRepository;

    // Injecting Repository via Constructor
    public SubjectController(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    // 1. GET ALL SUBJECTS
    // GET: api/Subject
    [HttpGet]
    public async Task<IActionResult> GetAllSubjects()
    {
        try
        {
            var result = await _subjectRepository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while retrieving subject records.",
                error = ex.Message
            });
        }
    }

    // 2. GET SUBJECT BY ID
    // GET: api/Subject/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid subject ID." });
            }

            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound(new { message = $"Subject with ID {id} not found." });
            }
            return Ok(subject);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while retrieving subject with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 3. POST NEW SUBJECT
    // POST: api/Subject
    [HttpPost]
    public async Task<IActionResult> AddSubject([FromBody] Subject subject)
    {
        try
        {
            if (subject == null)
            {
                return BadRequest(new { message = "Invalid Subject data." });
            }

            var createdSubject = await _subjectRepository.Add(subject);
            return CreatedAtAction(nameof(GetSubjectById), new { id = createdSubject.SubjectId }, createdSubject);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while creating subject record.",
                error = ex.Message
            });
        }
    }

    // 4. PUT (UPDATE) SUBJECT BY ID
    // PUT: api/Subject/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, [FromBody] Subject updatedSubject)
    {
        try
        {
            if (updatedSubject == null || id <= 0)
            {
                return BadRequest(new { message = "Invalid Subject data or ID." });
            }

            var subject = await _subjectRepository.Update(id, updatedSubject);
            if (subject == null)
            {
                return NotFound(new { message = $"Subject with ID {id} not found." });
            }

            return Ok(new { message = "Subject updated successfully.", subject });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while updating subject with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 5. DELETE SUBJECT BY ID
    // DELETE: api/Subject/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid subject ID." });
            }

            var success = await _subjectRepository.Delete(id);
            if (!success)
            {
                return NotFound(new { message = $"Subject with ID {id} not found." });
            }

            return Ok(new { message = $"Subject with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while deleting subject with ID {id}.",
                error = ex.Message
            });
        }
    }
}
