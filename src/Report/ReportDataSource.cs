namespace CRK.Report;

public static class ReportDataSource
{
    public static ReportModel GetReportDetails()
    {
        return GetDummyReportDetails();
    }

    private static ReportModel GetDummyReportDetails()
    {
        return new()
        {
            Student = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ajay",
                MiddleName = "Jill",
                LastName = "Foo",
                CetPercentile = 99.999,
                HscPercentage = 85.0,
                SscPercentage = 90.2,
                DateOfBirth = DateTime.UtcNow,
                YearOfAdmission = 2022,
                AcademicScore = 9,
                Employment = new()
                {
                    IsEmployed = true,
                    CompanyId = Guid.NewGuid(),
                    Company = new()
                    {
                        Name = "Valve",
                        MiniumSalary = 90000,
                        MaximumSalary = 150000,
                        Year = 2023,
                    },
                    TenureStart = DateTime.UtcNow,
                    TenureEnd = DateTime.UtcNow,
                },
                Achievements =
                [
                    new()
                    {
                        Name = "Chess Champ",
                        Prize = "Golden Trophy",
                        Date = DateTime.UtcNow,
                        AchievementLevel = new() { Name = "International" },
                        AchievementType = new() { Label = "Sports" }
                    },
                    new()
                    {
                        Name = "Best Speech",
                        Prize = "10000 INR",
                        Date = DateTime.UtcNow,
                        AchievementLevel = new() { Name = "College" },
                        AchievementType = new() { Label = "Extra-curricular" }
                    }
                ]
            }
        };
    }
}
