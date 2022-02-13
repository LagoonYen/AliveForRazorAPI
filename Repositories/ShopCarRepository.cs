using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ShopCarRepository
    {
        public void AddToCart(ProductShopcar ProductShopcar);

        public List<ShopcarListConditionModel> GetUserShopcartList(int uid);

        /// 廢棄用
        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid);

        public void DelFromCart(DelFromCartReqModel Req);

        
        public void UpsertCart(UpsertCartReqModel Req);

        public void CleanShopcar(int Uid);
    }
}
