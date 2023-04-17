using System.Globalization;
using GT.Data.Model;
using GT.Data.Services;

namespace GT.Loader;

public class Executor
{
    private readonly ILoaderService _loaderService;
    private readonly SourceOptions _sourceOptions;

    public Executor(ILoaderService loaderService, SourceOptions sourceOptions)
    {
        _loaderService = loaderService ?? throw new ArgumentNullException(nameof(loaderService));
        _sourceOptions = sourceOptions ?? throw new ArgumentNullException(nameof(sourceOptions));
    }

    public async Task Execute(int? year = null)
    {
        using var httpClient = new HttpClient();
        year ??= DateTime.Now.Year;
        var url = $"{_sourceOptions.Url}?year={year}";
        var response = await httpClient.GetAsync(url);

        var responseContent = await response.Content.ReadAsStringAsync();

        var lineBlocks = responseContent
            .Split("Date|").Where(i => !string.IsNullOrEmpty(i))
            .ToArray();

        foreach (var lineBlock in lineBlocks)
        {
            await ParseLinesBlock(lineBlock);
        }
    }

    private async Task ParseLinesBlock(string linesBlock)
    {
        var lines = linesBlock.Split("\n", StringSplitOptions.TrimEntries)
            .Where(line => !string.IsNullOrEmpty(line.Trim()))
            .ToArray();
        var index = 0;
        var currenciesAmount = lines
            .First()
            .Split("|")
            .Select(i => i.Split(" "))
            .ToDictionary(i => index++, i => (CurrencyName: i[1], Amount: int.Parse(i[0])));

        var currenciesToAdd =
            currenciesAmount.Select(i => i.Value.CurrencyName);
        var actualCurrencies = await _loaderService.AddCurrencies(currenciesToAdd);
        var actualCurrenciesDict = actualCurrencies
            ?.ToDictionary(currency => currency.Name, currency => currency);

        var prices = lines
            .Skip(1)
            .Select(line =>
            {
                index = 0;
                return line.Split('|');
            })
            .Select(
                splitLine => splitLine.Skip(1).Select(i =>
                {
                    try
                    {
                        var (currencyName, amount) = currenciesAmount[index++];
                        var rawPrice = decimal.Parse(i);
                        var price = rawPrice / amount;
                        var currencyId = actualCurrenciesDict?[currencyName].Id;
                        return new Price
                        {
                            Date = DateTime.ParseExact(splitLine[0], "dd.MM.yyyy",
                                CultureInfo.InvariantCulture),
                            Value = price,
                            CurrencyId = currencyId.Value
                        };
                    }
                    catch (Exception e)
                    {
                        return new Price();
                    }
                }).ToList()).ToList();

        foreach (var pricesBatch in prices)
        {
            await _loaderService.AddPrices(pricesBatch);
        }
    }
}