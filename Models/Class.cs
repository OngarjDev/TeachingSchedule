using System;
using System.Collections.Generic;

namespace TeachingSchedule.Models;

public partial class Class
{
    public int IdClass { get; set; }

    public string? NameClass { get; set; }

    public string? ContentClass { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
