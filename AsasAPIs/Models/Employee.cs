using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class Employee
{
    // This is the primary key for the Employee table
    public int EmployeeId { get; set; }


    // This is the foreign key for the Company table
    public int ComId { get; set; }


    // This is the foreign key for the AddEmp table
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
