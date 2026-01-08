using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class Part
{
    public int PartId { get; set; }

    public string PartName { get; set; } = null!;

    public int QtyOnHand { get; set; }

    public int MinQty { get; set; }

    public virtual ICollection<PartUsage> PartUsages { get; set; } = new List<PartUsage>();
}
