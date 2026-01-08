namespace TFBS.Dtos.Reservations;

public class CreateReservationRequest
{
    public int DeptId { get; set; }
    public int FacultyId { get; set; }
    public int RequiredTypeId { get; set; }

    public DateTime ExpectedDepartureDate { get; set; }
    public string Destination { get; set; } = "";
}
