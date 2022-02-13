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
                var shopcarList = _shopCarRepository.GetUserShopcartList(UID);
                if(shopcarList != null)
                {
                    //找同一份商品在購物車內數量
                    var shopCar_product = shopcarList.FirstOrDefault(x => x.product_id == productId);
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
                    Message = "新增商品完成",
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
                var ShopcartList = _shopCarRepository.GetUserShopcartList(UID);
                return new BaseQueryModel<ShopcarListConditionModel>
                {
                    Results = ShopcartList,
                    Message = "取得清單",
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

        /// <summary>
        /// 廢棄用
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int UID)
        {
            try
            {
                return _shopCarRepository.User_shopcart_listByView(UID);
                //return result;
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<MemberShopcar>()
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
                    Message = "更新完畢",
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
