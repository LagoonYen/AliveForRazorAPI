using AliveStoreTemplate.Model;
using AliveStoreTemplate.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberInfoController : ControllerBase
    {
        private readonly MemberRepository _memberRepository;

        public MemberInfoController(MemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
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
        public async Task PostMemberRegister(string ACCT, string Pwd)
        {
            var TimeNow = DateTime.Now;
            await _memberRepository.PostMemberRegister(ACCT, Pwd, TimeNow);
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("PatchPwd")]
        public async Task PatchPwdUpdate(string account, string Pwd)
        {
            var memberInfo =  await _memberRepository.GetMemberInfo(account);
            if (memberInfo == null)
            {
                return;
            }
            var DateTimeNow = DateTime.Now;
            memberInfo.Password = Pwd;
            memberInfo.UpdateTime = DateTimeNow;
            await _memberRepository.PatchMemberInfo(memberInfo);
        }




    }
}
