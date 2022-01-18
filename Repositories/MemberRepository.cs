using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface MemberRepository
    {
        Task<BaseResponseModel> PostMemberRegister(MemberInfo member);
        //Task PatchMemberUpdate(MemberInfo member);
        //Task<Exception> GetMemberInfoByAccount(string Account);
        Task<MemberInfo> GetMemberInfo(int id);
        Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account);
        Task PatchMemberInfo(MemberInfo member);

    }
}
