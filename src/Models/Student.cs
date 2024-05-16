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
    public DateTime Dob
    {
        get { return _dob; }
        set { _dob = value.ToUniversalTime(); }
    }
    private DateTime _dob;

    [NotNull]
    public DateTime YearOfAdmission
    {
        get { return _yearOfAdmission; }
        set { _yearOfAdmission = value.ToUniversalTime(); }
    }
    private DateTime _yearOfAdmission;

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
