using CRK.Models;

namespace CRK.Dtos;

public class StudentDto
{
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public double CetPercentile { get; set; }
    public double HscPercentage { get; set; }
    public double SscPercentage { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int YearOfAdmission { get; set; }
    public int? AcademicScore { get; set; }
    public AchievementDto AchievementDto { get; set; } = null!;
    public EmploymentDto EmploymentDto { get; set; } = null!;

    public Student ToStudent()
    {
        return new()
        {
            FirstName = FirstName,
            MiddleName = MiddleName,
            LastName = LastName,
            CetPercentile = CetPercentile,
            HscPercentage = HscPercentage,
            SscPercentage = SscPercentage,
            DateOfBirth = DateOfBirth.ToUniversalTime(),
            YearOfAdmission = YearOfAdmission,
            AcademicScore = AcademicScore,
            Employment = EmploymentDto.ToEmployment(),
            Id = Guid.NewGuid(),
            Inserted = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };
    }
}
