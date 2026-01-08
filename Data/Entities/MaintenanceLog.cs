using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class MaintenanceLog
{
    public int LogId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly LogStartDate { get; set; }

    public DateOnly? CompletionDate { get; set; }

    public int? ReleasedByMechanicId { get; set; }

    public virtual ICollection<MaintenanceDetail> MaintenanceDetails { get; set; } = new List<MaintenanceDetail>();

    public virtual ICollection<PartUsage> PartUsages { get; set; } = new List<PartUsage>();

    public virtual Mechanic? ReleasedByMechanic { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
