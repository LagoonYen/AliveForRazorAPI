using System;
using System.ComponentModel.DataAnnotations;

namespace AliveStoreTemplate.Model.ReqModel
{
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
}
