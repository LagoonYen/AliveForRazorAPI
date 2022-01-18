using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface MemberService
    {
        Task<BaseResponseModel> PostMemberRegister(string account, string password);
        Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account);
        Task PatchMemberInfo(string account, string Pwd);
        //Task PostMemberResetPwdSendMail(string Account);
    }
}
