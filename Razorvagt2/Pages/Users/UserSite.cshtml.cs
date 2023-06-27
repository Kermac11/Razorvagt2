using Razorvagt2.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razorvagt2.Models;
namespace Razorvagt2.Pages.Users
{
    public class UserSiteModel : PageModel
    {
        private IUserCatalog _ucatalog;

        public UserSiteModel(IUserCatalog ucatalog)
        {
            _ucatalog = ucatalog;
        }
        [BindProperty]
        public new User User
        {
            get; set;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            bool log = _ucatalog.GetAllUsers().Result.Exists(u => u.Username == User.Username && u.Password == User.Password);
            
            if (log)
            {
                User user = _ucatalog.GetAllUsers().Result.Find(u => u.Username == User.Username && u.Password == User.Password);
                if (user.Admin)
                {
                    HttpContext.Session.SetString("admin", "true");
                    HttpContext.Session.SetString("username", user.Username);
                    return RedirectToPage("/index");
                }
            }

            return Page();
        }
    }
}
