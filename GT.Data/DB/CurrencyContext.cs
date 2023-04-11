using GT.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace GT.Data.DB;

public class CurrencyContext : DbContext, ICurrencyContext
{
    public DbSet<Currency>? Currencies { get; set; }
    public DbSet<Price>? Prices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"c:\gameteq.db;Version=3");
    }
}

public interface ICurrencyContext : IDisposable {
    DbSet<Currency>? Currencies { get; set; }
    DbSet<Price>? Prices { get; set; }

    int SaveChanges();
}
