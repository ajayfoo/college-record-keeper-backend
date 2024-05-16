using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController]
[Route("[controller]")]
public class AchievementController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<Achievement> GetAchievement(Guid id)
    {
        Achievement? achievement = _context.Achievements.Find(id);
        if (achievement == null)
        {
            return NotFound();
        }
        return achievement;
    }

    [HttpPost]
    public async Task<ActionResult<Achievement>> AddAchievement(Achievement achievement)
    {
        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddAchievement), new { id = achievement.Id }, achievement);
    }
}
