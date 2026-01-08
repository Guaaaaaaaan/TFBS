using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class Mechanic
{
    public int MechanicId { get; set; }

    public string MechanicName { get; set; } = null!;

    public bool HasInspectionAuth { get; set; }

    public virtual ICollection<MaintenanceDetail> MaintenanceDetails { get; set; } = new List<MaintenanceDetail>();

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
}
