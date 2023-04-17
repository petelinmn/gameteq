using GT.Data.Model;

namespace GT.Data.Services;

public interface ILoaderService
{
    Task<IEnumerable<Currency>?> AddCurrencies(IEnumerable<string> currencies);
    Task AddPrices(IEnumerable<Price> prices);
}
