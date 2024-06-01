using CRK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRK.Data;

public class CollegeDbContext(DbContextOptions<CollegeDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<AchievementType> AchievementTypes { get; set; } = null!;
    public DbSet<AchievementLevel> AchievementLevels { get; set; } = null!;
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        _ = builder
            .Entity<AchievementType>()
            .HasData(
                [
                    new() { Id = Guid.NewGuid(), Label = "Co-curricular" },
                    new() { Id = Guid.NewGuid(), Label = "Extra curricular" },
                    new() { Id = Guid.NewGuid(), Label = "Sports" },
                ]
            );
        _ = builder
            .Entity<AchievementLevel>()
            .HasData(
                [
                    new() { Id = Guid.NewGuid(), Name = "International" },
                    new() { Id = Guid.NewGuid(), Name = "National" },
                    new() { Id = Guid.NewGuid(), Name = "District" },
                    new() { Id = Guid.NewGuid(), Name = "College" },
                ]
            );
        _ = builder
            .Entity<Company>()
            .HasData(
                [
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Valve",
                        MiniumSalary = 90000,
                        MaximumSalary = 150000
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Discord",
                        MiniumSalary = 70000,
                        MaximumSalary = 110000
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "TATA",
                        MiniumSalary = 50000,
                        MaximumSalary = 100000
                    },
                ]
            );
    }
}
