using GT.Data.Model;
using GT.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GT.Application.Controllers;

[ApiController]
[Route("home")]
public class CurrencyPriceController : ControllerBase
{
    private readonly ILogger<CurrencyPriceController> _logger;
    private readonly IDataReaderService _dataReaderService;

    public CurrencyPriceController(ILogger<CurrencyPriceController> logger,
        IDataReaderService dataReaderService)
    {
        _logger = logger;
        _dataReaderService = dataReaderService;
    }

    [HttpGet]
    public async Task<IEnumerable<Currency>> Get()
    {
        var currencies = await _dataReaderService.GetCurrencies();
        return currencies;
    }
}