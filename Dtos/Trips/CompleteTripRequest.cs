namespace TFBS.Dtos.Trips;

public class CompleteTripRequest
{
    public int EndOdometer { get; set; }
    public decimal? FuelGallons { get; set; }
    public string? CreditCardNo { get; set; }
    public string? MaintenanceComplaint { get; set; }
}
