using Microsoft.EntityFrameworkCore;
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
            DataTeacher.IdTeacher = 0;
            await _DbContext.Teachers.AddAsync(DataTeacher);
            await _DbContext.SaveChangesAsync();
        }
        public async Task<List<Teacher>> GetTeacherByIdClass(int idclass)
        {
            return await _DbContext.Teachers.Where(teacher => teacher.IdClass == idclass).ToListAsync();
        }
    }
}
