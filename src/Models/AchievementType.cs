using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRK.Models;

public class AchievementType
{
    [NotNull]
    public string? Label { get; set; }

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<Achievement> Achievements { get; } = new List<Achievement>();

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }
}
