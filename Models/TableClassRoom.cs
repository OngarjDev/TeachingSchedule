using System;
using System.Collections.Generic;

namespace TeachingSchedule.Models;

public partial class TableClassRoom
{
    public int IdTableclass { get; set; }

    public int IdtimePeriod { get; set; }

    public string? DayTableclass { get; set; }

    public int? IdRoom { get; set; }

    public int? IdSubject { get; set; }

    public int? IdTeacher { get; set; }

    public bool BreaktimeTableclass { get; set; }

    public int IdClass { get; set; }

    public int NumberroomTableclass { get; set; }
}
