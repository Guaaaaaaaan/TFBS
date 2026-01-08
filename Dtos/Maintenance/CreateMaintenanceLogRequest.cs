namespace TFBS.Dtos.Maintenance;

public class CreateMaintenanceLogRequest
{
    public int VehicleId { get; set; }
    public DateOnly LogStartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

}
