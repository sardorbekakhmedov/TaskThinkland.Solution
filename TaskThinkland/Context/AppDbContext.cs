using Microsoft.EntityFrameworkCore;
using TaskThinkland.Api.Entities;

namespace TaskThinkland.Api.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateTimeStampForBaseEntityClass();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimeStampForBaseEntityClass()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Entity is not BaseEntity entity)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreateAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.UpdatedAt = DateTime.UtcNow; 
                    break;
            }
        }
    }
}