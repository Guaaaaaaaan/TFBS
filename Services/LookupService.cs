using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Dtos.Lookups;
using TFBS.Services.Interfaces;

namespace TFBS.Services;

public class LookupService : ILookupService
{
    private readonly TfbsContext _db;

    public LookupService(TfbsContext db)
    {
        _db = db;
    }

    // Returns all departments for dropdown / selection usage
    public async Task<List<LookupItemDto>> GetDepartmentsAsync()
    {
        return await _db.Departments.AsNoTracking()
            .OrderBy(d => d.DeptId)
            .Select(d => new LookupItemDto { Id = d.DeptId, Name = d.DeptName })
            .ToListAsync();
    }

    // Optionally filters faculties by department
    public async Task<List<LookupItemDto>> GetFacultiesAsync(int? deptId)
    {
        var q = _db.Faculties.AsNoTracking();

        if (deptId.HasValue)
            q = q.Where(f => f.DeptId == deptId.Value);

        return await q
            .OrderBy(f => f.FacultyId)
            .Select(f => new LookupItemDto { Id = f.FacultyId, Name = f.FacultyName })
            .ToListAsync();
    }

    // Returns all available vehicle types
    public async Task<List<LookupItemDto>> GetVehicleTypesAsync()
    {
        return await _db.VehicleTypes.AsNoTracking()
            .OrderBy(t => t.TypeId)
            .Select(t => new LookupItemDto { Id = t.TypeId, Name = t.TypeName })
            .ToListAsync();
    }

    // Optionally filters vehicles by vehicle type
    public async Task<List<VehicleLookupDto>> GetVehiclesAsync(int? typeId)
    {
        var q = _db.Vehicles.AsNoTracking();

        if (typeId.HasValue)
            q = q.Where(v => v.TypeId == typeId.Value);

        return await q
            .OrderBy(v => v.VehicleId)
            .Select(v => new VehicleLookupDto
            {
                VehicleId = v.VehicleId,
                PlateNumber = v.PlateNumber,
                TypeId = v.TypeId
            })
            .ToListAsync();
    }

    // Optionally returns only mechanics with inspection authorization
    public async Task<List<MechanicLookupDto>> GetMechanicsAsync(bool? onlyInspectors)
    {
        var q = _db.Mechanics.AsNoTracking();

        if (onlyInspectors == true)
            q = q.Where(m => m.HasInspectionAuth);

        return await q
            .OrderBy(m => m.MechanicId)
            .Select(m => new MechanicLookupDto
            {
                MechanicId = m.MechanicId,
                MechanicName = m.MechanicName,
                HasInspectionAuth = m.HasInspectionAuth
            })
            .ToListAsync();
    }
}