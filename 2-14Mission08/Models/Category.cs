using System;
using System.Collections.Generic;

namespace _2_14Mission08.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
}
