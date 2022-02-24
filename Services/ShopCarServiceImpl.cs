using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using AliveStoreTemplate.Model.DTOModel;

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
                int UID = Req.Uid;
                int productId = Req.product_id;
                int num = Req.num;
                var time = DateTime.Now;

                //商品剩餘數量
                int productInventory = _productRepository.GetProductInfo(productId).Inventory;

                //叫出購物車清單
                var shopcartList = _shopCarRepository.GetUserShopcartList(UID);
                if(shopcartList != null)
                {
                    //找同一份商品在購物車內數量
                    var shopCar_product = shopcartList.FirstOrDefault(x => x.product_id == productId);
                    if(shopCar_product != null)
                    {
                        int shopCarProductInventory = shopCar_product.num;
                        if ((num + shopCarProductInventory) > productInventory)
                        {
                            throw new Exception("商品數量不足");
                        }
                        //更新購物車
                        num += shopCarProductInventory;
                    }
                }

                ProductShopcar PostNewShopCar = new ProductShopcar
                {
                    Uid = UID,
                    ProductId = productId,
                    Num = num,
                    CreateTime = time,
                    UpdateTime = time
                };
                _shopCarRepository.AddToCart(PostNewShopCar);
                return new BaseResponseModel
                {
                    Message = "新增完畢",
                    StatusCode = HttpStatusCode.OK
                };
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

        public BaseQueryModel<ShopcarListConditionModel> User_shopcart_list(int UID)
        {
            try
            {
                var shopcartList = _shopCarRepository.GetUserShopcartList(UID);
                if(shopcartList == null)
                {
                    throw new Exception("購物車內無商品");
                }
                return new BaseQueryModel<ShopcarListConditionModel>
                {
                    Results = shopcartList,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<ShopcarListConditionModel>()
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseResponseModel DelFromCart(DelFromCartReqModel Req)
        {
            try
            {
                _shopCarRepository.DelFromCart(Req);
                return new BaseResponseModel
                {
                    Message = "刪除完畢",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseResponseModel UpsertCart(UpsertCartReqModel Req)
        {
            try
            {
                Req.UpdateTime = DateTime.Now;
                _shopCarRepository.UpsertCart(Req);
                return new BaseResponseModel
                {
                    Message = "修改完畢",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
