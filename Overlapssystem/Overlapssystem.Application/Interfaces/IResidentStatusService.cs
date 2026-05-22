namespace Overlapssystem.Application.Interfaces;

// contract for updating a residents status text 
public interface IResidentStatusService
{
    Task<bool> UpdateStatusAsync(int residentSchemaId, string newStatus, int employeeId);
}
