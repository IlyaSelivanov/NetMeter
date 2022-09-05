using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment;
if(env.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("InMemoryDb");
    });
}
else if (env.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
       options.UseSqlServer(builder.Configuration.GetConnectionString("SettingsDb"));
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();