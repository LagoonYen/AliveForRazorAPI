using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

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
        /// 註冊帳號
        /// </summary>
        /// <param name="Req">帳號密碼</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] LoginReqModel Req)
        {
            try
            {
                var result = _memberService.PostMemberRegister(Req);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 登錄
        /// </summary>
        /// <param name="Req">帳號密碼</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        public IActionResult Login([FromForm]LoginReqModel Req)
        {
            try
            {
                //登入帳號
                var result = _memberService.PostLogin(Req);
                if(result.Results == null)
                {
                    throw new Exception(message: result.Message);
                }

                //寫入cookies
                var cookieOptions = new CookieOptions
                {
                    //取得或設定值，這個值會指出是否要使用安全通訊端層 (SSL) （也就是透過 HTTPS）來傳送 cookie。
                    Secure = true,
                    //取得或設定值，這個值指定用戶端指令碼是否可以存取 Cookie。
                    HttpOnly = true,
                    //取得或設定 Cookie 的到期日和時間。
                    Expires = DateTime.UtcNow + TimeSpan.FromMinutes(60),
                    //取得或設定 Cookie 的 SameSite 屬性值。 預設值為 Unspecified
                    SameSite = SameSiteMode.None
                };
                Response.Cookies.Append("account", result.Results.FirstOrDefault().Account, cookieOptions);
                Response.Cookies.Append("UID", result.Results.FirstOrDefault().Id.ToString(), cookieOptions);
                
                return Ok(result);
            }
            catch (Exception ex)
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
        public IActionResult MemberInfo()
        {
            try
            {
                //取得cookie
                var UID = int.Parse(Request.Cookies["UID"]);
                var result = _memberService.GetMemberInfo(UID);
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
        public IActionResult PatchMemberInfo([FromBody]PatchMemberInfoReqModel Req)
        {
            try
            {
                //取得cookie
                Req.UID = int.Parse(Request.Cookies["UID"]);
                var result = _memberService.PatchMemberInfo(Req);
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
    }
}
