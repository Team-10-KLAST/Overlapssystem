using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.BlazorClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register authentication state provider so Blazor knows who is logged in
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<JwtAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

// Register the message handler that attaches the JWT token to HTTP requests
builder.Services.AddScoped<AuthorizationMessageHandler>();

// HttpClient configured to call our API. Automatically attaches the JWT token to requests
// via AuthorizationMessageHandler.
// Port 7292 matches the API's HTTPS port from launchSettings.json.
builder.Services.AddScoped(sp =>
{
    var js = sp.GetRequiredService<IJSRuntime>();
    var handler = new AuthorizationMessageHandler(js)
    {
        InnerHandler = new HttpClientHandler()
    };
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7292")
    };
});

// Register Blazor-side services for Dependency Injection 
builder.Services.AddScoped<IResidentSchemaDataService, ResidentSchemaDataService>();
builder.Services.AddScoped<ITrafficLightDataService, TrafficLightDataService>();
builder.Services.AddScoped<IResidentStatusDataService, ResidentStatusDataService>();
builder.Services.AddScoped<IDepartmentNoteDataService, DepartmentNoteDataService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ISaveShiftService, SaveShiftService>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IMedicationDataService, MedicationDataService>();
builder.Services.AddScoped<IPNDataService, PNDataService>();
builder.Services.AddScoped<IEmployeeAssignmentDataService, EmployeeAssignmentDataService>();
builder.Services.AddScoped<IEmployeeDataService, EmployeeDataService>();
builder.Services.AddScoped<IActivityLogDataService, ActivityLogDataService>();


await builder.Build().RunAsync();