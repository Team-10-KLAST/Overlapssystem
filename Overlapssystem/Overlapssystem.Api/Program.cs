using Microsoft.EntityFrameworkCore;
using Overlapssystem.Infrastructure.Data;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services for Dependency Injection 
// "Scoped" because one instance per HTTP request matches DbContext lifetime. 
builder.Services.AddScoped<IResidentSchemaService, ResidentSchemaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins(
                    "https://localhost:7262",
                    "http://localhost:5172"
                    )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseAuthorization();

app.MapControllers();

app.Run();
