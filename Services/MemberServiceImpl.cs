using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
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

        //註冊
        public BaseResponseModel PostMemberRegister(LoginReqModel Req)
        {
            try
            {
                var account = Req.Account;
                var password = Req.Password;

                //取得帳號存在與否
                var baseQueryModel = _memberRepository.GetMemberInfo(account);
                if (baseQueryModel.Results != null)
                {
                    throw new Exception("此帳號已被註冊過");
                }

                //開始建立新帳戶必要資料
                MemberInfo member = new MemberInfo();
                member.Account = member.Email = account;
                member.Password = password;
                member.RegisterTime = member.UpdateTime = DateTime.Now;

                //創建帳號
                var result = _memberRepository.PostMemberRegister(member);
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

        //登入
        public BaseQueryModel<MemberInfo> PostLogin(LoginReqModel Req)
        {
            try
            {
                var account = Req.Account;
                var password = Req.Password;

                //取得帳號存在與否
                var baseQueryModel = _memberRepository.GetMemberInfo(account);
                var dbPassword = baseQueryModel.Results.FirstOrDefault(x => x.Account == account).Password;
                if (dbPassword != password)
                {
                    throw new Exception("密碼錯誤");
                }
                return new BaseQueryModel<MemberInfo>
                {
                    Results = baseQueryModel.Results,
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

        //登入
        public BaseQueryModel<MemberInfo> PostLogin(string account, string password)
        {
            try
            {
                //取得帳號存在與否
                var baseQueryModel = _memberRepository.GetMemberInfo(account);
                var dbPassword = baseQueryModel.Results.FirstOrDefault(x => x.Account == account).Password;
                if (dbPassword != password)
                {
                    throw new Exception("密碼錯誤");
                }
                return new BaseQueryModel<MemberInfo>
                {
                    Results = baseQueryModel.Results,
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

        //讀取個人資料
        public BaseQueryModel<MemberInfo> GetMemberInfo(int UID)
        {
            try
            {
                var baseQueryModel = _memberRepository.GetMemberInfo(UID);
                return new BaseQueryModel<MemberInfo>
                {
                    Results = baseQueryModel.Results,
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

        //修改個人資料
        public BaseResponseModel PatchMemberInfo(PatchMemberInfoReqModel Req)
        {
            try
            {
                //先取得帳號存在 及資料
                var baseQueryModel = _memberRepository.GetMemberInfo(Req.UID);
                var memberInfo = baseQueryModel.Results.FirstOrDefault();

                //預修改資料
                memberInfo.NickName = Req.NickName;
                memberInfo.PhoneNumber = Req.PhoneNumber;
                memberInfo.Email = Req.Email;
                memberInfo.UpdateTime = DateTime.Now;

                //修改資料並回傳
                var result = _memberRepository.PatchMemberInfo(memberInfo);
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

        //修改密碼前取得是否有帳號
        public BaseResponseModel GetMemberInfo(string account)
        {
            try
            {
                var memberInfo = _memberRepository.GetMemberInfo(account);
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
