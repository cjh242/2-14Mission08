using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2_14Mission08.Models;

public partial class TaskList
{
    public int TaskId { get; set; }

    [Required]
    public string Task { get; set; } = null!;

    public string? DueDate { get; set; }

    [Required]
    public int Quadrant { get; set; }

    public int? CategoryId { get; set; }

    public bool? Completed { get; set; }

    public virtual Category? Category { get; set; }
}
