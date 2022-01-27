using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;

namespace AliveStoreTemplate.Services
{
    public interface OrderService
    {
        public BaseResponseModel ToOrder(ToOrderReqModel Req);

    }
}
