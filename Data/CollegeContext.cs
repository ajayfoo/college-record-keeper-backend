using CRK.Models;
using Microsoft.EntityFrameworkCore;

namespace CRK.Data;

public class CollegeContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=college;Username=ajayk;Password=password"
        );
}
