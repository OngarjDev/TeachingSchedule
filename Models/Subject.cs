namespace TeachingSchedule.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Classes = new HashSet<Class>();
        }

        public int IdSubject { get; set; }
        public string NameSubject { get; set; } = null!;
        public string? PassSubject { get; set; }
        public int? AmountSubject { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
