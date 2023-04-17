using GT.Data.Model;

namespace GT.Data.Services;

public interface IDataReaderService
{
    Task<IEnumerable<Currency>> GetCurrencies();
    Task<Currency?> GetCurrency(int currencyId, DateTime date);
    Task<Price?> GetPrice(int currencyId, DateTime date);
}
