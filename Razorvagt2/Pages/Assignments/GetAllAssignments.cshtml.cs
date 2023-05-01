using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Pages.Assignments
{
    public class GetAllAssignmentsModel : PageModel
    {
        private IAssignmentCatalog _catalog;
        public GetAllAssignmentsModel(IAssignmentCatalog assignmentsCatalog)
        {
            _catalog = assignmentsCatalog;
        }

        private List<Assignment> _assignmentList;

        public List<Assignment> Assigments
        {
            get { return _assignmentList; }
        }

        public void OnGet()
        {
            _assignmentList = _catalog.GetAllAssignments().Result;

        }
    }
}
