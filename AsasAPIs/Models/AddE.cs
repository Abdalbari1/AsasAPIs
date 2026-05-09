using System;
using System.Collections.Generic;

namespace Asas.Models;

public partial class AddE
{
    public int ComId { get; set; }

    public string EmpId { get; set; } = null!;

    public string? DepId { get; set; }

    public string Role { get; set; } = null!;

    public virtual Acc? Acc { get; set; }

    public virtual Dep? Dep { get; set; }

  
}
