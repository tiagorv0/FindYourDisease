using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindYourDisease.Patient.Infra.Repository.Mapping;

public class PatientMapping : IEntityTypeConfiguration<Domain.Model.Patient>
{
    public void Configure(EntityTypeBuilder<Domain.Model.Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasMaxLength(400);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Phone).HasMaxLength(25);
        builder.Property(p => p.HashedPassword).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Photo).IsRequired().HasMaxLength(255);
        builder.Property(p => p.BirthDate).IsRequired();
        builder.Property(p => p.City).IsRequired().HasMaxLength(100);
        builder.Property(p => p.State).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Country).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Active).IsRequired().HasColumnType("int");

    }
}
