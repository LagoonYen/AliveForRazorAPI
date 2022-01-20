using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class ProductList
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string CardName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public string ImgUrl { get; set; }
        public DateTime RealseTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
