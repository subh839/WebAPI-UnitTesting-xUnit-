using System.Xml.Serialization;
using WebAPI_UnitTesting.Model;

namespace WebAPI_UnitTesting.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Student GetStudentById(int id);
        Student AddStudent(Student student);
        Student UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
