using GT.Data.DB;
using GT.Data.Services;
using GT.Data.Services.Impl;
using GT.Data.Settings;

var builder = WebApplication.CreateBuilder(args);

var databaseSettings = builder.Configuration.GetSection("Database").Get<DatabaseOptions>()!;
builder.Services.AddSingleton(databaseSettings);
builder.Services.AddDbContext<ICurrencyContext, CurrencyContext>();
builder.Services.AddTransient<IDataReaderService, DataReaderService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UsePathBase("/");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
