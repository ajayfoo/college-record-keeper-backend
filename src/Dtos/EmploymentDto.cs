using CRK.Models;

namespace CRK.Dtos;

public class EmploymentDto()
{
    public bool IsEmployed { get; set; }
    public Guid? CompanyId { get; set; }
    public double? Salary { get; set; }
    public DateTime? TenureStart { get; set; }
    public DateTime? TenureEnd { get; set; }

    public Employment ToEmployment()
    {
        return new()
        {
            IsEmployed = IsEmployed,
            CompanyId = CompanyId,
            Salary = Salary,
            TenureStart = TenureStart,
            TenureEnd = TenureEnd,
            Id = Guid.NewGuid(),
        };
    }
}
