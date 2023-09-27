using Microsoft.EntityFrameworkCore;

namespace University.Service;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

    public DbSet<Diploma> Diplomas => Set<Diploma>();
    public DbSet<DiplomaTemplate> DiplomaTemplates => Set<DiplomaTemplate>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Diploma>().HasKey(m => m.Id);
        builder.Entity<DiplomaTemplate>().HasKey(m => m.Id);

        base.OnModelCreating(builder);
    }
}
