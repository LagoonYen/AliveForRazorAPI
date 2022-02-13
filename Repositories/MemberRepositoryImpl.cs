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

        /// <summary>
        /// 利用account尋找該帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsMemberExist(string account)
        {
            try
            {
                var member = _dbShop.MemberInfos.FirstOrDefault(x => x.Account == account);
                if (member == null)
                {
                    return false; //此帳號尚未被註冊
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 取得帳號資料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public MemberInfo GetMemberInfo(string account)
        {
            try
            {
                var member = _dbShop.MemberInfos.FirstOrDefault(x => x.Account == account);
                return member;
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
                _dbShop.MemberInfos.Add(member);
                _dbShop.SaveChanges();
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
                var member = _dbShop.MemberInfos.Find(UID);
                if(member == null)
                {
                    throw new Exception("此帳號未被註冊!");
                }
                return member;
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
                var dbmember = _dbShop.MemberInfos.Find(member.Id);
                if(dbmember == null)
                {
                    throw new Exception("找不到該帳號資料");
                }
                dbmember.Password = member.Password;
                dbmember.NickName = member.NickName;
                dbmember.PhoneNumber = member.PhoneNumber;
                dbmember.Email = member.Email;
                dbmember.ProfilePhotoUrl = member.ProfilePhotoUrl;
                dbmember.UpdateTime = member.UpdateTime;
                _dbShop.SaveChanges();
                //回傳狀態碼
            }
            catch
            {
                throw;
            }
        }
    }
}
