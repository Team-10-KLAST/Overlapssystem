using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Overlapssystem.BlazorClient;
using Overlapssystem.BlazorClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient configured to call our API.
// Port 7292 matches the API's HTTPS port from launchSettings.json.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7292") });

// REgister Blazor-side services for Dependency Injection 
builder.Services.AddScoped<IResidentSchemaDataService, ResidentSchemaDataService>();
await builder.Build().RunAsync();
