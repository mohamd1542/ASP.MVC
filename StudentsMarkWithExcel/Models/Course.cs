using System;
using System.Collections.Generic;

namespace StudentsMarkWithExcel.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? CourseName { get; set; }

    public virtual ICollection<Degree> Degrees { get; } = new List<Degree>();
}
