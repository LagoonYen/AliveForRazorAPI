using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public class MemberServiceImpl : MemberService
    {
        public readonly MemberRepository _memberRepository;
        
        public MemberServiceImpl(MemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        //登錄
        public async Task<BaseQueryModel<MemberInfo>> PostLogin(LoginReqModel Req)
        {
            var account = Req.Account;
            var password = Req.Password;
            //先取得帳號存在
            var baseQueryModel = await _memberRepository.GetMemberInfo(account);
            if(baseQueryModel.Results == null)
            {
                return baseQueryModel;
            }
            var dbPassword = baseQueryModel.Results.FirstOrDefault(x => x.Account == account).Password;
            if(dbPassword != password)
            {
                return new BaseQueryModel<MemberInfo>()
                {
                    Results = null,
                    Message = "密碼不同",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            return baseQueryModel;
        }

        //註冊
        public async Task<BaseResponseModel> PostMemberRegister(LoginReqModel Req)
        {
            try
            {
                var account = Req.Account;
                var password = Req.Password;
                //先取得帳號不存在
                var baseQueryModel = await _memberRepository.GetMemberInfo(account);
                if(baseQueryModel.Results != null)
                {
                    throw new Exception("此帳號已被註冊過");
                }
                var TimeNow = DateTime.Now;
                MemberInfo member = new MemberInfo();
                member.Account = member.Email = account;
                member.Password = password;
                member.RegisterTime = member.UpdateTime = TimeNow;
                //創建帳號
                await _memberRepository.PostMemberRegister(member);
                return new BaseResponseModel
                {
                    Message = "註冊成功",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        //讀取個人資料
        public async Task<BaseQueryModel<MemberInfo>> GetMemberInfo(int id)
        {
            try
            {
                var baseQueryModel = await _memberRepository.GetMemberInfo(id);
                if(baseQueryModel.Results == null)
                {
                    throw new Exception(baseQueryModel.Message);
                }
                var Data = baseQueryModel.Results.FirstOrDefault();
                Data.RegisterTime = Data.RegisterTime.Date;
                Data.UpdateTime = Data.UpdateTime.Date;
                return baseQueryModel;
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
        public async Task<BaseResponseModel> PatchMemberInfo(PatchMemberInfoReqModel Req)
        {
            try
            {
                int id = Req.Id;
                //先取得帳號存在
                var baseQueryModel = await _memberRepository.GetMemberInfo(id);
                if (baseQueryModel.Results == null)
                {
                    throw new Exception(baseQueryModel.Message);
                }
                var memberInfo = baseQueryModel.Results.FirstOrDefault();
                var DateTimeNow = DateTime.Now;
                memberInfo.NickName = Req.NickName;
                memberInfo.PhoneNumber = Req.PhoneNumber;
                memberInfo.Email = Req.Email;
                memberInfo.UpdateTime = DateTimeNow;
                return await _memberRepository.PatchMemberInfo(memberInfo);
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
        public async Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account)
        {
            //try
            //{
                var memberInfo = await _memberRepository.GetMemberInfo(account);
                //if(memberInfo.Result == null)
                //{
                //    return memberInfo;
                //}
                return memberInfo;
            //}
            //catch(Exception ex)
            //{
            //    return null;
            //}
           
        }
    }
}
