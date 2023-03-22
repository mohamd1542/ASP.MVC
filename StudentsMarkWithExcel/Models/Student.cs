using System;
using System.Collections.Generic;

namespace StudentsMarkWithExcel.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? StudentName { get; set; }

    public virtual ICollection<Degree> Degrees { get; } = new List<Degree>();
    public virtual ICollection<GroupStudent> GroupStudent { get; } = new List<GroupStudent>();
}
