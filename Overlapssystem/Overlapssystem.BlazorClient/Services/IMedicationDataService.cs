using Overlapssystem.BlazorClient.Models;

namespace Overlapssystem.BlazorClient.Services;

// Interface for communicating with the Medication API endpoint.
public interface IMedicationDataService
{
    // Marks a planned medication time as given in a specific shift.
    Task<Result> MarkAsGivenAsync(int residentSchemaId, int medicationTimeId);
}
