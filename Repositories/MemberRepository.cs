using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface MemberRepository
    {
        //註冊帳號
        Task<BaseResponseModel> PostMemberRegister(MemberInfo member);

        //讀取個人資料
        Task<BaseQueryModel<MemberInfo>> GetMemberInfo(int id);

        //修改密碼前取得是否有帳號
        Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account);

        //修改個人資料
        Task<BaseResponseModel> PatchMemberInfo(MemberInfo member);
    }
}
