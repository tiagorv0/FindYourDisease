using Microsoft.EntityFrameworkCore;

namespace FindYourDisease.Patient.Repository;

public class PatientDbContext : DbContext
{
    public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
    {
        
    }

    public DbSet<Model.Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientDbContext).Assembly);
    }
}
