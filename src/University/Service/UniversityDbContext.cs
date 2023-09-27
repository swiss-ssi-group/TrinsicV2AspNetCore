using Microsoft.EntityFrameworkCore;

namespace University.Service;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

    public DbSet<Diploma> DriverLicenses => Set<Diploma>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Diploma>().HasKey(m => m.Id);

        base.OnModelCreating(builder);
    }
}
