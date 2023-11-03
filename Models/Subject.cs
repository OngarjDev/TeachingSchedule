using System;
using System.Collections.Generic;

namespace TeachingSchedule.Models;

public partial class Subject
{
    public int IdSubject { get; set; }

    public string NameSubject { get; set; } = null!;

    public string? PassSubject { get; set; }

    public int? AmountSubject { get; set; }

    public virtual ICollection<Class> Classes { get; } = new List<Class>();
}
