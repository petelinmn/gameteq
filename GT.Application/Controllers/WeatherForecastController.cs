using GT.Data.Model;
using GT.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GT.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDataReaderService _dataReaderService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
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

    [HttpGet]
    [Route("price")]
    public async Task<Price?> GetPrice(int currencyId, DateTime date) =>
        await _dataReaderService.GetPrice(currencyId, date);
}
