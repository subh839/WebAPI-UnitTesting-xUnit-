using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI_UnitTesting.Model;
using WebAPI_UnitTesting.Services;
namespace WebAPI_UnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _studentService.GetStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllStudents: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Student not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetStudentById: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            try
            {
                var newStudent = _studentService.AddStudent(student);
                return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddStudent: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            try
            {
                if (id != student.Id)
                    return BadRequest("ID mismatch");

                var updatedStudent = _studentService.UpdateStudent(student);
                return Ok(updatedStudent);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Student not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateStudent: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Student not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteStudent: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
