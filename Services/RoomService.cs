
using Microsoft.EntityFrameworkCore;
using TeachingSchedule.Models;

namespace TeachingSchedule.Services
{
    public class RoomService
    {
        private readonly TeachingScheduleDbContext _DbContext;
        public RoomService(TeachingScheduleDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task AddRoomToDb(Room DataRoom)
        {
            await _DbContext.Rooms.AddAsync(DataRoom);
            await _DbContext.SaveChangesAsync();
        }
        public async Task<List<Room>> GetRoomByIdClass(int IdClass)
        {
            return await _DbContext.Rooms.Where(room => room.IdClass == IdClass).ToListAsync();
        }
    }
}