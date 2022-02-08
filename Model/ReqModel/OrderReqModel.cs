using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model.ReqModel
{
    public class ToOrderReqModel
    {
        public int Uid { get; set; }
        public class OrderDetail
        {
            public int ProductId { get; set; }
            public int ProductNum { get; set; }
            public int ProductPrice { get; set; }
        }
        public string OrderName { get; set; }
        public string OrderPhoneNumber { get; set; }
        public string OrderCity { get; set; }
        public string OrderTown { get; set; }
        public string OrderAddress { get; set; }
        public string Remark { get; set; }
    }

    public class OrderDetailReqModel
    {
        public int OrderId { get; set; }
    }

    public class OrderDetailResponseModel
    {
        public string OrderNumber { get; set; }
        public int Uid { get; set; }
        public string Recipient { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientCity { get; set; }
        public string RecipientTown { get; set; }
        public string RecipientAddress { get; set; }
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
        public List<OrderProduct> Products { get; set; }
    }
}
