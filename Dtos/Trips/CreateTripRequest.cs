namespace TFBS.Dtos.Trips;

public class CreateTripRequest
{
    public int ReservationId { get; set; }
    public int VehicleId { get; set; }
    public int FacultyId { get; set; }
    public int StartOdometer { get; set; }
}
