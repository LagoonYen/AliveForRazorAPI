using AliveStoreTemplate.Model;
using AliveStoreTemplate.Repositories;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberInfoController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MemberInfoController(MemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        //標示該方法的回傳格式
        //[Produces("application/json")]
        //指定回傳時的型別
        //[ProducesResponseType(typeof(CardInformationProViewModel), 200)]
        [Route("PostMemberRegister")]
        public async Task PostMemberRegister(string account, string password)
        {
            var TimeNow = DateTime.Now;
            await _memberService.PostMemberRegister(account, password);
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("PatchPwd")]
        public async Task PatchMemberInfo(string account, string password)
        {
            await _memberService.PatchMemberInfo(account, password);
        }




    }
}
