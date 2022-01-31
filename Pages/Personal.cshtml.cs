using AliveStoreTemplate.Model;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net;

namespace AliveStoreTemplate.Pages
{
    public class PersonalModel : PageModel
    {
        private readonly MemberService _memberService;

        public PersonalModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
        public MemberInfo member { get; set; }

        public void OnGet()
        {
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                Response.Redirect("Login");
                return;
            }

            var uid = userSession.Id;
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
