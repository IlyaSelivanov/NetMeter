using Microsoft.EntityFrameworkCore;
using NMeter.Api.Reporting.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NMeterDB"))
);

var app = builder.Build();

await app.RunAsync();
