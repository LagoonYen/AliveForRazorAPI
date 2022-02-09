using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;

namespace AliveStoreTemplate.Services
{
    public interface ShopCarService
    {
        public BaseResponseModel AddToCart(AddToCartReqModel Req);
        public BaseQueryModel<shopcar_list_respModel> User_shopcart_list(int uid);
        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid);
        public BaseResponseModel DelFromCart(DelFromCartReqModel Req);
        public BaseResponseModel UpsertCart(UpsertCartReqModel Req);
    }
}
