using Razorvagt2.Models;
namespace Razorvagt2.Interfaces
{
    public interface IScheduleCatalog
    {
        Task<List<Schedule>> GetAllSchedule();

        Task<bool> CreateSchedule(Schedule schedule);

        Task<Schedule> DeleteSchedule(int id);

        Task<Schedule> GetSchedule(int id);
    }
}
