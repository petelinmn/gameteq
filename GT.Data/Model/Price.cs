namespace GT.Data.Model;

public class Price
{
    public int PriceId { get; set; }
    public decimal Value { get; set; }

    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
}
