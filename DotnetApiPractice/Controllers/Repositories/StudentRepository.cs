using DotnetApiPractice.DTOs;
using DotnetApiPractice.Data;
using DotnetApiPractice.Models;

namespace DotnetApiPractice.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _context;
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository (StudentDbContext context, ILogger<StudentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
  
    public List<StudentResponseDto>GetAll()
    {
        _logger.LogInformation("Fetching all students from the database");
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
       _logger.LogInformation("Fetching Student with ID {Id}, id");
       var student = _context.Students.FirstOrDefault(s => s.Id == id);
       if(student == null)
        {
            _logger.LogWarning("Student with ID {Id} not found ",id);
            return null;
        }    
 
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
        _logger.LogInformation("Add New Student: {Name}", dto.Name);
       var student = new Student{
           Name = dto.Name,
           Age = dto.Age,
           Course = dto.Course,
           Email = dto.Email
       };

       _context.Students.Add(student);
       _context.SaveChanges();
       _logger.LogInformation(" Student {Name} saved successfully with ID {Id}", student.Name,student.Id);

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
     _logger.LogInformation("Updating student with ID {Id}", id);
    var student = _context.Students.FirstOrDefault(s => s.Id == id);
    if(student == null)
    {
         _logger.LogWarning("Student with ID {Id} not found for update", id);
         return null;
    }  

    student.Name = dto.Name;
    student.Age = dto.Age;
    student.Course = dto.Course;
    student.Email = dto.Email;

    _context.SaveChanges();
    _logger.LogInformation("Student with ID {Id} updated successfully", id);

    return new StudentResponseDto{
        Id = student.Id,
        Name = student.Name,
        Age = student.Age,
        Course = student.Course,
        Email = student.Email
    };

   }

   public bool Delete(int id){
    
     _logger.LogInformation("Deleting student with ID {Id}", id);
    var student = _context.Students.FirstOrDefault(s => s.Id == id);
    if(student == null)
    {
         _logger.LogWarning("Student with ID {Id} not found for deletion", id);
          return false;
    }
    

    _context.Students.Remove(student);
    _context.SaveChanges();
     _logger.LogInformation("Student with ID {Id} deleted successfully", id);
    return true;
   }
}