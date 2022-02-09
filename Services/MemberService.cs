using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface MemberService
    {
        //會員註冊
        BaseResponseModel PostMemberRegister(LoginReqModel Req);

        //登入會員
        BaseQueryModel<MemberInfo> PostLogin(LoginReqModel Req);

        //登入會員
        BaseQueryModel<MemberInfo> PostLogin(string Account ,string Password);

        //讀取個人資料
        BaseQueryModel<MemberInfo> GetMemberInfo(int id);

        //修改個人資料
        BaseResponseModel PatchMemberInfo(PatchMemberInfoReqModel Req);

        //修改密碼前取得是否有帳號
        BaseResponseModel GetMemberInfo(string account);

        //修改密碼
        //BaseResponseModel PatchPassword()
    }
}
