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

    public class OrderDetailResponseModel : OrderList
    {
        public class OrderList : OrderProduct
        {
           
        }
    }
}
