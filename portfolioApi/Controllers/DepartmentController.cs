using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;

    // Injecting Repository via Constructor
    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    // 1. GET ALL DEPARTMENTS
    // GET: api/Department
    [HttpGet]
    public async Task<IActionResult> GetAllDepartment()
    {
        try
        {
            var result = await _departmentRepository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while retrieving department records.",
                error = ex.Message
            });
        }
    }

    // 2. GET DEPARTMENT BY ID
    // GET: api/Department/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid department ID." });
            }

            var department = await _departmentRepository.GetById(id);
            if (department == null)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }
            return Ok(department);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while retrieving department with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 3. ADD NEW DEPARTMENT
    // POST: api/Department
    [HttpPost]
    public async Task<IActionResult> AddDepartment([FromBody] Department department)
    {
        try
        {
            if (department == null)
            {
                return BadRequest(new { message = "Invalid Department data." });
            }

            var createdDepartment = await _departmentRepository.Add(department);
            return CreatedAtAction(
                nameof(GetDepartmentById),
                new { id = createdDepartment.DepartmentId },
                createdDepartment
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while creating department record.",
                error = ex.Message
            });
        }
    }

    // 4. UPDATE DEPARTMENT BY ID
    // PUT: api/Department/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department updatedDepartment)
    {
        try
        {
            if (updatedDepartment == null || id <= 0)
            {
                return BadRequest(new { message = "Invalid Department data or ID." });
            }

            var department = await _departmentRepository.Update(id, updatedDepartment);
            if (department == null)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }

            return Ok(new
            {
                message = "Department updated successfully.",
                department
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while updating department with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 5. DELETE DEPARTMENT BY ID
    // DELETE: api/Department/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid department ID." });
            }

            var success = await _departmentRepository.Delete(id);
            if (!success)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }

            return Ok(new { message = $"Department with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while deleting department with ID {id}.",
                error = ex.Message
            });
        }
    }
}
