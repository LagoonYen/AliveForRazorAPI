using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AliveStoreTemplate.Services
{
    public class MemberServiceImpl : MemberService
    {
        private readonly MemberRepository _memberRepository;
        
        public MemberServiceImpl(MemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel PostMemberRegister(LoginReqModel Req)
        {
            try
            {
                var account = Req.Account;
                var password = Req.Password;

                //取得帳號存在與否 bool
                var IsUserExist = _memberRepository.IsMemberExist(account);
                if (IsUserExist)
                {
                    throw new Exception("此帳號已被註冊過");
                }

                //開始建立新帳戶必要資料
                MemberInfo member = new MemberInfo();
                member.Account = member.Email = account;
                member.Password = password;
                member.RegisterTime = member.UpdateTime = DateTime.Now;

                //創建帳號
                _memberRepository.PostMemberRegister(member);
                return new BaseResponseModel
                {
                    Message = "註冊成功",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 註冊(有模型)
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseQueryModel<MemberInfo> PostLogin(LoginReqModel Req)
        {
            try
            {
                var account = Req.Account;
                var password = Req.Password;

                //取得帳號存在與否
                var IsUserExist = _memberRepository.IsMemberExist(account);
                //查無帳號
                if (!IsUserExist)
                {
                    throw new Exception("此帳號尚未被註冊");
                }

                //取得其資料
                var UserInfo = _memberRepository.GetMemberInfo(account);

                //比對密碼正確
                if (UserInfo.Password != password)
                {
                    throw new Exception("密碼錯誤");
                }

                return new BaseQueryModel<MemberInfo>
                {
                    Results = new List<MemberInfo> { UserInfo },
                    Message = "登入成功",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<MemberInfo>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 登入(無模型)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BaseQueryModel<MemberInfo> PostLogin(string account, string password)
        {
            try
            {
                //取得帳號存在與否
                var IsUserExist = _memberRepository.IsMemberExist(account);

                //查無帳號
                if (!IsUserExist)
                {
                    throw new Exception("此帳號尚未被註冊");
                }

                //取得其資料
                var MemberInfo = _memberRepository.GetMemberInfo(account);

                //比對密碼正確
                if (MemberInfo.Password != password)
                {
                    throw new Exception("密碼錯誤");
                }

                return new BaseQueryModel<MemberInfo>
                {
                    Results = new List<MemberInfo> { MemberInfo },
                    Message = "登入成功",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<MemberInfo>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 讀取個人資料
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public BaseQueryModel<MemberInfo> GetMemberInfo(int UID)
        {
            try
            {
                var baseQueryModel = _memberRepository.GetMemberInfo(UID);
                return new BaseQueryModel<MemberInfo>
                {
                    Results = new List<MemberInfo> { baseQueryModel },
                    Message = "讀取資料正確",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<MemberInfo>()
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 修改帳號內容
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel PatchMemberInfo(PatchMemberInfoReqModel Req)
        {
            try
            {
                //先取得帳號存在 及資料
                var memberInfo = _memberRepository.GetMemberInfo(Req.UID);

                //欲修改資料
                memberInfo.NickName = Req.NickName;
                memberInfo.PhoneNumber = Req.PhoneNumber;
                memberInfo.Email = Req.Email;
                memberInfo.UpdateTime = DateTime.Now;

                //修改資料並回傳
                _memberRepository.PatchMemberInfo(memberInfo);
                return new BaseResponseModel
                {
                    Message = "資料修改完畢",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 修改密碼前取得是否有帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public BaseResponseModel GetMemberInfo(string account)
        {
            try
            {
                var memberExist = _memberRepository.IsMemberExist(account);
                if (!memberExist)
                {
                    throw new Exception("帳號不存在");
                }

                return new BaseResponseModel
                {
                    Message = "帳號存在, 已寄出認證信件",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode= HttpStatusCode.BadRequest
                };
            }
        }

        //修改密碼

    }
}
