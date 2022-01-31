using AliveStoreTemplate.Model;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net;

namespace AliveStoreTemplate.Pages
{
    public class BuildPersonalPageModel : PageModel
    {
        private readonly MemberService _memberService;

        public BuildPersonalPageModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
        public string Gender { get; set; }
        public string[] Genders = new[] { "Male", "Female", "Unspecified" };
    

        public MemberInfo member { get; set; }

        public void OnGet()
        {
            //var uid = int.Parse(Request.Cookies["id"]);
            var user = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if(user == null)
            {
                Response.Redirect("Login");
                return;
            }

            var uid = user.Id;
            var result = _memberService.GetMemberInfo(uid);

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var memberInfo = result.Result.Results.FirstOrDefault();

                if (memberInfo != null)
                {
                    member = memberInfo;
                }
            }
            ViewData["Message"] = string.Format("Login Error");
        }
    }
}
