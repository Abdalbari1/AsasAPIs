using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class Department
{
    public int ComId { get; set; }

    public int DepAutoId { get; set; }

    public string DepName { get; set; } = null!;

    public int? SupervisorId { get; set; }

    public virtual ICollection<AddEmp> AddEmps { get; set; } = new List<AddEmp>();

    public virtual Company Com { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
