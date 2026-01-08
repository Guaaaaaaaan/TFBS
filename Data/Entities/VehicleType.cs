using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class VehicleType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public decimal MileageRate { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
