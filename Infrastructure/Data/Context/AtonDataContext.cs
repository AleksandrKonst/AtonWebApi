using Domain.Entity;
using Infrastructure.Data.Context.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class AtonDataContext : DbContext
{
    public AtonDataContext()
    {
    }

    public AtonDataContext(DbContextOptions<AtonDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_ROUTE") ?? "Server=localhost;Port=5432;Database=Aton_Data;User Id=postgres;Password=1243");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
