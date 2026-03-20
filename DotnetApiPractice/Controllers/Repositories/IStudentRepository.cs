using DotnetApiPractice.DTOs;

namespace DotnetApiPractice.Repositories;

public interface IStudentRepository
{
    List<StudentResponseDto> GetAll();
    StudentResponseDto? GetById(int id);
    StudentResponseDto Add(StudentCreateDto dto);
    StudentResponseDto? Update(int id, StudentCreateDto dto);
    bool Delete(int id);
}