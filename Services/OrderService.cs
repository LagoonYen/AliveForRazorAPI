using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;

namespace AliveStoreTemplate.Services
{
    public interface OrderService
    {
        public BaseResponseModel ToOrder(ToOrderReqModel Req);
        public BaseQueryModel<OrderList> GetOrderList(int id);
        public BaseQueryModel<OrderDetailResponseModel> GetOrderDetail(OrderDetailReqModel Req);

        
    }
}
