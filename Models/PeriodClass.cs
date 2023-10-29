namespace TeachingSchedule.Models
{
    /// <summary>
    /// Model ที่มีไว้เพื่อใช้สร้างตารางวันที่-เวลา ของคาบเรียน
    /// หากไม่มีข้อมูลใน LocalStorage จะเรียกใช้ DataSet เริ่มต้นสร้างการทำงาน
    /// </summary>
    public partial class PeriodClass
    {
        public int Id { get; set; }
        public TimeOnly TimeStart { get; set; }//เวลาเริ่มเรียน
        public TimeOnly TimeEnd { get; set; }//เวลาหยุดเรียน
        public bool? BreakTime { get; set; }//เวลาพัก
    }
}
