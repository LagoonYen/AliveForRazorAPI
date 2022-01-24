using AliveStoreTemplate.Common;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Utils;

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

        public class reCAPTCHAResponse
        {
            public bool success { get; set; }
        }

        /// <summary>
        /// 登錄
        /// </summary>
        /// <remarks>注意事項：請將Acct及pwd打包</remarks> 
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Login([FromForm]LoginReqModel Req, [FromForm] string recaptcha)
        public async Task<IActionResult> Login([FromForm]LoginReqModel Req)
        {
            try
            {
                //var form = new NameValueCollection()
                //{
                //    ["secret"] = "6LegNyweAAAAAEmqmofjJzoJElV4TXmdGuNHQ7yO",
                //    ["response"] = recaptcha // 使用者傳到後端的Token
                //};
                ////StackExchange.Utils.Http套件下載
                //var resultCAPTCHAR = await Http.Request("https://www.google.com/recaptcha/api/siteverify")
                //    .SendForm(form)
                //    .ExpectJson<reCAPTCHAResponse>()
                //    .PostAsync();
                var result = await _memberService.PostLogin(Req);
                if(result.Results == null)
                {
                    throw new Exception(message: result.Message);
                }
                var cookieOptions = new CookieOptions
                {
                    // Set the secure flag, which Chrome's changes will require for SameSite none.
                    // Note this will also require you to be running on HTTPS.
                    Secure = true,

                    // Set the cookie to HTTP only which is good practice unless you really do need
                    // to access it client side in scripts.
                    HttpOnly = true,
                    Expires = DateTime.UtcNow + TimeSpan.FromMinutes(10),
                    // Add the SameSite attribute, this will emit the attribute with a value of none.
                    // To not emit the attribute at all set
                    // SameSite = (SameSiteMode)(-1)
                    SameSite = SameSiteMode.None
                };
                // Add the cookie to the response cookie collection
                Response.Cookies.Append("account", result.Results.FirstOrDefault().Account, cookieOptions);
                Response.Cookies.Append("id", result.Results.FirstOrDefault().Id.ToString(), cookieOptions);
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
        public async Task<IActionResult> Register([FromBody] LoginReqModel Req)
        {
            try
            {
                var result = await _memberService.PostMemberRegister(Req);
                if(result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Message);
                }
                var cookieOptions = new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTime.UtcNow + TimeSpan.FromMinutes(10),
                    SameSite = SameSiteMode.None
                };
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
        /// <returns></returns>
        [HttpGet]
        [Route("MemberInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MemberInfo()
        {
            try
            {
                //取得cookie
                var uid = int.Parse(Request.Cookies["id"]);
                var result = await _memberService.GetMemberInfo(uid);
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
                //取得cookie
                Req.Id = int.Parse(Request.Cookies["id"]);
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
        ///// 登出
        ///// </summary>
        ///// <returns></returns>
        //登出 Action 記得別加上[Authorize]，不管用戶是否登入，都可以執行Logout
        //[HttpGet]
        //[Route("Logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return RedirectToPage("/Home");
        //}

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
