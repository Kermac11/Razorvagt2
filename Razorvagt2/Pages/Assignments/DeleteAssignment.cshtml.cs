using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Pages.Assignments
{
    public class DeleteAssignmentModel : PageModel
    {
        private IAssignmentCatalog _acatalog;
        public DeleteAssignmentModel(IAssignmentCatalog acatalog)
        {
            _acatalog = acatalog;
        }
        public Assignment Assignment { get; set; }
        public void OnGet(int id)
        {
            Assignment = _acatalog.GetAssignmentFromId(id).Result;
        }

        public IActionResult OnPost(int id)
        {
            _acatalog.DeleteAssignment(id);

            return RedirectToPage("/index");
        }
    }
}
