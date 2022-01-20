using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class PatchMemberInfoReqModel : LoginReqModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "暱稱")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "電話號碼")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "信箱")]
        public string Email { get; set; }
    }
}
