using Serilog;
using SeriLogSamples;
string myName = "Mariam Shindy";
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
     .Enrich.WithProperty("UserName", myName)
     .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {UserName}: {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("logs/inventory-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Inventory Management Application Starting up");
    var inventory = new Inventory(myName);
    inventory.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
        