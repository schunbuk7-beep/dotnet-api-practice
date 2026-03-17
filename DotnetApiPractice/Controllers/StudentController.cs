using Microsoft.AspNetCore.Mvc;
using DotnetApiPractice.DTOs;
using DotnetApiPractice.Services;

namespace DotnetApiPractice.Conrollers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
         _studentService =  studentService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
       return Ok(_studentService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
       var student = _studentService.GetById(id);
       if(student == null)
       return NotFound(new {Message = $"Student with Id {id} not found"});
       
       return Ok(student);
    }

    [HttpPost]
    public IActionResult Add([FromBody] StudentCreateDto dto)
    {
        var created = _studentService.Add(dto);
        return CreatedAtAction(nameof(GetById),new {id = created.Id}, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id,[FromBody] StudentCreateDto dto )
    {
        var student = _studentService.Update(id,dto);

        if(student == null)
        return NotFound(new { Message =  $"Student with Id {id} not Found"});

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _studentService.Delete(id);
        
        if (!result)
            return NotFound(new { Message = $"Student with ID {id} not found" });

        return NoContent();
    }
}