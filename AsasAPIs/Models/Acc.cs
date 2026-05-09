using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asas.Models;

public partial class Acc
{

    [Required(ErrorMessage = "Comid is required")]
    public int ComId { get; set; }

    public string EmpId { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Pass { get; set; } = null!; 
     public byte[] IV { get; set; } = null!; // Use for Decryption Pass 


    public string? PhoneN { get; set; }

    public bool? IsActive { get; set; }

    public virtual AddE AddE { get; set; } = null!;
}
