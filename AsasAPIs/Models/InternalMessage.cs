using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class InternalMessage
{
    public int MsgId { get; set; }

    public int ComId { get; set; }

    public int SenderId { get; set; }

    public int? ParentMsgId { get; set; }

    public string? Subject { get; set; }

    public string Body { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Company Com { get; set; } = null!;

    public virtual ICollection<InternalMessage> InverseParentMsg { get; set; } = new List<InternalMessage>();

    public virtual ICollection<MessageAttachment> MessageAttachments { get; set; } = new List<MessageAttachment>();

    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; } = new List<MessageRecipient>();

    public virtual InternalMessage? ParentMsg { get; set; }

    public virtual AddEmp Sender { get; set; } = null!;
}
