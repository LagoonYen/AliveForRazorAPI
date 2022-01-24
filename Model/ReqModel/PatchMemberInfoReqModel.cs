using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class PatchMemberInfoReqModel
    {
        [EmailAddress]
        [Display(Name = "電子郵件帳號")]
        public string Account { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "暱稱")]
        public string NickName { get; set; }

        [Display(Name = "電話號碼")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "信箱")]
        public string Email { get; set; }
    }
}
