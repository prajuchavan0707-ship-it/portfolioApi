using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceRepository _attendanceRepository;

    // Injecting Repository via Constructor
    public AttendanceController(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    // 1. GET ALL ATTENDANCE RECORDS
    // GET: api/Attendance
    [HttpGet]
    public async Task<IActionResult> GetAllAttendance()
    {
        try
        {
            var result = await _attendanceRepository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while retrieving attendance records.",
                error = ex.Message
            });
        }
    }

    // 2. GET ATTENDANCE BY ID
    // GET: api/Attendance/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttendanceById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid attendance ID." });
            }

            var attendance = await _attendanceRepository.GetById(id);
            if (attendance == null)
            {
                return NotFound(new { message = $"Attendance record with ID {id} not found." });
            }
            return Ok(attendance);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while retrieving attendance record with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 3. ADD NEW ATTENDANCE
    // POST: api/Attendance
    [HttpPost]
    public async Task<IActionResult> AddAttendance([FromBody] Attendance attendance)
    {
        try
        {
            if (attendance == null)
            {
                return BadRequest(new { message = "Invalid Attendance data." });
            }

            var createdAttendance = await _attendanceRepository.Add(attendance);
            return CreatedAtAction(
                nameof(GetAttendanceById),
                new { id = createdAttendance.AttendanceId },
                createdAttendance
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An error occurred while creating attendance record.",
                error = ex.Message
            });
        }
    }

    // 4. UPDATE ATTENDANCE BY ID
    // PUT: api/Attendance/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttendance(int id, [FromBody] Attendance updatedAttendance)
    {
        try
        {
            if (updatedAttendance == null || id <= 0)
            {
                return BadRequest(new { message = "Invalid Attendance data or ID." });
            }

            var attendance = await _attendanceRepository.Update(id, updatedAttendance);
            if (attendance == null)
            {
                return NotFound(new { message = $"Attendance record with ID {id} not found." });
            }

            return Ok(new
            {
                message = "Attendance updated successfully.",
                attendance
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while updating attendance record with ID {id}.",
                error = ex.Message
            });
        }
    }

    // 5. DELETE ATTENDANCE BY ID
    // DELETE: api/Attendance/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttendance(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid attendance ID." });
            }

            var success = await _attendanceRepository.Delete(id);
            if (!success)
            {
                return NotFound(new { message = $"Attendance record with ID {id} not found." });
            }

            return Ok(new { message = $"Attendance record with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = $"An error occurred while deleting attendance record with ID {id}.",
                error = ex.Message
            });
        }
    }
}
