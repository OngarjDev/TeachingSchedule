using TeachingSchedule.Models;

namespace TeachingSchedule.Services
{
    public class ClassService
    {
        private TeachingScheduleDbContext _DbContext;
        public ClassService(TeachingScheduleDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task AddClass(Class Datateacher)
        {
            await _DbContext.Classes.AddAsync(Datateacher);
            await _DbContext.SaveChangesAsync();
        }
    }
}
