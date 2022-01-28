using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class OrderList
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int Uid { get; set; }
        public int? AddressId { get; set; }
        public string Remark { get; set; }
        public int? PayPrice { get; set; }
        public byte? IsPay { get; set; }
        public int? PayTime { get; set; }
        public byte? IsShip { get; set; }
        public int? ShipTime { get; set; }
        public byte? IsReceipt { get; set; }
        public int? ReceiptTime { get; set; }
        public string ShipNumber { get; set; }
        public byte? Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
