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
        public readonly List<string> Day = new List<string> { "M", "Tu", "W", "Th", "F" };
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
            Class Class_Dbs = await GetClassByIdClass(IdClass);
            Random random = new Random();

            for (int i = 0; i <= Class_Dbs.NumberClass; i++) ///ลูบตารางสอนตามห้องเรียน
            {
                List<Teacher> Teacher = await _Teacher.GetTeacherByIdClass(IdClass);
                List<Subject> Subjects = await _Subject.GetSubjectByIdClass(IdClass);
                List<Room> Room = await _Room.GetRoomByIdClass(IdClass);
                Class Class_Db = await GetClassByIdClass(IdClass);
                List<ClassDay> ManyTableClass = new List<ClassDay>();
                for (int k = 0; k <= Period.Count - 1; k++)///ลูปเวลาในแต่ละคาบ
                    {
                        TableClassRoom tableClassRoom = new TableClassRoom();// ล้างค้าทุกครั้งเพื่อสร้างคาบใหม่ๆ
                        tableClassRoom.NumberroomTableclass = i;
                        tableClassRoom.DayTableclass = Day[i];
                        if (Subjects.Count == 0) { break; /* หยุดการทำงานของลูป*/}
                        tableClassRoom.IdtimePeriod = Period[k].Id;
                        tableClassRoom.IdClass = IdClass;
                        if (Period[k].BreakTime == true) { tableClassRoom.BreaktimeTableclass = true; _DbContext.TableClassRooms.Add(tableClassRoom); continue; }
                        int randomsubject = random.Next(0, Subjects.Count());
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
                        tableClassRoom.IdSubject = Subjects[randomsubject].IdSubject;
                        tableClassRoom.IdTeacher = Teacher.Where(teacher => teacher.IdSubject == tableClassRoom.IdSubject)
                            .Select(teacher => teacher.IdTeacher)
                            .FirstOrDefault();

                        Teacher teacherToDelete = Teacher.FirstOrDefault(teacher => teacher.IdTeacher == tableClassRoom.IdTeacher);
                        Teacher.Remove(teacherToDelete);
                        Subjects.RemoveAt(randomsubject); // เปลี่ยนจาก randomSubject เป็น s
                        _DbContext.TableClassRooms.Add(tableClassRoom);
                    }
            }
            await _DbContext.SaveChangesAsync();
        }
    }
}
