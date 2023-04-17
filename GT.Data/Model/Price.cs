namespace GT.Data.Model;

public class Price
{
    public int Id { get; set; }
    public decimal Value { get; set; }

    public DateTime Date { get; set; }

    public int CurrencyId { get; set; }
}
