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
        public readonly ShopContext _dbShop;

        public MemberRepositoryImpl(ShopContext shopContext)
        {
            _dbShop = shopContext;
        }

        public async Task<BaseQueryModel<MemberInfo>> GetMemberInfo(string account)
        {
            try
            {
                MemberInfo member = new MemberInfo();
                member = _dbShop.MemberInfos.FirstOrDefault(x => x.Account == account);
                if (member == null)
                {
                    throw new Exception("此帳號未被註冊!");
                }
                member = await _dbShop.MemberInfos.FindAsync(member.Id);
                return new BaseQueryModel<MemberInfo>
                {
                    //初始化
                    Results = new List<MemberInfo> { member },
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch(Exception ex)
            {
                return new BaseQueryModel<MemberInfo>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.OK
                };
            }
        }

        //建立帳號
        public async Task<BaseResponseModel> PostMemberRegister(MemberInfo member)
        {
            try
            {
                await _dbShop.MemberInfos.AddAsync(member);
                await _dbShop.SaveChangesAsync();
                return new BaseResponseModel
                {
                    StatusCode = (HttpStatusCode)200  //弱轉型
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<BaseQueryModel<MemberInfo>> GetMemberInfo(int id)
        {
            try
            {
                //MemberInfo member = new MemberInfo();
                var member = await _dbShop.MemberInfos.FindAsync(id);
                if(member == null)
                {
                    throw new Exception("找不到此帳號!");
                }
                return new BaseQueryModel<MemberInfo>
                {
                    //初始化
                    Results = new List<MemberInfo> { member },
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<MemberInfo>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<BaseResponseModel> PatchMemberInfo(MemberInfo member)
        {
            try
            {
                var dbmember = await _dbShop.MemberInfos.FindAsync(member.Id);
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
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task PatchMemberUpdate(MemberInfo member)
        {
            try
            {
                var dbmember = await _dbShop.MemberInfos.FindAsync(member.Id);
                if (dbmember == null)
                {
                    throw new Exception("找不到此帳號!資料更新不全");
                }
                dbmember.NickName = member.NickName;
                dbmember.PhoneNumber = member.PhoneNumber;
                dbmember.Email = member.Email;
                dbmember.ProfilePhotoUrl = member.ProfilePhotoUrl;
                dbmember.UpdateTime = member.UpdateTime;
                await _dbShop.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
