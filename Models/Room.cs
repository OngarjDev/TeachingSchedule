using System;
using System.Collections.Generic;

namespace TeachingSchedule.Models
{
    public partial class Room
    {
        public int IdRoom { get; set; }
        public string NameRoom { get; set; } = null!;
        public TimeSpan? ActiveTimeRoom { get; set; }
    }
}
