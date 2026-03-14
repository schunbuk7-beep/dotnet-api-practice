using DotnetApiPractice.Models;
using DotnetApiPractice.Data;

namespace DotnetApiPractice.Services;

public class StudentService
{
  private readonly StudentDbContext _context;

    public StudentService(StudentDbContext context)
    {
        _context = context;
    }

    public List<Student> GetAll()
    {
        return _context.Students.ToList();
    }

    public Student? GetById(int id)
    {
      return _context.Students.FirstOrDefault(s => s.Id == id);
    }

    public Student Add(Student student)
    {
      _context.Students.Add(student);
      _context.SaveChanges();
      return student;
    }

    public Student? Update(int id,Student updatedStudent)
    {
      var student = _context.Students.FirstOrDefault(s => s.Id == id);
      if(student == null)  return null;

      student.Name = updatedStudent.Name;
      student.Age = updatedStudent.Age;
      student.Course = updatedStudent.Course;

      _context.SaveChanges();
      return student;
    }

    public bool Delete(int id)
    {
       var student = _context.Students.FirstOrDefault(s => s.Id == id);
       if(student == null) return false;

       _context.Students.Remove(student);
       _context.SaveChanges();
       return true;
    }
}