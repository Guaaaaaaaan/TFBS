using TFBS.Dtos.Lookups;

namespace TFBS.Services.Interfaces;

public interface ILookupService
{
    Task<List<LookupItemDto>> GetDepartmentsAsync();
    Task<List<LookupItemDto>> GetFacultiesAsync(int? deptId);
    Task<List<LookupItemDto>> GetVehicleTypesAsync();
    Task<List<VehicleLookupDto>> GetVehiclesAsync(int? typeId);

    Task<List<MechanicLookupDto>> GetMechanicsAsync(bool? onlyInspectors);
}
