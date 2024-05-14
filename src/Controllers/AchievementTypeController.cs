using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController]
[Route("[controller]")]
public class AchievementTypeController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<AchievementType> GetAchievementType(Guid id)
    {
        if (_context.AchievementTypes == null) return NotFound();
        AchievementType? achievementType = _context.AchievementTypes.Find(id);
        if (achievementType == null)
        {
            return NotFound();
        }
        return achievementType;
    }

    [HttpPost]
    public async Task<ActionResult<AchievementType>> AddAchievementType(
        AchievementType achievementType
    )
    {
        if (_context.AchievementTypes == null) return NotFound();
        _context.AchievementTypes.Add(achievementType);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(AddAchievementType),
            new { id = achievementType.Id },
            achievementType
        );
    }
}
