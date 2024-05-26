using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class AchievementController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<Achievement> GetAchievement(Guid id)
    {
        Achievement? achievement = _context.Achievements.Find(id);
        return (achievement == null) ? NotFound() : achievement;
    }

    [HttpPost]
    public async Task<ActionResult<Achievement>> AddAchievement(Achievement achievement)
    {
        _ = _context.Achievements.Add(achievement);
        _ = await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddAchievement), new { id = achievement.Id }, achievement);
    }
}
