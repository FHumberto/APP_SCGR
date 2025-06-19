using Microsoft.EntityFrameworkCore;
using SCGR.Domain.Entities.Categories;
using SCGR.Domain.Entities.Transactions;

namespace SCGR.Infrastructure.Persistence.Contexts;

public sealed class ScgrDbContext(DbContextOptions<ScgrDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScgrDbContext).Assembly);
    }
}