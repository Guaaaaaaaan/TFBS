using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Data.Entities;
using TFBS.Dtos.Reservations;
using TFBS.Exceptions;
using TFBS.Services.Interfaces;

namespace TFBS.Services;

public class ReservationService : IReservationService
{
    private readonly TfbsContext _db;

    public ReservationService(TfbsContext db)
    {
        _db = db;
    }

    public async Task<CreateReservationResponse> CreateAsync(CreateReservationRequest request)
    {
        if (!await _db.Departments.AnyAsync(d => d.DeptId == request.DeptId))
            throw new NotFoundException("DEPARTMENT_NOT_FOUND", "Department not found.");

        if (!await _db.Faculties.AnyAsync(f => f.FacultyId == request.FacultyId))
            throw new NotFoundException("FACULTY_NOT_FOUND", "Faculty not found.");

        if (!await _db.VehicleTypes.AnyAsync(t => t.TypeId == request.RequiredTypeId))
            throw new NotFoundException("VEHICLE_TYPE_NOT_FOUND", "Vehicle type not found.");

        var facultyInDept = await _db.Faculties.AnyAsync(f =>
            f.FacultyId == request.FacultyId && f.DeptId == request.DeptId);

        if (!facultyInDept)
            throw new ApiException(
                409,
                "FACULTY_DEPARTMENT_MISMATCH",
                "Faculty does not belong to the specified department."
            );

        var entity = new Reservation
        {
            DeptId = request.DeptId,
            FacultyId = request.FacultyId,
            RequiredTypeId = request.RequiredTypeId,
            ExpectedDepartureDate = DateOnly.FromDateTime(request.ExpectedDepartureDate),
            Destination = request.Destination
        };

        _db.Reservations.Add(entity);
        await _db.SaveChangesAsync();

        return new CreateReservationResponse { ReservationId = entity.ReservationId };
    }

}
