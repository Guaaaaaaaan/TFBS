using System;
using System.Collections.Generic;

namespace TFBS.Data.Entities;

public partial class Department
{
    public int DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
