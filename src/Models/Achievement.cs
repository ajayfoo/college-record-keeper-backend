using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class Achievement
{
    public string Name { get; set; } = null!;

    public AchievementLevel AchievementLevel { get; set; } = null!;
    public AchievementType AchievementType { get; set; } = null!;

    public string Prize { get; set; } = null!;
    public DateTime Date { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }
}
