using System;
using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class AddToCartReqModel
    {
        [Required]
        public int Uid { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int num { get; set; }
    }

    public class shopcar_list_respModel
    {
        public int uid { get; set; }
        public int product_id { get; set; }
        public string cardName { get; set; }
        public int num { get; set; }
        public string imgUrl { get; set; }
        public int price { get; set; }
        public int inventory { get; set; }
        public int total { get; set; }
    }

    public class DelFromCartReqModel
    {
        [Required]
        public int uid { get; set; }

        [Required]
        public int product_id { get; set; }
    }

    public class UpsertCartReqModel
    {
        [Required]
        public int uid { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int num { get; set; }
        
        public DateTime UpdateTime { get; set; }
    }

    public class UIDReqModel
    {
        public int UID { get; set; }
    }

}
