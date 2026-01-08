using TFBS.Dtos.Reports;

namespace TFBS.Services.Interfaces;

public interface IReportService
{
    Task<List<MileageByVehicleDto>> MileageByVehicleAsync(int year, int month);
    Task<List<MileageByDepartmentDto>> MileageByDepartmentAsync(int year, int month);
    Task<List<RevenueByDepartmentDto>> RevenueByDepartmentAsync(int year, int month);
}
