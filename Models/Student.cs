using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRK.Models;

public class Student
{
    [NotNull]
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }

    [NotNull]
    public required string LastName { get; set; }

    [NotNull]
    public double CetPercentile { get; set; }

    [NotNull]
    public double HscPercentage { get; set; }

    [NotNull]
    public double SscPercentage { get; set; }

    [NotNull]
    public DateTime Dob { get; set; }

    [NotNull]
    public DateTime YearOfAdmission { get; set; }

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
