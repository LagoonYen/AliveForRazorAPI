using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class LoginReqModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件帳號")]
        public string Account { get; set; }

        [Required]
        [StringLength(15 , ErrorMessage = "{0}必需擁有至少{1}個字元,最多{2}個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }

    public class PatchMemberInfoReqModel
    {
        [EmailAddress]
        [Display(Name = "電子郵件帳號")]
        public string Account { get; set; }

        [Display(Name = "UID")]
        public int UID { get; set; }

        [Display(Name = "暱稱")]
        public string NickName { get; set; }

        [Display(Name = "電話號碼")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "信箱")]
        public string Email { get; set; }
    }
}
