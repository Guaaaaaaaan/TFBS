namespace TFBS.Dtos.Lookups;

public class MechanicLookupDto
{
    public int MechanicId { get; set; }
    public string MechanicName { get; set; } = "";
    public bool HasInspectionAuth { get; set; }
}
