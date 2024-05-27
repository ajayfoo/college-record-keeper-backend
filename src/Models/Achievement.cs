using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class Achievement
{
    public string Name { get; set; } = null!;

    public AchievementLevel Level { get; set; } = null!;
    public Guid AchievementTypeId { get; set; }

    public string Prize { get; set; } = null!;
    public DateTime Date { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }
}
