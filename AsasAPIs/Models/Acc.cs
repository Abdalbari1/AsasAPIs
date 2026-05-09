using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asas.Models;

public partial class Acc
{

    [Required(ErrorMessage = "Comid is required")]
    public int ComId { get; set; }

    public string EmpId { get; set; } = null!;
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "صيغة البريد غير صحيحة")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "كلمة المرور مطلوبة")]
    public string Pass { get; set; } = null!; 
     public byte[] IV { get; set; } = null!; // Use for Decryption Pass 


    public string? PhoneN { get; set; }

    public bool? IsActive { get; set; }

    public virtual AddE AddE { get; set; } = null!;
}
