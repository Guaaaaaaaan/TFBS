using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Dtos.Reports;
using TFBS.Exceptions;
using TFBS.Services.Interfaces;

namespace TFBS.Services;

public class ReportService : IReportService
{
    private readonly TfbsContext _db;

    public ReportService(TfbsContext db)
    {
        _db = db;
    }

    private static void ValidateYearMonth(int year, int month)
    {
        if (year < 2000 || year > 2100)
            throw new BadRequestException("INVALID_YEAR", "Year must be between 2000 and 2100.");

        if (month < 1 || month > 12)
            throw new BadRequestException("INVALID_MONTH", "Month must be between 1 and 12.");
    }


    public async Task<List<MileageByVehicleDto>> MileageByVehicleAsync(int year, int month)
    {
        ValidateYearMonth(year, month);

        var q =
            from t in _db.Trips.AsNoTracking()
            join r in _db.Reservations.AsNoTracking() on t.ReservationId equals r.ReservationId
            join v in _db.Vehicles.AsNoTracking() on t.VehicleId equals v.VehicleId
            where r.ExpectedDepartureDate.Year == year && r.ExpectedDepartureDate.Month == month
            select new
            {
                t.VehicleId,
                v.PlateNumber,
                Miles = t.EndOdometer - t.StartOdometer
            };

        return await q
            .GroupBy(x => new { x.VehicleId, x.PlateNumber })
            .Select(g => new MileageByVehicleDto
            {
                VehicleId = g.Key.VehicleId,
                PlateNumber = g.Key.PlateNumber,
                Trips = g.Count(),
                MilesDriven = g.Sum(x => x.Miles)
            })
            .OrderByDescending(x => x.MilesDriven)
            .ToListAsync();
    }

    public async Task<List<MileageByDepartmentDto>> MileageByDepartmentAsync(int year, int month)
    {
        ValidateYearMonth(year, month);

        var q =
            from t in _db.Trips.AsNoTracking()
            join r in _db.Reservations.AsNoTracking() on t.ReservationId equals r.ReservationId
            join d in _db.Departments.AsNoTracking() on r.DeptId equals d.DeptId
            where r.ExpectedDepartureDate.Year == year && r.ExpectedDepartureDate.Month == month
            select new
            {
                d.DeptId,
                d.DeptName,
                Miles = t.EndOdometer - t.StartOdometer
            };

        return await q
            .GroupBy(x => new { x.DeptId, x.DeptName })
            .Select(g => new MileageByDepartmentDto
            {
                DeptId = g.Key.DeptId,
                DeptName = g.Key.DeptName,
                Trips = g.Count(),
                MilesDriven = g.Sum(x => x.Miles)
            })
            .OrderByDescending(x => x.MilesDriven)
            .ToListAsync();
    }

    public async Task<List<RevenueByDepartmentDto>> RevenueByDepartmentAsync(int year, int month)
    {
        ValidateYearMonth(year, month);

        var q =
            from t in _db.Trips.AsNoTracking()
            join r in _db.Reservations.AsNoTracking() on t.ReservationId equals r.ReservationId
            join d in _db.Departments.AsNoTracking() on r.DeptId equals d.DeptId
            join v in _db.Vehicles.AsNoTracking() on t.VehicleId equals v.VehicleId
            join vt in _db.VehicleTypes.AsNoTracking() on v.TypeId equals vt.TypeId
            where r.ExpectedDepartureDate.Year == year && r.ExpectedDepartureDate.Month == month
            select new
            {
                d.DeptId,
                d.DeptName,
                Miles = t.EndOdometer - t.StartOdometer,
                Rate = vt.MileageRate
            };

        return await q
            .GroupBy(x => new { x.DeptId, x.DeptName })
            .Select(g => new RevenueByDepartmentDto
            {
                DeptId = g.Key.DeptId,
                DeptName = g.Key.DeptName,
                Trips = g.Count(),
                MilesDriven = g.Sum(x => x.Miles),
                Revenue = g.Sum(x => (decimal)x.Miles * x.Rate)
            })
            .OrderByDescending(x => x.Revenue)
            .ToListAsync();
    }
}
