using System;
using System.Collections.Generic;

namespace Asas.Models;

public partial class Company
{
    public int ComId { get; set; }

    public string EmpId { get; set; } = null!;

    public decimal? Price { get; set; }

    public DateTime DateOfEnd { get; set; }

    public bool IsActive { get; set; }
}
