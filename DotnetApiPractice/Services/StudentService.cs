using DotnetApiPractice.Models;

namespace DotnetApiPractice.Services;

public class StudentService
{
    private static List<Student> students = new List<Student>
    {
        new Student {Id=1, Name = "Kim Namjoon"   , Age=31 , Course = "Leading The World Biggest BoyBand"},
        new Student {Id=2, Name = "Kim Seokjin"   , Age=33 , Course = "World Wide Handsome"},
        new Student {Id=3, Name = "Min Yoongi"    , Age=33 , Course = "Producing Music"},
        new Student {Id=4, Name = "Jung Hoseok"   , Age=32 , Course = "Chreographing"},
        new Student {Id=5, Name = "Park Jimin"    , Age=30 , Course = "Singing"},
        new Student {Id=6, Name = "Kim Taehyung"  , Age=30 , Course = "Singing"},
        new Student {Id=7, Name = "Jeon Jungkook" , Age=28 , Course = "MultiTask"}

    };

    public List<Student> GetAll()
    {
        return students;
    }

    public Student GetById(int id)
    {
      return students.FirstOrDefault(s => s.Id == id);
    }

    public Student Add(Student student)
    {
      students.Add(student);
      return student;
    }

    public Student Update(int id,Student updatedStudent)
    {
      var student = students.FirstOrDefault(s => s.Id == id);
      if(student == null)  return null;

      student.Name = updatedStudent.Name;
      student.Age = updatedStudent.Age;
      student.Course = updatedStudent.Course;

      return student;
    }

    public bool Delete(int id)
    {
       var student = students.FirstOrDefault(s => s.Id == id);
       if(student == null) return false;

       students.Remove(student);
       return true;
    }
}