using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface MemberService
    {
        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="Req">帳密</param>
        /// <returns></returns>
        BaseResponseModel PostMemberRegister(LoginReqModel Req);

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="Req">帳密</param>
        /// <returns></returns>
        BaseQueryModel<MemberInfo> PostLogin(LoginReqModel Req);

        /// <summary>
        /// 同上會員登入
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        BaseQueryModel<MemberInfo> PostLogin(string Account ,string Password);

        /// <summary>
        /// 讀取個人資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseQueryModel<MemberInfo> GetMemberInfo(int id);

        /// <summary>
        /// 修改個人資訊
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        BaseResponseModel PatchMemberInfo(PatchMemberInfoReqModel Req);

        /// <summary>
        /// 忘記密碼前確認是否有帳號 並寄送驗證信件
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        BaseResponseModel GetMemberInfo(string account);
    }
}
