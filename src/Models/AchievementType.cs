using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class AchievementType
{
    public string Label { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<Achievement> Achievements { get; } = new List<Achievement>();
}
