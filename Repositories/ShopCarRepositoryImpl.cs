using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using System.Net;


namespace AliveStoreTemplate.Repositories
{
    public class ShopCarRepositoryImpl : ShopCarRepository
    {
        private readonly ShopContext _shopContext;

        public ShopCarRepositoryImpl(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public BaseResponseModel AddToCart(ProductShopcar ProductShopcar)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == ProductShopcar.Uid).FirstOrDefault(x => x.ProductId == ProductShopcar.ProductId);
                if (result != null)
                {
                    result.Num = ProductShopcar.Num;
                    result.UpdateTime = ProductShopcar.UpdateTime;
                    _shopContext.SaveChanges();
                    return new BaseResponseModel()
                    {
                        Message = "已更新購物車",
                        StatusCode = HttpStatusCode.OK
                    };
                };

                _shopContext.ProductShopcars.Add(ProductShopcar);
                _shopContext.SaveChanges();
                return new BaseResponseModel()
                {
                    Message = "已加入購物車",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        //public BaseResponseModel UpdateCart(ProductShopcar ProductShopcar)
        //{
            
        //}

        public BaseQueryModel<ProductShopcar> GetUserShopCarList(int uid)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == uid).ToList();
                return new BaseQueryModel<ProductShopcar>
                {
                    Results = result,
                    Message = string.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }

        }
    }
}
