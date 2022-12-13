using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Domain;
using NMeter.Api.Reporting.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NMeterDB"))
);

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("RedisUrl")
);

builder.Services.AddTransient<IResultRepository, ResultRepository>();
builder.Services.AddTransient<IHashProvider, SHA256HashProvider>();
builder.Services.AddTransient<IResultDomain, ResultDomain>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
