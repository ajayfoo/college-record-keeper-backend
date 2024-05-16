using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRK.Models;

public class Achievement
{
    [NotNull]
    public required string Name { get; set; }

    [NotNull]
    public required string Level { get; set; }

    public required Guid AchievementTypeId { get; set; }

    [NotNull]
    public required string Prize { get; set; }

    [NotNull]
    public required DateTime Year
    {
        get { return _year; }
        set { _year = value.ToUniversalTime(); }
    }
    private DateTime _year;

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
