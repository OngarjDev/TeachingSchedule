using Microsoft.EntityFrameworkCore;
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
            Datateacher.IdClass = 0;
            await _DbContext.Classes.AddAsync(Datateacher);
            await _DbContext.SaveChangesAsync();
        }
        public async Task<List<Class>> GetClass()
        {
            return await _DbContext.Classes.ToListAsync();
        }
        public async Task DeleteClass(int idclass)
        {
            Class DataClass = await _DbContext.Classes.Where(x => x.IdClass == idclass).FirstAsync();
            _DbContext.Classes.Remove(DataClass);
            _DbContext.SaveChanges();
        }
    }
}
