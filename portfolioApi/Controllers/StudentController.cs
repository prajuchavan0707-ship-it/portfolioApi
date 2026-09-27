// using Microsoft.AspNetCore.Mvc;  // it uesd to crreat wed api it contains the methods & classes
// using portfolioApi.Model;

// namespace portfolioApi.Controllers;

// [ApiController]
// [Route("api/[controller]")]     //it defines the url pattern  for controller
// public class StudentController : ControllerBase
// {
//     // Static list to persist student data in memory for testing
//     private static List<Student> students = new List<Student>
//     {
//         new Student { Prn = 1, FirstName = "Prajakta", LastName = "APRNS" },
//         new Student { Prn = 2, FirstName = "Diksha", LastName = "APRNS" },
//         new Student { Prn = 3, FirstName = "Mrunal", LastName = "Computer" }
//     };

//     // 1. GET ALL STUDENTS
//     // GET: api/Student
//     [HttpGet]
//     public IActionResult GetAll()
//     {
//         return Ok(students);
//     }

//     // 2. GET STUDENT BY PRN
//     // GET: api/Student/1
//     [HttpGet("{prn}")]
//     public IActionResult GetByPrn(int prn)
//     {
//         var student = students.FirstOrDefault(s => s.Prn == prn);
//         if (student == null)
//         {
//             return NotFound(new { message = $"Student with PRN {prn} not found." });
//         }
//         return Ok(student);
//     }

//     // 3. POST (CREATE) NEW STUDENT
//     // POST: api/Student
//     [HttpPost]
//     public IActionResult Post([FromBody] Student student)
//     {
//         if (student == null)
//         {
//             return BadRequest(new { message = "Invalprn student data." });
//         }

//         // Auto-assign Prn if not specified or 0
//         if (student.Prn == 0)
//         {
//             student.Prn = students.Count > 0 ? students.Max(s => s.Prn) + 1 : 1;
//         }

//         students.Add(student);
//         return CreatedAtAction(firstnameof(GetByPrn), new { prn = student.Prn }, student);
//     }

//     // 4. PUT (UPDATE) STUDENT BY PRN
//     // PUT: api/Student/1
//     [HttpPut("{prn}")]
//     public IActionResult Put(int prn, [FromBody] Student updatedStudent)
//     {
//         if (updatedStudent == null)
//         {
//             return BadRequest(new { message = "Invalprn student data." });
//         }

//         var student = students.FirstOrDefault(s => s.Prn == prn);
//         if (student == null)
//         {
//             return NotFound(new { message = $"Student with PRN {prn} not found." });
//         }

//         student.FirstName = updatedStudent.FirstName;
//         student.LastName = updatedStudent.LastName;

//         return Ok(new { message = "Student updated successfully.", student });
//     }

//     // 5. DELETE STUDENT BY PRN
//     // DELETE: api/Student/1
//     [HttpDelete("{prn}")]
//     public IActionResult Delete(int prn)
//     {
//         var student = students.FirstOrDefault(s => s.Prn == prn);
//         if (student == null)
//         {
//             return NotFound(new { message = $"Student with PRN {prn} not found." });
//         }

//         students.Remove(student);
//         return Ok(new { message = $"Student with PRN {prn} deleted successfully." });
//     }
// }