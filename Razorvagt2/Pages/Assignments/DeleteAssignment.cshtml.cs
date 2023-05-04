using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Pages.Assignments
{
    public class DeleteAssignmentModel : PageModel
    {
        public Assignment Assignment { get; set; }
        public void OnGet()
        {
        }
    }
}
