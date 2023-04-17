using GT.Data.Model;

namespace GT.Data.Services;

public interface IDataReaderService
{
    /// <summary>
    /// Retrieving all available currencies
    /// </summary>
    /// <returns>Collection of currencies</returns>
    Task<IEnumerable<Currency>> GetCurrencies();

    /// <summary>
    /// Retrieving currency and all prices by date
    /// </summary>
    /// <param name="currencyId">Id of currency in database</param>
    /// <param name="date">Date of price</param>
    /// <returns></returns>
    Task<Currency?> GetCurrency(int currencyId, DateTime date);
}
