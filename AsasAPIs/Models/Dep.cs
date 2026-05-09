using System;
using System.Collections.Generic;

namespace Asas.Models;

public partial class Dep
{
    public int ComId { get; set; }

    public string EmpId { get; set; } = null!;

    public string DepId { get; set; } = null!;

    public string DepN { get; set; } = null!;
    public virtual ICollection<AddE> AddEs { get; set; } = new List<AddE>();
}
