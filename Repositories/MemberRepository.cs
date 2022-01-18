using AliveStoreTemplate.Model;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface MemberRepository
    {
        Task PostMemberRegister(string ACCT, string Pwd, DateTime TimeNow);
        //Task PatchMemberUpdate(MemberInfo member);
        //Task<Exception> GetMemberInfoByAccount(string Account);
        Task<MemberInfo> GetMemberInfo(int id);
        Task<MemberInfo> GetMemberInfo(string account);
        Task PatchMemberInfo(MemberInfo member);

    }
}
