using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Services
{
    public class ScheduleCatalog : Connection, IScheduleCatalog
    {
        private string GetAllScheduleSql = "";

        //        CREATE TABLE[dbo].[Schedule]
        //        (

        //   [Schedule_ID] INT      IDENTITY(1, 1) NOT NULL,

        //  [Start_Date]  DATETIME NULL,

        //  [End_Date]    DATETIME NULL,
        //  CONSTRAINT[PK_Schedule] PRIMARY KEY CLUSTERED([Schedule_ID] ASC)
        //);


        public ScheduleCatalog(IConfiguration configuration) : base(configuration)
        {
        }


        public Task<bool> CreateSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public Task<Schedule> DeleteSchedule(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Schedule>> GetAllSchedule()
        {
            throw new NotImplementedException();
        }

        public Task<Schedule> GetSchedule(int id)
        {
            throw new NotImplementedException();
        }
    }
}
