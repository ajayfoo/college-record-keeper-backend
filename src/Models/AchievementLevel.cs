using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class AchievementLevel
{
    public string Name { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<Achievement> Achievements { get; } = new List<Achievement>();
}
