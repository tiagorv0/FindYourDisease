using FindYourDisease.Users.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FindYourDisease.Users.Infra.Repository;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.Now;
                entry.Property(x => x.UpdateAt).CurrentValue = DateTime.Now;
                entry.Property(x => x.Active).CurrentValue = true;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("Updated").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Property("Updated").CurrentValue = DateTime.Now;
                entry.Property(x => x.Active).CurrentValue = false;
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }
}
