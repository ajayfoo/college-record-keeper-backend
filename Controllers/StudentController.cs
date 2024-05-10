using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public Student? GetStudent(Guid id)
    {
        return _context.Students.Find(id);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> AddStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddStudent), new { id = student.Id }, student);
    }
}
