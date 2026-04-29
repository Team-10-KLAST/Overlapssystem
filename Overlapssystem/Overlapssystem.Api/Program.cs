using Microsoft.EntityFrameworkCore;
using Overlapssystem.Infrastructure.Repositories.Data;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Services;
using Overlapssystem.BlazorClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy
            .WithOrigins("https://localhost:7262")
            .AllowAnyHeader()
            .AllowAnyMethod();
      });
  });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency injection for ShiftService, som håndterer forretningslogikken for vagter
builder.Services.AddScoped<IShiftService, ShiftService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("BlazorClient");

app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine("Unhandled exception: " + ex);
    throw;
}
