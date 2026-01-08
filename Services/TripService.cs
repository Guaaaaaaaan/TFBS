using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Data.Entities;
using TFBS.Dtos.Trips;
using TFBS.Exceptions;
using TFBS.Services.Interfaces;

namespace TFBS.Services;

public class TripService : ITripService
{
    private readonly TfbsContext _db;

    public TripService(TfbsContext db)
    {
        _db = db;
    }

    public async Task<CreateTripResponse> CreateAsync(CreateTripRequest request)
    {
        if (request.StartOdometer < 0)
            throw new BadRequestException("INVALID_ODOMETER", "StartOdometer must be >= 0.");

        var reservation = await _db.Reservations.AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReservationId == request.ReservationId);

        if (reservation == null)
            throw new NotFoundException("RESERVATION_NOT_FOUND", "Reservation not found.");

        if (await _db.Trips.AnyAsync(t => t.ReservationId == request.ReservationId))
            throw new ApiException(409, "TRIP_ALREADY_EXISTS", "This reservation already has a trip.");

        if (reservation.FacultyId != request.FacultyId)
            throw new ApiException(409, "FACULTY_MISMATCH", "Faculty does not match reservation.");

        var vehicle = await _db.Vehicles.AsNoTracking()
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId);

        if (vehicle == null)
            throw new NotFoundException("VEHICLE_NOT_FOUND", "Vehicle not found.");

        if (vehicle.TypeId != reservation.RequiredTypeId)
            throw new ApiException(409, "VEHICLE_TYPE_MISMATCH", "Vehicle type does not match reservation.");

        var entity = new Trip
        {
            ReservationId = request.ReservationId,
            VehicleId = request.VehicleId,
            FacultyId = request.FacultyId,
            StartOdometer = request.StartOdometer,
            EndOdometer = request.StartOdometer
        };

        _db.Trips.Add(entity);
        await _db.SaveChangesAsync();

        return new CreateTripResponse { TripId = entity.TripId };
    }

    public async Task<CompleteTripResponse> CompleteAsync(int tripId, CompleteTripRequest request)
    {
        if (request.EndOdometer < 0)
            throw new ArgumentException("EndOdometer must be >= 0.");

        if (request.FuelGallons is not null && request.FuelGallons < 0)
            throw new ArgumentException("FuelGallons must be >= 0.");

        // 题目语义：买油了就应该有信用卡号
        if (request.FuelGallons is not null && request.FuelGallons > 0 &&
            string.IsNullOrWhiteSpace(request.CreditCardNo))
            throw new ArgumentException("CreditCardNo is required when fuel is purchased.");

        var trip = await _db.Trips.FirstOrDefaultAsync(t => t.TripId == tripId);
        if (trip == null)
            throw new ArgumentException("Invalid TripId.");

        if (request.EndOdometer < trip.StartOdometer)
            throw new ArgumentException("EndOdometer must be >= StartOdometer.");

        trip.EndOdometer = request.EndOdometer;
        trip.FuelGallons = request.FuelGallons;
        trip.CreditCardNo = string.IsNullOrWhiteSpace(request.CreditCardNo) ? null : request.CreditCardNo;
        trip.MaintenanceComplaint = string.IsNullOrWhiteSpace(request.MaintenanceComplaint) ? null : request.MaintenanceComplaint;

        await _db.SaveChangesAsync();

        return new CompleteTripResponse
        {
            TripId = trip.TripId,
            MilesDriven = trip.EndOdometer - trip.StartOdometer
        };
    }


}
