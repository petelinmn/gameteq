using GT.Data.Model;
using GT.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GT.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ILogger<CurrencyController> _logger;
    private readonly IDataReaderService _dataReaderService;

    public CurrencyController(ILogger<CurrencyController> logger,
        IDataReaderService dataReaderService)
    {
        _logger = logger;
        _dataReaderService = dataReaderService;
    }

    [Route("currency")]
    [HttpGet]
    public async Task<Currency?> Get(int currencyId, DateTime date) =>
        await _dataReaderService.GetCurrency(currencyId, date);

    [HttpGet]
    [Route("all")]
    public async Task<IEnumerable<Currency>> GetAll() =>
        await _dataReaderService.GetCurrencies();
}
