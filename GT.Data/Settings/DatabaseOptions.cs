namespace GT.Data.Settings;

public class DatabaseOptions
{
    public const string Database = "Database";

    public string Type { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}
