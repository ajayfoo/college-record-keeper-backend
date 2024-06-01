using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRK.Models;

public class AchievementType
{
    public string Label { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [JsonIgnore]
    public ICollection<Achievement> Achievements { get; } = new List<Achievement>();
}
