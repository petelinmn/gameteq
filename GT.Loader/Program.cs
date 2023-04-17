using System.Globalization;
using GT.Data.DB;
using GT.Data.Model;
using GT.Data.Services;
using GT.Data.Services.Impl;
using GT.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GT.Loader
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            await new ServiceCollection()
                .AddSingleton(configuration.GetSection("Database").Get<DatabaseOptions>()!)
                .AddSingleton(configuration.GetSection("Source").Get<SourceOptions>()!)
                .AddDbContext<ICurrencyContext, CurrencyContext>()
                .AddTransient<ILoaderService, LoaderService>()
                .AddSingleton<Executor>()
                .BuildServiceProvider()
                .GetService<Executor>()
                ?.Execute(args.Length > 0 ? int.Parse(args[0]) : null)!;
        }
    }
}
