using Microsoft.Extensions.Logging.EventLog;
using Movie.Repository;
using Movie.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder
        .AddDebug()
        .AddEventLog(new EventLogSettings()
        {
            SourceName = "eAuctionLogSource",
            LogName = "eAuctionErrorLog",
            Filter = (x, y) => y >= LogLevel.Error
        });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
