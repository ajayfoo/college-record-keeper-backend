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
    public ActionResult<Student> GetStudent(Guid id)
    {
        if (_context.Students == null) return NotFound();
        Student? student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }
        return student;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> AddStudent(Student student)
    {
        student.Dob = student.Dob.ToUniversalTime();
        student.YearOfAdmission = student.YearOfAdmission.ToUniversalTime();
        if (_context.Students == null) return NotFound();
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddStudent), new { id = student.Id }, student);
    }
}
