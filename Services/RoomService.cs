
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
            DataRoom.IdRoom = 0;
            await _DbContext.Rooms.AddAsync(DataRoom);
            await _DbContext.SaveChangesAsync();
        }
    }
}