using CRK.Models;

namespace CRK.Dtos;

public class AchievementDto()
{
    public string Name { get; set; } = null!;
    public string Prize { get; set; } = null!;
    public DateTime Date { get; set; }
    public Guid AchievementLevelId { get; set; }
    public Guid AchievementTypeId { get; set; }

    public Achievement ToAchievement()
    {
        return new Achievement
        {
            Name = Name,
            Prize = Prize,
            Date = Date.ToUniversalTime(),
            Id = Guid.NewGuid(),
            Inserted = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
            AchievementTypeId = AchievementTypeId,
            AchievementLevelId = AchievementLevelId,
        };
    }
}
