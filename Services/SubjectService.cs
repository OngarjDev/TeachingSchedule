using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingSchedule.Models;

namespace TeachingSchedule.Services
{
    public class SubjectService : Controller
    {
        private readonly TeachingScheduleDbContext _DbContext;
        public SubjectService(TeachingScheduleDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task AddSubject(Subject DataSubject)
        {
            DataSubject.IdSubject = 0;
            await _DbContext.Subjects.AddAsync(DataSubject);
            await _DbContext.SaveChangesAsync();
        }
        public async Task<List<Subject>> GetSubject()
        {
            return await _DbContext.Subjects.ToListAsync();
        }
    }
}
