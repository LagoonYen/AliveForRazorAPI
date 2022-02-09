using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public class MemberRepositoryImpl : MemberRepository
    {
        private readonly ShopContext _dbShop;

        public MemberRepositoryImpl(ShopContext shopContext)
        {
            _dbShop = shopContext;
        }

        public BaseQueryModel<MemberInfo> GetMemberInfo(string account)
        {
            try
            {
                var member = _dbShop.MemberInfos.FirstOrDefault(x => x.Account == account);
                if (member == null)
                {
                    return new BaseQueryModel<MemberInfo>
                    {
                        //初始化
                        Results = null,
                        Message = "此帳號尚未被註冊",
                        StatusCode = HttpStatusCode.OK,
                    };
                }
                return new BaseQueryModel<MemberInfo>
                {
                    //初始化
                    Results = new List<MemberInfo> { member },
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch
            {
                throw;
            }
        }

        //建立帳號
        public BaseResponseModel PostMemberRegister(MemberInfo member)
        {
            try
            {
                _dbShop.MemberInfos.Add(member);
                _dbShop.SaveChanges();
                return new BaseResponseModel
                {
                    StatusCode = (HttpStatusCode)200  //弱轉型
                    //StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseQueryModel<MemberInfo> GetMemberInfo(int UID)
        {
            try
            {
                var member = _dbShop.MemberInfos.Find(UID);
                if(member == null)
                {
                    throw new Exception("此帳號未被註冊!");
                }
                return new BaseQueryModel<MemberInfo>
                {
                    //初始化
                    Results = new List<MemberInfo> { member },
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseResponseModel PatchMemberInfo(MemberInfo member)
        {
            try
            {
                var dbmember = _dbShop.MemberInfos.Find(member.Id);
                dbmember.Password = member.Password;
                dbmember.NickName = member.NickName;
                dbmember.PhoneNumber = member.PhoneNumber;
                dbmember.Email = member.Email;
                dbmember.ProfilePhotoUrl = member.ProfilePhotoUrl;
                dbmember.UpdateTime = member.UpdateTime;
                _dbShop.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "資料已變更",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
