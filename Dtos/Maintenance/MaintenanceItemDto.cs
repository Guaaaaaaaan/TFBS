namespace TFBS.Dtos.Maintenance;

public class MaintenanceItemDto
{
    public string Description { get; set; } = string.Empty;
    public int PerformedByMechanicId { get; set; }
}
