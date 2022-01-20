using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
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
        /// 登錄
        /// </summary>
        /// <remarks>注意事項：請將Acct及pwd打包</remarks> 
        /// <returns></returns>
        //標示該方法的回傳格式
        //[Produces("application/json")]
        //指定回傳時的型別
        //[ProducesResponseType(typeof(CardInformationProViewModel), 200)]
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromForm] LoginReqModel Req)
        {
            try
            {
                var result = await _memberService.PostLogin(Req);
                if(result.Results == null)
                {
                    throw new Exception(message: result.Message);
                }
                //todo 
                //HttpCookie myCookie = new HttpCookie("Product"); //Cookie名稱  
                //foreach (var i in Product)
                //{
                //    myCookie[j.ToString()] = i.Id.ToString() + "," + i.Name + "," + i.Price.ToString() + "," + i.Amount.ToString();//欲儲存的資料內容(這邊以逗號作區隔)
                //    j++; //子索引鍵的識別值
                //}
                //myCookie.Expires = DateTime.Now.AddDays(1);//有效期限一天 
                //myCookie.HttpOnly = true;
                //Response.Cookies.Add(myCookie);//加入Cookie
                //return true;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromForm] LoginReqModel Req)
        {
            try
            {
                var result = await _memberService.PostMemberRegister(Req);
                if(result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Message);
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 讀取會員資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MemberInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MemberInfo(int id)
        {
            try
            {
                var result = await _memberService.GetMemberInfo(id);
                if(result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Message);
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 修改會員資訊
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("PatchMemberInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchMemberInfo([FromBody] PatchMemberInfoReqModel Req)
        {
            try
            {
                var result = await _memberService.PatchMemberInfo(Req);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    new Exception(message: result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///// <summary>
        ///// 判斷是否有此帳號
        ///// </summary>
        ///// <param name="account"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetHasAccount")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetMemberInfo(string account)
        //{
        //    return NotFound();
        //    await _memberService.GetMemberInfo(account);
        //    var State = HttpStatusCode.BadRequest;
        //}
    }
}
