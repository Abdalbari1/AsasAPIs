using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class AddEmp
{
    // This is the primary key for the AddEmp table, which is an auto-incrementing integer.
    public int EmpAutoId { get; set; }

    // This is a foreign key that references the ComId in the Company table.
    public int ComId { get; set; }

   
    public string EmpId { get; set; } = null!;

    public decimal Role { get; set; }

    // This is a foreign key that references the DepAutoId in the Department table.

    public int DepAutoId { get; set; }

    public string JobTitle { get; set; } = null!;

    public virtual Company Com { get; set; } = null!;

    public virtual Department DepAuto { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<InternalMessage> InternalMessages { get; set; } = new List<InternalMessage>();

    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; } = new List<MessageRecipient>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
