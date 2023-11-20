using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab01.Models;

public partial class Mark
{
    [Key]
    [Required(ErrorMessage = "StudentCode is required")]
    public string? StudentCode { get; set; }
    [Key]
    [Required(ErrorMessage = "Subject is required")]
    public string Subject { get; set; } = null!;
    [Range(10, 100, ErrorMessage = "Marks from 10 to 100")]
    [Required(ErrorMessage = "Mark is required")]
    public decimal? Mark1 { get; set; }

    public virtual Student? StudentCodeNavigation { get; set; }
}
