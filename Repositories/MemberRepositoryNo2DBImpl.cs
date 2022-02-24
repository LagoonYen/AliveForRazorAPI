using AliveStoreTemplate.Model;
using No2DB.Base;
using System;
using System.Linq;

namespace AliveStoreTemplate.Repositories
{
    public class MemberRepositoryNo2DBImpl : MemberRepository
    {
        /// <summary>
        /// 取得帳號資料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public MemberInfo GetMemberInfo(string account)
        {
            try
            {
                var collection = new DRole("Member");
                var allUsers = collection.Query<MemberInfo>("UserInfo").AllDatas();
                var user = allUsers.FirstOrDefault(x => x.Account == account);
                if (user == null)
                {
                    throw new Exception("此帳號未被註冊!");
                }
                return user;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 利用UID取得MemberInfo
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public MemberInfo GetMemberInfo(int UID)
        {
            try
            {
                var collection = new DRole("Member");
                var allUsers = collection.Query<MemberInfo>("UserInfo").AllDatas();
                var user = allUsers.FirstOrDefault(x => x.Id == UID);
                if (user == null)
                {
                    throw new Exception("此帳號未被註冊!");
                }
                return user;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 利用account尋找該帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsMemberExist(string account)
        {
            try
            {
                var collection = new DRole("Member");
                var allUsers = collection.Query<MemberInfo>("UserInfo").AllDatas();
                if(allUsers == null)
                {
                    return false;
                }
                var user = allUsers.FirstOrDefault(x => x.Account == account);
                if(user == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 修改帳號資訊
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public void PatchMemberInfo(MemberInfo member)
        {
            try
            {
                No2DB.Transaction.Operator op = new No2DB.Transaction.Operator("aaa");
                var collection = new DRole("Member");
                var user = collection.Query<MemberInfo>("UserInfo").DataByKey(member.Id + "");
                
                if (user == null)
                {
                    throw new Exception("找不到該帳號資料");
                }

                user.Password = member.Password;
                user.NickName = member.NickName;
                user.PhoneNumber = member.PhoneNumber;
                user.Email = member.Email;
                user.ProfilePhotoUrl = member.ProfilePhotoUrl;
                user.UpdateTime = member.UpdateTime;

                op.Update(collection, "UserInfo", user.Id + "", user);
                op.Done();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 建立帳號
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public void PostMemberRegister(MemberInfo member)
        {
            try
            {
                var collection = new DRole("Member");
                var DataKeys = collection.Query<ProductList>("UserInfo").DataCount();
                member.Id = DataKeys + 1;
                collection.GetOp("UserInfo").Update(member.Id + "", member);
            }
            catch
            {
                throw;
            }
        }
    }
}
