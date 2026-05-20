using Overlapssystem.Api;
using Overlapssystem.Api.Seed;
using Overlapssystem.Application;
using Overlapssystem.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Register services in the DI container in each layer's DependencyInjection class.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

await IdentitySeeder.SeedAsync(app.Services);
if (app.Environment.IsDevelopment())
    await EmployeeSeeder.SeedAsync(app.Services);
await PhoneSeeder.SeedAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();