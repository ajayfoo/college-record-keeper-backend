using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRK.Models;

public class Achievement
{
    [NotNull]
    public string? Name { get; set; }

    [NotNull]
    public string? Level { get; set; }

    public Guid AchievementTypeId { get; set; }

    [NotNull]
    public string? Prize { get; set; }

    [NotNull]
    public DateTime Date
    {
        get => _date;
        set => _date = value.ToUniversalTime();
    }
    private DateTime _date;

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }
}
