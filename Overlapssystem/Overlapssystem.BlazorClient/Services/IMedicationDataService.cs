using Overlapssystem.BlazorClient.Models;

namespace Overlapssystem.BlazorClient.Services;

// Interface for communicating with the Medication API endpoint.
public interface IMedicationDataService
{
    Task<Result> MarkAsGivenAsync(int residentSchemaId, int medicationTimeId);
}
