namespace TeachingSchedule.Models
{
    public partial class Class
    {
        public string IdClass { get; set; } = null!;
        public string? NameClass { get; set; }
        public int? IdSubject { get; set; }
        public int? IdTeacher { get; set; }

        public virtual Subject? IdSubjectNavigation { get; set; }
    }
}
