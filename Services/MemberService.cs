using AliveStoreTemplate.Model;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface MemberService
    {
        Task PostMemberRegister(string ACCT, string Pwd);
        Task PatchPwdUpdate(int id, string Pwd);
        //Task PostMemberResetPwdSendMail(string Account);
    }
}
