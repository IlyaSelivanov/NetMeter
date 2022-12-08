using Microsoft.EntityFrameworkCore;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.GraphQL;
using NMeter.Api.Reporting.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NMeterDB"))
);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.MapGraphQL();

await app.RunAsync();
