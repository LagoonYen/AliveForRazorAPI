using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface ShopCarService
    {
        public Task<BaseResponseModel> AddToShopCar(ShopCarReqModel ShopCarReqModel);
    }
}
