using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int ComId { get; set; }

    public int EmpAutoId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string HashingPassword { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool IsActive { get; set; }

    public string Major { get; set; } = null!;

    public virtual Company Com { get; set; } = null!;

    public virtual AddEmp EmpAuto { get; set; } = null!;
}
