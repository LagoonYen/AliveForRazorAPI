using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface ShopCarService
    {
        public BaseResponseModel AddToCart(AddToCartReqModel Req);
    }
}
