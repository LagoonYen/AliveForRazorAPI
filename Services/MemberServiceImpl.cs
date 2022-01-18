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

        public async Task<BaseResponseModel> PostMemberRegister(string account, string password)
        {
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

        public async Task PatchMemberInfo(string account, string password)
        {
            var memberInfo = await _memberRepository.GetMemberInfo(account);
            if (memberInfo == null)
            {
                return;
            }
            var memberInfoString = memberInfo.Results.FirstOrDefault();
            var DateTimeNow = DateTime.Now;
            memberInfoString.Password = password;
            memberInfoString.UpdateTime = DateTimeNow;
            await _memberRepository.PatchMemberInfo(memberInfoString);
        }

        //public async Task PostMemberResetPwdSendMail(string Account)
        //{
        //    await _memberRepository.PostMemberResetPwdSendMail(Account);
        //}

        public Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account)
        {
            return _memberRepository.GetMemberInfo(account);
        }
    }
}
