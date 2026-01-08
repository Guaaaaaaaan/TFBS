using TFBS.Dtos.Maintenance;

namespace TFBS.Services.Interfaces;

public interface IMaintenanceService
{
    Task<CreateMaintenanceLogResponse> CreateLogAsync(CreateMaintenanceLogRequest request);
    Task CompleteAsync(int logId, CompleteMaintenanceRequest request);

}