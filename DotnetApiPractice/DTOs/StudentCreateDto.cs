using System.ComponentModel.DataAnnotations;

namespace DotnetApiPractice.DTOs;

public class StudentCreateDto
{
    [Required (ErrorMessage = "Name is Required")]
    [MaxLength(100, ErrorMessage = "Name Cannot Exeed more than 100 characters")]
    public string Name { get; set; }

    [Required (ErrorMessage = "Age is Required")]
    [Range(1,150,ErrorMessage = "Age Must be between 1 to 150")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Course is Required")]
    [MaxLength(200, ErrorMessage = "Course cannot Exeed more than 200 characters")]
    public string Course { get; set; }

    [EmailAddress(ErrorMessage = "Please Provide a valid Email Address")]
     public string? Email { get; set; }
     
 }