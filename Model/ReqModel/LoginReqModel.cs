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
        [StringLength(15, ErrorMessage = "{0} 長度必須最少 {2} 以及最多 {1} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}
