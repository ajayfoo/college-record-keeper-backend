using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRK.Models;

public class Company
{
    [NotNull]
    public required string Name { get; set; }

    [NotNull]
    public decimal MiniumSalary { get; set; }

    [NotNull]
    public decimal MaximumSalary { get; set; }

    [NotNull]
    public int Year { get; set; }

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }
}
