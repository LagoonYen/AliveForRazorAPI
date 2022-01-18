using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class MemberInfo
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
