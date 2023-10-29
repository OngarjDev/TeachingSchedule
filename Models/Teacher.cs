using System;
using System.Collections.Generic;

namespace TeachingSchedule.Models
{
    public partial class Teacher
    {
        public int IdTeacher { get; set; }
        public string NameTeacher { get; set; } = null!;
        public int IdSubject { get; set; }
    }
}
