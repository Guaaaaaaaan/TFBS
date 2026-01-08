using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public int TypeId { get; set; }

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    public virtual VehicleType Type { get; set; } = null!;
}
