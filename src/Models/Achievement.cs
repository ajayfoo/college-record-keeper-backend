using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class Achievement
{
    public string Name { get; set; } = null!;
    public string Prize { get; set; } = null!;
    public DateTime Date { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }

    public AchievementLevel AchievementLevel { get; set; } = null!;
    public Guid AchievementLevelId { get; set; }

    public AchievementType AchievementType { get; set; } = null!;
    public Guid AchievementTypeId { get; set; }

    public Guid StudentId { get; set; }

    public Student Student { get; set; } = null!;
}
