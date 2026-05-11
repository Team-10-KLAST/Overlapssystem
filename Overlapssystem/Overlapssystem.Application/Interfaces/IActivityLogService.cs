using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Service interface for fetching audit data used in the activity log.
// Returns raw entities — mapping to DTOs happens in the API layer (Api/Mappers/).
public interface IActivityLogService
{
    Task<List<MedicationRegistration>> GetMedicationLogAsync(
        int employeeId, DateTime from, DateTime to);

    Task<List<ResidentSchema>> GetResidentSchemaLogAsync(
        int employeeId, DateTime from, DateTime to);
}