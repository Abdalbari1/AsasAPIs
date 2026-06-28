using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int ComId { get; set; }

    public int EmpAutoId { get; set; }

    public int DepAutoId { get; set; }

    public int? SupervisorId { get; set; }

    public string TaskName { get; set; } = null!;

    public string TaskDetails { get; set; } = null!;

    public decimal TaskPriority { get; set; }

    public DateOnly DateOfStart { get; set; }

    public DateOnly DateOfEnd { get; set; }

    public DateOnly? CompletionDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Company Com { get; set; } = null!;

    public virtual Department DepAuto { get; set; } = null!;

    public virtual AddEmp EmpAuto { get; set; } = null!;
}
