using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserCatalog _userCatalog;

        private List<User> _users;

        [BindProperty]
        public User User { get; set; }

        public string Error { get; set; }

        public CreateUserModel(IUserCatalog userCatalog)
        {
            _userCatalog = userCatalog;
            _users = _userCatalog.GetAllUsers().Result;
        }
        public async Task OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_users.Exists(u => u.Username == User.Username))
            {
                Error = "Dette Brugernavn eksitere allrede";
                return Page();
            }
            await _userCatalog.CreateUser(User);

           return RedirectToPage("GetAllUsers");
        }
    }
}
