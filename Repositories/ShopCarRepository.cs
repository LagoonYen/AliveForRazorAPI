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
        public BaseQueryModel<shopcar_list_respModel> User_shopcart_list(int uid);

        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid);

        public BaseResponseModel DelFromCart(DelFromCartReqModel Req);
        public BaseResponseModel UpsertCart(UpsertCartReqModel Req);
    }
}
