using Razorvagt2.Services;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razorvagt2.Pages.Users
{
    public class GetAllUsersModel : PageModel
    {
        public List<User> Users;

        private IUserCatalog _userCatalog;

        public GetAllUsersModel(IUserCatalog userCatalog)
        {
            _userCatalog = userCatalog;
        }
        public void OnGet()
        {
            Users = _userCatalog.GetAllUsers().Result;
        }
    }
}
