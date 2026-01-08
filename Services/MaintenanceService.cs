using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Data.Entities;
using TFBS.Dtos.Maintenance;
using TFBS.Exceptions;
using TFBS.Services.Interfaces;

namespace TFBS.Services;

public class MaintenanceService : IMaintenanceService
{
    private readonly TfbsContext _db;

    public MaintenanceService(TfbsContext db) => _db = db;

    public async Task<CreateMaintenanceLogResponse> CreateLogAsync(CreateMaintenanceLogRequest request)
    {
        // Validate vehicle existence
        var vehicleExists = await _db.Vehicles.AnyAsync(v => v.VehicleId == request.VehicleId);
        
        // Throw not found if vehicle does not exist
        if (!vehicleExists)
            throw new NotFoundException(
                "VEHICLE_NOT_FOUND",
                "Vehicle not found."
            );

        // Create new maintenance log
        var log = new MaintenanceLog
        {
            VehicleId = request.VehicleId,
            LogStartDate = request.LogStartDate,
            CompletionDate = null,
            ReleasedByMechanicId = null
        };

        // Add and save the new log
        _db.MaintenanceLogs.Add(log);
        // Save changes to generate LogId
        await _db.SaveChangesAsync();

        // Return response with new log ID
        return new CreateMaintenanceLogResponse { LogId = log.LogId };
    }

    // Complete maintenance log
    public async Task CompleteAsync(int logId, CompleteMaintenanceRequest request)
    {
        // Validate maintenance log existence
       var log = await _db.MaintenanceLogs.FirstOrDefaultAsync(x => x.LogId == logId);
        // Throw not found if log does not exist
        if (log == null)
            throw new NotFoundException(
                "MAINTENANCE_LOG_NOT_FOUND",
                "Maintenance log not found."
            );
        // Check if maintenance is already completed
        if (log.CompletionDate != null)
            throw new ApiException(
                409,
                "MAINTENANCE_ALREADY_COMPLETED",
                "Maintenance log already completed."
            );
        // Validate releaser mechanic existence
        var releaser = await _db.Mechanics.FirstOrDefaultAsync(m => m.MechanicId == request.ReleasedByMechanicId);

        // Throw not found if releaser mechanic does not exist
        if (releaser == null)
            throw new NotFoundException(
                "MECHANIC_NOT_FOUND",
                "ReleasedByMechanic not found."
            );

        // Check if releaser mechanic has inspection authorization
        if (!releaser.HasInspectionAuth)
            throw new ApiException(
                403,
                "MECHANIC_NOT_AUTHORIZED",
                "Mechanic does not have inspection authorization."
            );

        // Validate maintenance items
        if (request.Items.Count == 0)
            throw new BadRequestException(
                "NO_MAINTENANCE_ITEMS",
                "At least one maintenance item is required."
            );

        // Validate parts used
        foreach (var p in request.Parts)
        {
            if (p.QuantityUsed <= 0)
                throw new BadRequestException(
                    "INVALID_PART_QUANTITY",
                    "QuantityUsed must be greater than zero."
                );
        }

        // Begin transaction
        await using var tx = await _db.Database.BeginTransactionAsync();

        // Process maintenance items
        foreach (var item in request.Items)
        {
            // Validate maintenance item description
            if (string.IsNullOrWhiteSpace(item.Description))
                throw new BadRequestException(
                    "EMPTY_MAINTENANCE_DESCRIPTION",
                    "Maintenance item description is required."
                );

            // Validate performing mechanic existence
            var mechanicExists = await _db.Mechanics.AnyAsync(m => m.MechanicId == item.PerformedByMechanicId);

            // Throw not found if performing mechanic does not exist
            if (!mechanicExists)
                throw new NotFoundException(
                    "MECHANIC_NOT_FOUND",
                    $"Mechanic {item.PerformedByMechanicId} not found."
                );

            // Add maintenance detail
            _db.MaintenanceDetails.Add(new MaintenanceDetail
            {
                LogId = logId,
                Description = item.Description.Trim(),
                PerformedByMechanicId = item.PerformedByMechanicId
            });
        }

        // Process parts used
        foreach (var p in request.Parts)
        {
            // Validate part existence
            var part = await _db.Parts.FirstOrDefaultAsync(x => x.PartId == p.PartId);

            // Throw not found if part does not exist
            if (part == null)
                throw new NotFoundException(
                    "PART_NOT_FOUND",
                    $"Part {p.PartId} not found."
                );

            // Check for sufficient stock
            if (part.QtyOnHand < p.QuantityUsed)
                throw new ApiException(
                    409,
                    "INSUFFICIENT_PART_STOCK",
                    $"Insufficient stock for Part {p.PartId}."
                );

            // Deduct used quantity from stock
            part.QtyOnHand -= p.QuantityUsed;

            // Record part usage
            _db.PartUsages.Add(new PartUsage
            {
                LogId = logId,
                PartId = p.PartId,
                QuantityUsed = p.QuantityUsed
            });
        }

        // Update maintenance log as completed
        log.CompletionDate = request.CompletionDate;
        // Set releaser mechanic
        log.ReleasedByMechanicId = request.ReleasedByMechanicId;

        // Save all changes
        await _db.SaveChangesAsync();
        // Commit transaction
        await tx.CommitAsync();
    }
}
