using WebAPI_UnitTesting.Model;

namespace WebAPI_UnitTesting.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students;
        private readonly ILogger<StudentService> _logger;
        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
            _students = new List<Student>
            {
                new Student { Id = 1, Name = "Michael Johnson", Age = 20, Birthday = "2003-06-15" },
                new Student { Id = 2, Name = "Bob Smith", Age = 22, Birthday = "2001-09-10" },
                new Student { Id = 3, Name = "Jivan Smith", Age = 28, Birthday = "2001-05-10" },
                new Student { Id = 4, Name = "Chitta Sung", Age = 26, Birthday = "2001-06-10" }
            };
        }

        public List<Student> GetStudents()
        {
            try
            {
                _logger.LogInformation("Fetching all students.");
                return _students;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching students: {ex.Message}");
                throw;
            }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching student with ID: {id}");
                var student = _students.FirstOrDefault(s => s.Id == id);
                return student ?? throw new KeyNotFoundException("Student not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching student {id}: {ex.Message}");
                throw;
            }
        }

        public Student AddStudent(Student student)
        {
            try
            {
                student.Id = _students.Count + 1;
                _students.Add(student);
                _logger.LogInformation($"Student added: {student.Name}");
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding student: {ex.Message}");
                throw;
            }
        }


        public Student UpdateStudent(Student student)
        {
            try
            {
                var existingStudent = _students.FirstOrDefault(s => s.Id == student.Id);
                if (existingStudent == null) throw new KeyNotFoundException("Student not found.");

                existingStudent.Name = student.Name;
                existingStudent.Age = student.Age;
                existingStudent.Birthday = student.Birthday;

                _logger.LogInformation($"Student updated: {student.Name}");
                return existingStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating student {student.Id}: {ex.Message}");
                throw;
            }
        }

        public void DeleteStudent(int id)
        {
            try
            {
                var student = _students.FirstOrDefault(s => s.Id == id);
                if (student == null) throw new KeyNotFoundException("Student not found.");

                _students.Remove(student);
                _logger.LogInformation($"Student deleted: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting student {id}: {ex.Message}");
                throw;
            }
        }
    }
}
