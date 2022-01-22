using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class ShopCarReqModel
    {
        [Required]
        public int Uid { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Num { get; set; }
    }
}
