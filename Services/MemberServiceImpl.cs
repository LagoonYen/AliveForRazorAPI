using AliveStoreTemplate.Model;
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
        public async Task<BaseQueryModel<MemberInfo>> PostLogin(string account, string password)
        {
            var baseQueryModel =  await _memberRepository.GetMemberInfo(account);
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
        public async Task<BaseResponseModel> PostMemberRegister(string account, string password)
        {
            //先取得帳號不存在
            var memberInfo = await _memberRepository.GetMemberInfo(account);
            try
            {
                if(memberInfo.Results != null)
                {
                    throw new Exception("此帳號已被註冊過");
                }
                var TimeNow = DateTime.Now;
                MemberInfo member = new MemberInfo();
                member.Account = account;
                member.Password = password;
                member.RegisterTime = member.UpdateTime = TimeNow;
                //創建帳號
                await _memberRepository.PostMemberRegister(member);
                return new BaseResponseModel
                {
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

        //修改密碼
        public async Task<BaseResponseModel> PatchMemberInfo(string account, string password)
        {
            try
            {
                //先取得帳號存在
                var memberInfo = await _memberRepository.GetMemberInfo(account);
                if (memberInfo.Results == null)
                {
                    throw new Exception(memberInfo.Message);
                }
                var memberInfoString = memberInfo.Results.FirstOrDefault();
                var DateTimeNow = DateTime.Now;
                memberInfoString.Password = password;
                memberInfoString.UpdateTime = DateTimeNow;
                return await _memberRepository.PatchMemberInfo(memberInfoString);
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
        public Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account)
        {
            //try
            //{
                var memberInfo =  _memberRepository.GetMemberInfo(account);
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

        //讀取個人資料
        public Task<BaseQueryModel<MemberInfo>> GetMemberInfo(int id)
        {
            return _memberRepository.GetMemberInfo(id);
        }
    }
}
