using AliveStoreTemplate.Model;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AliveStoreTemplate.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string Account { get; set; }
        [BindProperty]
        public string Password { get; set; }


        private readonly MemberService _memberService;

        public LoginModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        public void OnGet()
        {
            //var myAccount = Account;
        }

        public void OnPostMyLogin()
        {
            var result = _memberService.PostLogin(Account, Password);

            if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var user = result.Result.Results.FirstOrDefault();
                
                if (user != null)
                {
                    Common.CommonUtil.SessionSetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession, user);
                    Response.Redirect("Home");
                }
            }
            ViewData["Message"] = string.Format("Login Error");
        }
    }
}
