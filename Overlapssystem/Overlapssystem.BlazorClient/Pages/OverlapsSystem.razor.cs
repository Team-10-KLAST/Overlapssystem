using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages
{
    // Code-behind for the OverlapsSystem.razor page.
    public partial class OverlapsSystem
    {
        [Inject] private AuthService AuthService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IResidentSchemaDataService SchemaService { get; set; } = default!;

        private IEnumerable<ResidentSchemaDto>? schemas;

        protected override async Task OnInitializedAsync()
        {
            schemas = await SchemaService.GetAllAsync();
        }

        // Called when the user clicks the logout button
        private async Task HandleLogout()
        {
            await AuthService.LogoutAsync();
            Navigation.NavigateTo("/login");
        }
    }
}
