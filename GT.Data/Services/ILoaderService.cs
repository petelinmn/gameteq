using GT.Data.Model;

namespace GT.Data.Services;

public interface ILoaderService
{
    /// <summary>
    /// Adding collection of currencies if they don't exist
    /// </summary>
    /// <param name="currencies">Collection of currencies</param>
    /// <returns>Collection of added currencies with new Ids</returns>
    Task<IEnumerable<Currency>?> AddCurrencies(IEnumerable<string> currencies);

    /// <summary>
    /// Add collection of prices
    /// </summary>
    /// <param name="prices">Collection of prices</param>
    /// <returns></returns>
    Task AddPrices(IEnumerable<Price> prices);
}
