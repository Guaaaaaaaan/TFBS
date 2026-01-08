namespace TFBS.Dtos.Reports;

public class MileageByVehicleDto
{
    public int VehicleId { get; set; }
    public string PlateNumber { get; set; } = "";
    public int Trips { get; set; }
    public int MilesDriven { get; set; }
}

public class MileageByDepartmentDto
{
    public int DeptId { get; set; }
    public string DeptName { get; set; } = "";
    public int Trips { get; set; }
    public int MilesDriven { get; set; }
}

public class RevenueByDepartmentDto
{
    public int DeptId { get; set; }
    public string DeptName { get; set; } = "";
    public int Trips { get; set; }
    public int MilesDriven { get; set; }
    public decimal Revenue { get; set; }
}
