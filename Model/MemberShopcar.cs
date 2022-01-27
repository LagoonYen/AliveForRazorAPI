using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class MemberShopcar
    {
        public int Uid { get; set; }
        public int ProductId { get; set; }
        public string CardName { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public string ImgUrl { get; set; }
        public int Num { get; set; }
    }
}
