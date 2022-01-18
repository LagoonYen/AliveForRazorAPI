using AliveStoreTemplate.Model;
using AliveStoreTemplate.Repositories;
using System;
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

        public async Task PostMemberRegister(string ACCT, string Pwd)
        {
            var TimeNow = DateTime.Now;
            await _memberRepository.PostMemberRegister(ACCT, Pwd, TimeNow);
        }

        public async Task PatchPwdUpdate(int id, string Pwd)
        {
            var memberInfo = await _memberRepository.GetMemberInfo(id);
            if (memberInfo == null)
            {
                return;
            }
            var DateTimeNow = DateTime.Now;
            memberInfo.Password = Pwd;
            memberInfo.UpdateTime = DateTimeNow;
            await _memberRepository.PatchMemberInfo(memberInfo);
            //var TimeNow = DateTime.Now;
            //member.UpdateTime = TimeNow;
            //await _memberRepository.PatchMemberUpdate(member);
        }

        //public async Task PostMemberResetPwdSendMail(string Account)
        //{
        //    await _memberRepository.PostMemberResetPwdSendMail(Account);
        //}
    }
}
