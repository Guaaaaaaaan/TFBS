using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class PartUsage
{
    public int UsageId { get; set; }

    public int LogId { get; set; }

    public int PartId { get; set; }

    public int QuantityUsed { get; set; }

    public virtual MaintenanceLog Log { get; set; } = null!;

    public virtual Part Part { get; set; } = null!;
}
