using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class AddToCartReqModel
    {
        [Required]
        public int uid { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int num { get; set; }
    }
}
