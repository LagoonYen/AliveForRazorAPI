using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductNum { get; set; }
        public int ProductPrice { get; set; }
        public byte? Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
