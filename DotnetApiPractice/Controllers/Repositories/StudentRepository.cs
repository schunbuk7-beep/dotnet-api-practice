using DotnetApiPractice.DTOs;
using DotnetApiPractice.Data;
using DotnetApiPractice.Models;

namespace DotnetApiPractice.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _context;

    public StudentRepository (StudentDbContext context)
    {
        _context = context;
    }
  
    public List<StudentResponseDto>GetAll()
    {
        return _context.Students.Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            Course = s.Course,
            Email = s.Email
        }).ToList();
    }

    public StudentResponseDto? GetById(int id)
    {
       var student = _context.Students.FirstOrDefault(s => s.Id == id);
       if(student == null) return null;
 
       return new StudentResponseDto
       {
        Id = student.Id,
        Name = student.Name,
        Age = student.Age,
        Course = student.Course,
        Email = student.Email
       };
    }

    public StudentResponseDto Add(StudentCreateDto dto)
    {
       var student = new Student{
           Name = dto.Name,
           Age = dto.Age,
           Course = dto.Course,
           Email = dto.Email
       };

       _context.Students.Add(student);
       _context.SaveChanges();

       return new StudentResponseDto{
        Id = student.Id,
        Name = student.Name,
        Age = student.Age,
        Course = student.Course,
        Email = student.Email
       };
   }

   public StudentResponseDto? Update(int id, StudentCreateDto dto )
   {
    var student = _context.Students.FirstOrDefault(s => s.Id == id);
    if(student == null)  return null;

    student.Name = dto.Name;
    student.Age = dto.Age;
    student.Course = dto.Course;
    student.Email = dto.Email;

    _context.SaveChanges();

    return new StudentResponseDto{
        Id = student.Id,
        Name = student.Name,
        Age = student.Age,
        Course = student.Course,
        Email = student.Email
    };

   }

   public bool Delete(int id){
    
    var student = _context.Students.FirstOrDefault(s => s.Id == id);
    if(student == null) return false;

    _context.Students.Remove(student);
    _context.SaveChanges();
    return true;
   }
}