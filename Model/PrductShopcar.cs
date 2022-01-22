using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class PrductShopcar
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public int PrductId { get; set; }
        public int Num { get; set; }
        public byte? Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
