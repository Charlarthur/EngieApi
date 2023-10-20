using EngieApi.Extensions;
using EngieApi.Logging;
using EngieApi.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval : RollingInterval.Minute)
    .CreateLogger();


try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<ILogging, Logging>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<RoutingMiddleware>();
    app.ConfigureExceptionHandler(app.Logger);
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unexpected error");
}
finally
{
    Log.CloseAndFlush();
}