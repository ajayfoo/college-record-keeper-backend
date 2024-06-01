using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRK.Models;

public class AchievementLevel
{
    public string Name { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [JsonIgnore]
    public ICollection<Achievement> Achievements { get; } = new List<Achievement>();
}
