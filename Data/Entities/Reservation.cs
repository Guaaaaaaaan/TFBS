using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public DateOnly ExpectedDepartureDate { get; set; }

    public string Destination { get; set; } = null!;

    public int DeptId { get; set; }

    public int FacultyId { get; set; }

    public int RequiredTypeId { get; set; }

    public virtual Department Dept { get; set; } = null!;

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual VehicleType RequiredType { get; set; } = null!;

    public virtual Trip? Trip { get; set; }
}
