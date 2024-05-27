using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class AchievementLevel
{
    public string Name { get; set; } = "State";

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
