using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ShopCarRepository
    {
        public void AddToCart(ProductShopcar ProductShopcar);

        public BaseQueryModel<ShopcarListConditionModel> GetUserShopcartList(int uid);

        /// <summary>
        /// 廢棄用
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid);

        /// <summary>
        /// 刪除單項商品
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel DelFromCart(DelFromCartReqModel Req);

        /// <summary>
        /// 修改購物車商品數量
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel UpsertCart(UpsertCartReqModel Req);

        /// <summary>
        /// 清空購物車
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public BaseResponseModel CleanShopcar(int Uid);
    }
}
