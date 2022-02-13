using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface MemberRepository
    {
        public bool IsMemberExist(string account);

        public MemberInfo GetMemberInfo(string account);

        public void PostMemberRegister(MemberInfo member);

        public MemberInfo GetMemberInfo(int UID);

        public void PatchMemberInfo(MemberInfo member);
    }
}
