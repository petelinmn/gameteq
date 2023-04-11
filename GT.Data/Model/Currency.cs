namespace GT.Data.Model;

public class Currency
{
    public int CurrencyId { get; set; }
    public string? Name { get; set; }

    public List<Price>? Prices { get; set; }
}
