using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
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
    
        [BindProperty]
        public MemberInfo member { get; set; }

        [BindProperty]
        public int Count { get; set; }

        public void OnGet()
        {
            //var uid = int.Parse(Request.Cookies["id"]);
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if(userSession == null)
            {
                Response.Redirect("Login");
                return;
            }

            var uid = userSession.Id;
            var result = _memberService.GetMemberInfo(uid);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var memberInfo = result.Results.FirstOrDefault();

                if (memberInfo != null)
                {
                    member = memberInfo;
                }
            }
            ViewData["Message"] = string.Format("Login Error");
        }

        public void OnPostPatchMemberInfo()
        {
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                Response.Redirect("Login");
                return;
            }

            var uid = userSession.Id;

            PatchMemberInfoReqModel Req = new PatchMemberInfoReqModel
            {
                UID = uid,
                Account = member.Account,
                NickName = member.NickName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber
            };

            var result = _memberService.PatchMemberInfo(Req);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                Response.Redirect("Personal");
                return;
            }
            ViewData["Message"] = string.Format("Patch Error");
        }
    }
}
