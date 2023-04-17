namespace GT.Data.Model;

public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Price>? Prices { get; set; }
}
