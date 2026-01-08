using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class Trip
{
    public int TripId { get; set; }

    public int ReservationId { get; set; }

    public int VehicleId { get; set; }

    public int FacultyId { get; set; }

    public int StartOdometer { get; set; }

    public int EndOdometer { get; set; }

    public decimal? FuelGallons { get; set; }

    public string? CreditCardNo { get; set; }

    public string? MaintenanceComplaint { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
