using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AliveStoreTemplate.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string Account { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            var myAccount = Account;
        }

        public void OnPostMyLogin()
        {
            var myAccount = Account;
        }
    }
}
