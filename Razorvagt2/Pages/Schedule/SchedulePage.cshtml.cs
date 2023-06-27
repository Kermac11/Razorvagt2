using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Pages.Schedule
{
    public class SchedulePageModel : PageModel
    {
        private IAssignmentCatalog _acatalog;
        public SchedulePageModel(IAssignmentCatalog acatalog)
        {
            _acatalog = acatalog;
        }
        public List<Assignment> ScheduleAssignments { get; set; }
        public void OnGet()
        {
            List<Assignment> assignment = _acatalog.GetAllAssignments().Result;


            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now.AddDays(-6) >= a.Date && DateTime.Now <= a.Date);
                    break;
                case DayOfWeek.Monday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now <= a.Date && DateTime.Now.AddDays(6) >= a.Date);
                    break;
                case DayOfWeek.Tuesday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now.AddDays(-1) <= a.Date && DateTime.Now.AddDays(5) >= a.Date);
                    break;
                case DayOfWeek.Wednesday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now.AddDays(-2) <= a.Date && DateTime.Now.AddDays(4) >= a.Date);
                    break;
                case DayOfWeek.Thursday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now.AddDays(-3) <= a.Date && DateTime.Now.AddDays(3) >= a.Date);
                    break;
                case DayOfWeek.Friday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now.AddDays(-4) <= a.Date && DateTime.Now.AddDays(2) >= a.Date);
                    break;
                case DayOfWeek.Saturday:
                    ScheduleAssignments = assignment.FindAll(a => DateTime.Now.AddDays(-5) <= a.Date && DateTime.Now.AddDays(1) >= a.Date);
                    break;
            }
            ScheduleAssignments.Sort((a1, a2) => a1.Date.CompareTo(a2.Date));
        }
    }
}
