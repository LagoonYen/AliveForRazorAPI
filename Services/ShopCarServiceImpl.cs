using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace AliveStoreTemplate.Services
{
    public class ShopCarServiceImpl :ShopCarService
    {
        //購物車Repository
        private readonly ShopCarRepository _shopCarRepository;
        private readonly ProductRepository _productRepository;
        public ShopCarServiceImpl(ShopCarRepository shopCarRepository, ProductRepository productRepository)
        {
            _shopCarRepository = shopCarRepository;
            _productRepository = productRepository;
        }

        public BaseResponseModel AddToCart(AddToCartReqModel Req)
        {
            try
            {
                int uid = Req.uid;
                int product_id = Req.product_id;
                int num = Req.num;

                var time = DateTime.Now;

                //商品剩餘數量
                int product_inventory = _productRepository.Product_Info(product_id).Results.FirstOrDefault().Inventory;

                //購物車內數量
                var shopCar_product = _shopCarRepository.GetUserShopCarList(uid).Results.FirstOrDefault(x => x.ProductId == product_id);

                if(shopCar_product != null)
                {
                    int shopCar_product_inventory = shopCar_product.Num;
                    if ((num + shopCar_product_inventory) > product_inventory)
                    {
                        throw new Exception("商品數量不足");
                    }
                    //更新購物車
                    num += shopCar_product_inventory;
                }

                ProductShopcar PostNewShopCar = new()
                {
                    Uid = uid,
                    ProductId = product_id,
                    Num = num,
                    CreateTime = time,
                    UpdateTime = time
                };
                return _shopCarRepository.AddToCart(PostNewShopCar);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel()
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
