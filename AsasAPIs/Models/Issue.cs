using System;
using System.Collections.Generic;

namespace AsasAPIs.Models;

public partial class Issue
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Message { get; set; }
}
