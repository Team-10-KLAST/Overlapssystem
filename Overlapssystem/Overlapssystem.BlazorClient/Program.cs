using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Overlapssystem.BlazorClient;
using Overlapssystem.BlazorClient.Services; // Sørg for at denne using er her!
using Overlapssystem.Application.Services;
using Overlapssystem.Application.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7292/")
});

builder.Services.AddScoped<SaveShiftService>();

builder.Services.AddScoped<IShiftService, ShiftService>();

await builder.Build().RunAsync();