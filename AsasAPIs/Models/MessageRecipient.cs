using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class MessageRecipient
{
    public int MsgId { get; set; }

    public int RecipientId { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual InternalMessage Msg { get; set; } = null!;

    public virtual AddEmp Recipient { get; set; } = null!;
}
