using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class Company
{
    public int ComId { get; set; }

    public bool IsActive { get; set; }

    public decimal Price { get; set; }

    public DateTime DateOfEnd { get; set; }

    public virtual ICollection<AddEmp> AddEmps { get; set; } = new List<AddEmp>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<InternalMessage> InternalMessages { get; set; } = new List<InternalMessage>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
