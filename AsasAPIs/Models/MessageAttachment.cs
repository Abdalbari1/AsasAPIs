using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class MessageAttachment
{
    public int AttachmentId { get; set; }

    public int MsgId { get; set; }

    public string FilePath { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public virtual InternalMessage Msg { get; set; } = null!;
}
