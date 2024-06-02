using System.ComponentModel.DataAnnotations.Schema;

namespace CRK.Models;

public class Company
{
    public string Name { get; set; } = null!;
    public decimal MiniumSalary { get; set; }
    public decimal MaximumSalary { get; set; }
    public int Year { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<Employment> Posts { get; } = new List<Employment>();
}
