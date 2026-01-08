namespace TFBS.Dtos.Maintenance;

public class CompleteMaintenanceRequest
{
    public DateOnly CompletionDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public int ReleasedByMechanicId { get; set; }

    public List<MaintenanceItemDto> Items { get; set; } = new();
    public List<PartUsageDto> Parts { get; set; } = new();
}
