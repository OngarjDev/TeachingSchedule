using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TeachingSchedule.Models;

namespace TeachingSchedule.Services
{
    public class ClassService
    {
        private TeachingScheduleDbContext _DbContext;
        private readonly Blazored.LocalStorage.ILocalStorageService _LocalStorage;
        private readonly TeacherService _Teacher;
        private readonly SubjectService _Subject;
        private readonly RoomService _Room;
        public readonly List<string> Day = new List<string> { "วันจันทร์", "วันอังคาร์", "วันพุธ", "วันพฤหัสบดี", "วันศุกร์" };
        public ClassService(TeachingScheduleDbContext dbContext, Blazored.LocalStorage.ILocalStorageService localStorage, TeacherService teacher, SubjectService subject, RoomService room)
        {
            _DbContext = dbContext;
            _LocalStorage = localStorage;
            _Teacher = teacher;
            _Subject = subject;
            _Room = room;
        }
        public async Task AddClass(Class Datateacher)
        {
            Datateacher.IdClass = 0;
            await _DbContext.Classes.AddAsync(Datateacher);
            await _DbContext.SaveChangesAsync();
        }
        public async Task<List<Class>> GetClass()
        {
            return await _DbContext.Classes.ToListAsync();
        }
        public async Task<Class> GetClassByIdClass(int IdClass)
        {
            return await _DbContext.Classes.Where(Class => Class.IdClass == IdClass).FirstAsync();
        }
        public async Task DeleteClass(int idclass)
        {
            Class DataClass = await _DbContext.Classes.Where(x => x.IdClass == idclass).FirstAsync();
            _DbContext.Classes.Remove(DataClass);
            _DbContext.SaveChanges();
        }

        /// <summary>
        /// ทำการเช็คว่าข้อมูลซ้ำกันหรือไม่ ระหว่างตารางสอนแต่ละห้อง
        /// </summary>
        /// <returns>จริง ข้อมูลซ้ากัน, ไม่จริง ข้อมูลไม่ซ้ำกัน</returns>
        public async Task GenerateTable(int IdClass)
        {
            string PeriodJson = await _LocalStorage.GetItemAsync<string>("DefaultPeriod");
            List<PeriodClass> Period = JsonSerializer.Deserialize<List<PeriodClass>>(PeriodJson);
            Random random = new Random();
            List<Teacher> Teacher = await _Teacher.GetTeacherByIdClass(IdClass);
            List<Subject> Subjects = await _Subject.GetSubjectByIdClass(IdClass);
            List<Room> Room = await _Room.GetRoomByIdClass(IdClass);
            Class Class_Db = await GetClassByIdClass(IdClass);
            List<TableClassRoom> tableClassRoomsKeeps = new List<TableClassRoom>();
            for (int i = 0; i <= Day.Count - 1; i++) ///ลูบตารางสอนตามวัน
            {
                Teacher = await _Teacher.GetTeacherByIdClass(IdClass);
                Room = await _Room.GetRoomByIdClass(IdClass);
                //bool status_study = false;///เช็คสถานะกรณีมีคาบติดกันแล้วมีพัก
                int hours_study = 0;///ชั่วโมงต้องไม่เกินต่อวัน
                //int total_study = 0;
                //int keepid_study = 0;
                //int id_study = 0;
                //int position_subject = 0;
                //bool status_study = false;
                for (int k_Loop = 0; k_Loop <= Period.Count - 4; k_Loop++)///ลูปเวลาในแต่ละคาบ 
                {
                    TableClassRoom tableClassRoom = new TableClassRoom();// ล้างค้าทุกครั้งเพื่อสร้างคาบใหม่ๆ
                    if (Period[k_Loop].BreakTime == true)
                    {
                        tableClassRoom.BreaktimeTableclass = true; _DbContext.TableClassRooms.Add(tableClassRoom); continue;
                    }
                    //if (total_study > 0)
                    //{
                    //    tableClassRoom.IdSubject = id_study;
                    //    total_study -= 1;
                    //    if (total_study <= 0) { Subjects.RemoveAt(position_subject); }
                    //}
                    if (Subjects.Count == 0 || hours_study >= Period.Count - 1) { break; /* หยุดการทำงานของลูป*/}
                    tableClassRoom.DayTableclass = Day[i];
                    tableClassRoom.IdtimePeriod = Period[hours_study].Id;
                    tableClassRoom.IdClass = IdClass;
                    if (Room.Count() > 0)
                    {
                        int randomroom = random.Next(0, Room.Count());
                        tableClassRoom.IdRoom = Room[randomroom].IdRoom;
                        Room.RemoveAt(randomroom);
                    }
                    else
                    {
                        tableClassRoom.IdRoom = 0;
                    }
                    ///ตรวจสอบว่า 1 วิชาสอนกี่คาบ
                    int randomsubject = random.Next(0, Subjects.Count());
                    if (Subjects[randomsubject].AmountSubject > 1)
                    {
                        hours_study += Subjects[randomsubject].AmountSubject ?? 1;
                        tableClassRoom.AmountSubject = Subjects[randomsubject].AmountSubject ?? 1;
                        if (hours_study + Subjects[randomsubject].AmountSubject >= Period.Count())
                        {
                            try
                            {
                                tableClassRoom.IdSubject = Subjects.Where(lp => lp.AmountSubject == 1).FirstOrDefault().IdSubject;
                            }catch (Exception ex)
                            {
                                tableClassRoom.IdSubject = Subjects[0].IdSubject;
                            }
                        }
                        else
                        {
                            tableClassRoom.IdSubject = Subjects[randomsubject].IdSubject;
                        }
                        //total_study = Subjects[randomsubject].AmountSubject - 1 ?? 1;
                        //id_study = Subjects[randomsubject].IdSubject;
                        //position_subject = randomsubject;
                        Subjects.RemoveAt(randomsubject);
                        
                    }
                    else
                    {
                        tableClassRoom.IdSubject = Subjects[randomsubject].IdSubject;
                        Subjects.RemoveAt(randomsubject);
                    }

                    ///ตรวจสอบว่า อาจารย์คนในมีวิชาเอกนี้บ้าง
                    tableClassRoom.IdTeacher = Teacher.Where(teacher => teacher.IdSubject == tableClassRoom.IdSubject)
                            .Select(teacher => teacher.IdTeacher)
                            .FirstOrDefault();

                    Teacher teacherToDelete = Teacher.FirstOrDefault(teacher => teacher.IdTeacher == tableClassRoom.IdTeacher);
                    Teacher.Remove(teacherToDelete);
                    tableClassRoomsKeeps.Add(tableClassRoom);

                }
            }
            string DataJson = JsonSerializer.Serialize(tableClassRoomsKeeps);
            var Dbconnect = await _DbContext.Classes.Where(xe => xe.IdClass == IdClass).FirstAsync();
            Dbconnect.ContentClass = DataJson;
            await _DbContext.SaveChangesAsync();
        }
    }
}
