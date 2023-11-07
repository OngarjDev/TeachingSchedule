namespace TeachingSchedule.Models
{
    public class ClassDay
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public int IdTime { get; set; }
        public int? IdRoom { get; set; }
        public int? IdSubject { get; set; }
        public int? IdTeacher { get; set; }
        public bool? BreakTime { get; set; }
    }
}
