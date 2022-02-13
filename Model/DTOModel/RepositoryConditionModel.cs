using System;

namespace AliveStoreTemplate.Model.DTOModel
{
    public class AddressUpserConditionModel
    {
        public int Uid { get; set; }
        public string OrderName { get; set; }
        public string OrderPhoneNumber { get; set; }
        public string OrderCity { get; set; }
        public string OrderTown { get; set; }
        public string OrderAddress { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class ShopcarListConditionModel
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
