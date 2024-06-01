using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRK.Models;

public class Employment
{
    public bool IsEmployed { get; set; }
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
    public double? Salary { get; set; }
    public DateTime? TenureStart { get; set; }
    public DateTime? TenureEnd { get; set; }

    [NotNull]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
