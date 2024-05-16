using CRK.Models;
using Microsoft.EntityFrameworkCore;

namespace CRK.Data;

public class CollegeDbContext(DbContextOptions<CollegeDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<AchievementType> AchievementTypes { get; set; } = null!;
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
}
