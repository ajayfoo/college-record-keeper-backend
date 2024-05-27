using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class AchievementLevelController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<AchievementLevel> GetAchievementLevel(Guid id)
    {
        AchievementLevel? achievementLevel = _context.AchievementLevels.Find(id);
        return (achievementLevel == null) ? NotFound() : achievementLevel;
    }

    [HttpPost]
    public async Task<ActionResult<AchievementLevel>> AddAchievementLevel(
        AchievementLevel achievementLevel
    )
    {
        _ = _context.AchievementLevels.Add(achievementLevel);
        _ = await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(AddAchievementLevel),
            new { id = achievementLevel.Id },
            achievementLevel
        );
    }
}
