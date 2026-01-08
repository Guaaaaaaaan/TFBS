using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class MaintenanceDetail
{
    public int DetailId { get; set; }

    public int LogId { get; set; }

    public string Description { get; set; } = null!;

    public int PerformedByMechanicId { get; set; }

    public virtual MaintenanceLog Log { get; set; } = null!;

    public virtual Mechanic PerformedByMechanic { get; set; } = null!;
}
