using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Api.Mappers;

// Responsible for mapping ResidentSchema entities to DTOs.
public static class ResidentSchemaMapper
{
    // Maps a ResidentSchema entity to a ResidentSchemaDto,
    // including related resident and medication information for display purposes.
    public static ResidentSchemaDto ToDto(ResidentSchema s)
    {
        return new ResidentSchemaDto
        {
            ResidentSchemaId = s.ResidentSchemaId,
            Initials = s.Resident.Initials,
            RelativeContact = s.RelativeContact,
            Status = s.Status,
            TrafficLight = s.TrafficLight?.ToString(),
            ShoppingDay = s.Resident.ShoppingDay,
            PaymentMethod = ToDanish(s.Resident.PaymentMethod),
            MedicationTimes = MapMedicationTimes(s),
            SelectedPrimaryId = s.PrimaryEmployeeId,
            SelectedSecondaryId = s.SecondaryEmployeeId,
        };
    }

    // Maps MedicationTimes for a resident to MedicationTimeDtos,
    // enriched with administration status from MedicationRegistrations in the current shift.
    private static List<MedicationTimeDto> MapMedicationTimes(ResidentSchema s)
    {
        return s.Resident.MedicationTimes.Select(mt => new MedicationTimeDto
        {
            MedicationTimeId = mt.MedicationTimeId,
            PlannedTime = mt.PlannedTime.ToString("HH:mm"),
            IsGiven = s.MedicationRegistrations
                .Any(mr => mr.MedicationTimeId == mt.MedicationTimeId && mr.Given),
            GivenTime = s.MedicationRegistrations
                .FirstOrDefault(mr => mr.MedicationTimeId == mt.MedicationTimeId && mr.Given)
                ?.GivenTime
        }).ToList();
    }

    // Translates paymentmethod enum values to danish for display in UI. 
    public static string ToDanish(PaymentMethod value) => value switch
    {
        PaymentMethod.OwnCard => "Eget kort",
        PaymentMethod.PCard => "P-kort",
        PaymentMethod.Cash => "Kontant",
        _ => "Ukendt"
    };
}
