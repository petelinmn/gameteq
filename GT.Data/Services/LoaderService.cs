using GT.Data.DB;
using GT.Data.Model;

namespace GT.Data.Services;

public interface ILoaderService
{
    void AddCurrencies(IEnumerable<Currency> currencies);
}

public class LoaderService
{
    private ICurrencyContext CurrencyContext { get; }

    public LoaderService(ICurrencyContext currencyContext)
    {
        CurrencyContext = currencyContext ?? throw new ArgumentException(null, nameof(currencyContext));
    }

    async Task AddCurrencies(IEnumerable<Currency> currencies) =>
        await CurrencyContext?.Currencies?.AddRangeAsync(currencies)!;
}
