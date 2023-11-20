using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab01.Models;

public partial class Student
{
    [Key]
    [Required(ErrorMessage = "StudentCode is required")]
    public string StudentCode { get; set; } = null!;
    [Required(ErrorMessage = "StudentName is required")]
    public string? StudentName { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
}
