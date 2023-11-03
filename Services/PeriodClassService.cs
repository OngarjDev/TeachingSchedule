using System.Text.Json;
using TeachingSchedule.Models;
namespace TeachingSchedule.Services
{
    public class PeriodClassService
    {
        private readonly Blazored.LocalStorage.ILocalStorageService _LocalStorage;
        public PeriodClassService(Blazored.LocalStorage.ILocalStorageService storage)
        {
            _LocalStorage = storage;
        }
        List<PeriodClass> DefaultPeriod = new List<PeriodClass> {
            new PeriodClass {Id = 1,TimeStart = new TimeOnly(8,00),TimeEnd = new TimeOnly(8,30), },
            new PeriodClass {Id = 2,TimeStart = new TimeOnly(8,30),TimeEnd = new TimeOnly(9,30), },
            new PeriodClass {Id = 3,TimeStart = new TimeOnly(9,30),TimeEnd = new TimeOnly(10,30), },
            new PeriodClass {Id = 4,TimeStart = new TimeOnly(10,30),TimeEnd = new TimeOnly(11,30), },
            new PeriodClass {Id = 5,TimeStart = new TimeOnly(11,30),TimeEnd = new TimeOnly(12, 30),BreakTime = true},
            new PeriodClass {Id = 6,TimeStart = new TimeOnly(12, 30),TimeEnd = new TimeOnly(13, 30)},
            new PeriodClass {Id = 7,TimeStart = new TimeOnly(13, 30),TimeEnd = new TimeOnly(14, 30), },
            new PeriodClass {Id = 8,TimeStart = new TimeOnly(14, 30),TimeEnd = new TimeOnly(15, 30), },
            new PeriodClass {Id = 9,TimeStart = new TimeOnly(15, 30),TimeEnd = new TimeOnly(16,30), },
            new PeriodClass {Id = 10,TimeStart = new TimeOnly(16,30),TimeEnd = new TimeOnly(17,30), },
            new PeriodClass {Id = 11,TimeStart = new TimeOnly(17,30),TimeEnd = new TimeOnly(18,30), },
            new PeriodClass {Id = 12,TimeStart = new TimeOnly(18,30),TimeEnd = new TimeOnly(19,30), },
            new PeriodClass {Id = 13,TimeStart = new TimeOnly(19,30),TimeEnd = new TimeOnly(20,30), },
        };
        public async Task GetDefaultPeriod()
        {
            // แปลง Object เป็น JSON
            string Datajson = JsonSerializer.Serialize(DefaultPeriod);
            // บันทึก JSON ใน LocalStorage
            await _LocalStorage.SetItemAsync("DefaultPeriod", Datajson);
        }
    }
}
