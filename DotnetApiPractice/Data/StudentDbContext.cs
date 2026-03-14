using Microsoft.EntityFrameworkCore;
using DotnetApiPractice.Models;

namespace DotnetApiPractice.Data;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext>options) : base(options)
    {

    }

    public DbSet<Student>Students {get; set;}
}