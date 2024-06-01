using CRK.Data;
using CRK.Models;

namespace CRK.Dtos;

public class EmploymentDto(CollegeDbContext context)
{
    public bool IsEmployed { get; set; }
    public Guid? CompanyId { get; set; }
    public double? Salary { get; set; }
    public DateTime? TenureStart { get; set; }
    public DateTime? TenureEnd { get; set; }

    public Employment ToEmployment()
    {
        Company? company = null;
        if (IsEmployed)
        {
            company = context.Companies.First(c => c.Id == CompanyId);
        }
        return new()
        {
            IsEmployed = IsEmployed,
            CompanyId = CompanyId,
            Company = company,
            Salary = Salary,
            TenureStart = TenureStart,
            TenureEnd = TenureEnd,
            Id = Guid.NewGuid(),
        };
    }
}
