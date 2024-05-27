using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class Student
{
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public double CetPercentile { get; set; }
    public double HscPercentage { get; set; }
    public double SscPercentage { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime YearOfAdmission { get; set; }
    public int? AcademicScore { get; set; }
    public Achievement[]? Achievements { get; set; }
    public Employment? Employment { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime LastUpdated { get; set; }
}
