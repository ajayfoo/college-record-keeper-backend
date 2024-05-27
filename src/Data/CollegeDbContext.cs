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
    public DbSet<Company> Companies { get; set; } = null!;
}
