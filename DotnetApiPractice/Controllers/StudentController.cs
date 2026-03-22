using Microsoft.AspNetCore.Mvc;
using DotnetApiPractice.DTOs;
using DotnetApiPractice.Repositories;

namespace DotnetApiPractice.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _repository;
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentRepository repository, ILogger<StudentController> logger)
    {
         _repository =  repository;
         _logger = logger;

    }

    [HttpGet]
    public IActionResult GetAll()
    {
       _logger.LogInformation("GET/student called");
       return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
       _logger.LogInformation("GET /student/{Id} called, id");
       var student = _repository.GetById(id);
       if(student == null)
       return NotFound(new {Message = $"Student with Id {id} not found"});
       
       return Ok(student);
    }

    [HttpPost]
    public IActionResult Add([FromBody] StudentCreateDto dto)
    {
        _logger.LogInformation("POST /student called for{Name}", dto.Name);
        var created = _repository.Add(dto);
        return CreatedAtAction(nameof(GetById),new {id = created.Id}, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id,[FromBody] StudentCreateDto dto )
    {
        _logger.LogInformation("PUT/student/{Id} called, id");
        var student = _repository.Update(id,dto);

        if(student == null)
        return NotFound(new { Message =  $"Student with Id {id} not Found"});

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("DELETE /student/{Id} called", id);
        var result = _repository.Delete(id);
        
        if (!result)
            return NotFound(new { Message = $"Student with ID {id} not found" });

        return NoContent();
    }
}