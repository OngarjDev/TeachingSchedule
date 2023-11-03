using TeachingSchedule.Models;

namespace TeachingSchedule.Services
{
    public class TeacherService
    {
        private readonly TeachingScheduleDbContext _DbContext;
        public TeacherService(TeachingScheduleDbContext Dbconnect) 
        { 
            _DbContext = Dbconnect;
        }
        public async Task AddTeacher(Teacher DataTeacher)
        {
            await _DbContext.Teachers.AddAsync(DataTeacher);
            await _DbContext.SaveChangesAsync();
        }
    }
}
