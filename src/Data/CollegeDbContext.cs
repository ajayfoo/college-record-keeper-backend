using CRK.Models;
using Microsoft.EntityFrameworkCore;

namespace CRK.Data;

public class CollegeDbContext(DbContextOptions<CollegeDbContext> options) : DbContext(options)
{
    public DbSet<Student>? Students { get; set; }
    public DbSet<AchievementType>? AchievementTypes { get; set; }
}
