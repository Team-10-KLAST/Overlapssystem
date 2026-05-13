using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.UseCases;

// Interface for the use case that orchestrates shift creation and schema initialization.
public interface ICreateShiftUseCase
{
    Task<Shift> ExecuteAsync(DateTime date, ShiftType shiftType, int departmentId);
}
