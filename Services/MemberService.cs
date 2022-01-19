using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface MemberService
    {
        //登錄帳戶
        Task<BaseQueryModel<MemberInfo>> PostLogin(string account, string password);
        
        //會員註冊
        Task<BaseResponseModel> PostMemberRegister(string account, string password);

        //修改密碼前取得是否有帳號
        Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account);

        //讀取個人資料
        Task<BaseQueryModel<MemberInfo>> GetMemberInfo(int id);

        //修改密碼
        Task<BaseResponseModel> PatchMemberInfo(string account, string Pwd);
    }
}
