using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ShopCarRepository
    {
        public BaseResponseModel AddToCart(ProductShopcar ProductShopcar);
        //public BaseResponseModel UpdateCart(ProductShopcar ProductShopcar);
        public BaseQueryModel<ProductShopcar> GetUserShopCarList(int uid);
    }
}
