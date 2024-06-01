using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRK.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class AchievementTypeController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<AchievementType> GetAchievementType(Guid id)
    {
        AchievementType? achievementType = _context.AchievementTypes.Find(id);
        return (achievementType == null) ? NotFound() : achievementType;
    }

    [HttpPost]
    public async Task<ActionResult<AchievementType>> AddAchievementType(
        AchievementType achievementType
    )
    {
        _ = _context.AchievementTypes.Add(achievementType);
        _ = await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(AddAchievementType),
            new { id = achievementType.Id },
            achievementType
        );
    }
}
