using CRK.Data;
using CRK.Dtos;
using CRK.Models;
using CRK.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CRK.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class StudentController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(Guid id)
    {
        Student? student = _context.Students.Find(id);
        return (student == null) ? NotFound() : student;
    }

    [HttpPost("filtered")]
    public async Task<ActionResult<IEnumerable<Student>>> GetFilteredStudent(
        [FromBody] JObject jobj
    )
    {
        dynamic obj = jobj;
        string firstName = obj.firstName;
        int year = obj.yearOfAdmission;
        return (firstName == "" && year == 0)
            ? await _context.Students.OrderByDescending(s => s.Inserted).Take(5).ToListAsync()
            : (firstName == "")
                ? await _context.Students.Where(s => s.YearOfAdmission == year).ToListAsync()
                : (year == 0)
                    ? await _context.Students.Where(s => s.FirstName == firstName).ToListAsync()
                    : await _context
                        .Students.Where(s => s.FirstName == firstName && s.YearOfAdmission == year)
                        .ToListAsync();
    }

    [HttpGet("yearsOfAdmission")]
    public async Task<ActionResult<IEnumerable<int>>> GetYearsOfAdmission()
    {
        return await _context.Students.Select(s => s.YearOfAdmission).Distinct().ToListAsync();
    }

    [HttpGet]
    [Route("latests")]
    public async Task<ActionResult<IEnumerable<Student>>> GetLatests()
    {
        return await _context.Students.OrderByDescending(s => s.Inserted).Take(5).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Student>> AddStudent(StudentDto studentDto)
    {
        Student newStudent = studentDto.ToStudent();
        newStudent.Id = Guid.NewGuid();

        Achievement achievement = studentDto.AchievementDto.ToAchievement();
        achievement.AchievementLevel = _context
            .AchievementLevels.Where(a => a.Id == achievement.AchievementLevelId)
            .Single();
        achievement.AchievementType = _context
            .AchievementTypes.Where(a => a.Id == achievement.AchievementTypeId)
            .Single();
        achievement.StudentId = newStudent.Id;
        achievement.Student = newStudent;

        newStudent.Achievements = [achievement];

        Employment employment = studentDto.EmploymentDto.ToEmployment();
        employment.Company = _context.Companies.Where(c => c.Id == employment.CompanyId).Single();
        employment.StudentId = newStudent.Id;
        employment.Student = newStudent;

        newStudent.Employment = employment;

        _ = _context.Students.Add(newStudent);
        _ = await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddStudent), new { id = newStudent.Id }, newStudent);
    }

    [HttpDelete("{id}")]
    public ActionResult<Student> DeleteStudent(Guid id)
    {
        int deletedCount = _context.Students.Where(student => student.Id == id).ExecuteDelete();
        return (deletedCount != 1) ? NotFound() : NoContent();
    }

    [HttpGet]
    [Route("report/{id}")]
    public ActionResult<Student> GetStudentReport(Guid id)
    {
        Student? student = _context.Students.Where(student => student.Id == id).SingleOrDefault();
        if (student == null)
        {
            return NotFound();
        }
        byte[] pdf = ReportGenerator.Generate(student);
        return File(pdf, "application/pdf", "hello-world.pdf");
    }
}
