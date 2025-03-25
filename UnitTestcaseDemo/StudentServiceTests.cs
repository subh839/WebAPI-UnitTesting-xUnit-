using Moq;
using WebAPI_UnitTesting.Model;
using WebAPI_UnitTesting.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;
namespace UnitTestcaseDemo
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentService> _mockStudentService;

        public StudentServiceTests()
        {
            _mockStudentService = new Mock<IStudentService>();
        }

        private List<Student> GetMockStudents()
        {
            return new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 20, Birthday = "2003-05-12" },
            new Student { Id = 2, Name = "Bob", Age = 22, Birthday = "2001-08-19" },
            new Student { Id = 3, Name = "Charlie", Age = 21, Birthday = "2002-03-25" }
        };
        }
        [Fact]
        public void GetStudents_ShouldReturn_AllStudents()
        {
            // Arrange
            var students = GetMockStudents();
            _mockStudentService.Setup(service => service.GetStudents()).Returns(students);

            // Act
            var result = _mockStudentService.Object.GetStudents();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }
        [Fact]
        public void GetStudentById_InvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockStudentService.Setup(service => service.GetStudentById(99)).Returns((Student)null);

            // Act
            var result = _mockStudentService.Object.GetStudentById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddStudent_ShouldReturn_NewStudent()
        {
            // Arrange
            var newStudent = new Student { Id = 4, Name = "David", Age = 23, Birthday = "2000-07-15" };
            _mockStudentService.Setup(service => service.AddStudent(newStudent)).Returns(newStudent);

            // Act
            var result = _mockStudentService.Object.AddStudent(newStudent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("David", result.Name);
        }
        [Fact]
        public void UpdateStudent_ValidStudent_ShouldReturnUpdatedStudent()
        {
            // Arrange
            var updatedStudent = new Student { Id = 1, Name = "Alice Updated", Age = 20, Birthday = "2003-05-12" };
            _mockStudentService.Setup(service => service.UpdateStudent(updatedStudent)).Returns(updatedStudent);

            // Act
            var result = _mockStudentService.Object.UpdateStudent(updatedStudent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice Updated", result.Name);
        }

        [Fact]
        public void DeleteStudent_ValidId_ShouldNotThrowException()
        {
            // Arrange
            _mockStudentService.Setup(service => service.DeleteStudent(1));

            // Act & Assert
            var exception = Record.Exception(() => _mockStudentService.Object.DeleteStudent(1));
            Assert.Null(exception);
        }

    }
}