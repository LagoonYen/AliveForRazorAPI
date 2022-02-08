using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface MemberRepository
    {
        //帳號取得登入資訊
        BaseQueryModel<MemberInfo> GetMemberInfo(string account);

        //註冊帳號
        BaseResponseModel PostMemberRegister(MemberInfo member);

        //讀取個人資料
        BaseQueryModel<MemberInfo> GetMemberInfo(int UID);

        //修改個人資料
        BaseResponseModel PatchMemberInfo(MemberInfo member);
    }
}
