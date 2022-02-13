using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;

namespace AliveStoreTemplate.Services
{
    public interface ShopCarService
    {
        /// <summary>
        /// 新增至購物車
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel AddToCart(AddToCartReqModel Req);
        
        /// <summary>
        /// 讀取購物車清單
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public BaseQueryModel<ShopcarListConditionModel> User_shopcart_list(int uid);
        
        /// <summary>
        /// 廢棄用
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid);

        /// <summary>
        /// 刪除某一項購物車清單
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel DelFromCart(DelFromCartReqModel Req);

        /// <summary>
        /// 更新購物車清單
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel UpsertCart(UpsertCartReqModel Req);
    }
}
