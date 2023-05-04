using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Models;
using Razorvagt2.Interfaces;

namespace Razorvagt2.Pages.Assignments
{
    public class CreateAssignmentModel : PageModel
    {
        private Object _lock = new Object();

        private IAssignmentCatalog _aCatalog;
        private IUserCatalog _userCatalog;

        [BindProperty]
        public Assignment Assignment { get; set; }
        public string CurrentTime { get; set; }

        public List<User> UserList { get; set; }

        public CreateAssignmentModel(IAssignmentCatalog assignmentCatalog, IUserCatalog userCatalog)
        {
            _aCatalog = assignmentCatalog;
            _userCatalog = userCatalog;
            UserList = _userCatalog.GetAllUsers().Result;

        }
        public void OnGet()
        {
            CurrentTime = DateTime.Now.ToString("dd-MM-yyyyT00:00");
        }
        
        public IActionResult OnPost(int user)
        {
            Assignment place = new Assignment();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            lock (_lock)
            {
                _aCatalog.CreateAssignment(Assignment);
                List<Assignment> el = _aCatalog.GetAllAssignments().Result;
                place = el.Last();
            }
            //Place for Assignement types

            return RedirectToPage("GetAllAssignment");


        }
    }
}
