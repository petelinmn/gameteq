using GT.Data.Model;
using GT.Data.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GT.Data.DB;

public sealed class CurrencyContext : DbContext, ICurrencyContext
{
    public DbSet<Currency>? Currencies { get; set; }
    public DbSet<Price>? Prices { get; set; }

    private readonly DatabaseOptions _databaseOptions;

    public CurrencyContext(DatabaseOptions databaseOptions)
    {
        _databaseOptions = databaseOptions ?? throw new ArgumentException(null, nameof(databaseOptions));
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (_databaseOptions.Type)
        {
            case "Sqlite":
                optionsBuilder.UseSqlite(_databaseOptions.ConnectionString);
                break;
            default:
                throw new Exception("Database type is not specified or is not supported");
        }
    }
}

public interface ICurrencyContext : IDisposable {
    DbSet<Currency>? Currencies { get; set; }
    DbSet<Price>? Prices { get; set; }

    int SaveChanges();

    ChangeTracker ChangeTracker { get; }
}
