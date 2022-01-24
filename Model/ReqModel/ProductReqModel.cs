namespace AliveStoreTemplate.Model.ReqModel
{
    public class ProductListReqModel
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }

    public class ProductSearchRespModel
    {
        public int ProductId { get; set; }
        public int ProductNum { get; set; }
    }
    


}
