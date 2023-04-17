using GT.Data.DB;
using GT.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace GT.Data.Services.Impl;

public class LoaderService : ILoaderService
{
    private readonly ICurrencyContext _dataContext;

    public LoaderService(ICurrencyContext currencyContext)
    {
        _dataContext = currencyContext ?? throw new ArgumentNullException(nameof(currencyContext));
    }

    public async Task<IEnumerable<Currency>?> AddCurrencies(IEnumerable<string> currencies)
    {
        if (_dataContext.Currencies is null)
            throw new ArgumentNullException(nameof(_dataContext.Currencies));

        var currenciesToAdd = currencies.ToArray();

        var existed = await _dataContext.Currencies
            .Where(c => currenciesToAdd.Any(currencyName => currencyName == c.Name))
            .ToListAsync();

        var newCurrencies = currenciesToAdd
            .Where(currencyName => existed.All(cur => cur.Name != currencyName))
            .Select(currencyName => new Currency { Name = currencyName })
            .ToList();

        await _dataContext.Currencies?.AddRangeAsync(newCurrencies)!;

        _dataContext.SaveChanges();

        return _dataContext.Currencies.Where(cur => currenciesToAdd.Any(currencyName => currencyName == cur.Name));
    }

    public async Task AddPrices(IEnumerable<Price> prices)
    {
        var existedPrices = (await _dataContext.Prices!.ToListAsync())
            .Where(price => prices.Any(newPrice => newPrice.CurrencyId == price.CurrencyId && newPrice.Date == price.Date))
            .ToList();

        var newPrices = prices
            .Where(price =>
                price.CurrencyId != 0 &&
                existedPrices.All(exPrice => exPrice.CurrencyId != price.CurrencyId || exPrice.Date != price.Date))
            .ToList();

        await _dataContext.Prices!.AddRangeAsync(newPrices);

        _dataContext.SaveChanges();
    }
}
