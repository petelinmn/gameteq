using GT.Data.DB;
using GT.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace GT.Data.Services.Impl;


public class DataReaderService : IDataReaderService
{
    private readonly ICurrencyContext _dataContext;

    public DataReaderService(ICurrencyContext currencyContext)
    {
        _dataContext = currencyContext ?? throw new ArgumentNullException(nameof(currencyContext));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Currency>> GetCurrencies() =>
        await _dataContext.Currencies!.ToListAsync();

    /// <inheritdoc />
    public async Task<Currency?> GetCurrency(int currencyId, DateTime date)
    {
        return await _dataContext.Currencies!
            .Include(currency =>
                currency.Prices!
                    .Where(price => price.Date <= date)
                    .OrderByDescending(c => c.Date).Take(1))
            .FirstOrDefaultAsync(c => c.Id == currencyId);
    }
}
