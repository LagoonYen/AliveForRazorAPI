using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
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
            await _memberService.PostMemberRegister(account, password);
        }

        /// <summary>
        /// 判斷是否有此帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetHasAccount")]
        public async Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account)
        {
            return await _memberService.GetMemberInfo(account);
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("PatchPassword")]
        public async Task<BaseResponseModel> PatchPassword(string account, string password)
        {
            return await _memberService.PatchMemberInfo(account, password);
        }

        /// <summary>
        /// 登錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostLogin")]
        public async Task<BaseQueryModel<MemberInfo>> PostLogin(string account, string password)
        {
            return await _memberService.PostLogin(account, password);
        }

        /// <summary>
        /// 讀取會員資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMemberInfo")]
        public async Task<BaseQueryModel<MemberInfo>> GetMemberInfo(int id)
        {
            return await _memberService.GetMemberInfo(id);
        }

        //[HttpPatch]
        //[Route("PatchMemberData")]
        



    }
}
