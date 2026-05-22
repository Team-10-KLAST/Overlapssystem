using Overlapssystem.Api;
using Overlapssystem.Api.Seed;
using Overlapssystem.Application;
using Overlapssystem.Infrastructure;


// Entry point of the application. Configures services, middleware, and runs the web application.
// Configures services, middleware, and seed initial data on startup.
var builder = WebApplication.CreateBuilder(args);

// Register services in the DI container in each layer's DependencyInjection class.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

// Seed initial data on startup.
await IdentitySeeder.SeedAsync(app.Services);
if (app.Environment.IsDevelopment())
    await EmployeeSeeder.SeedAsync(app.Services);
await PhoneSeeder.SeedAsync(app.Services);

// Configure midlleware pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Der opstod en uventet fejl. Prøv igen senere."
            });
        });
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();