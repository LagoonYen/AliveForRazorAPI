using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
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
                int uid = Req.Uid;
                int product_id = Req.product_id;
                int num = Req.num;
                var time = DateTime.Now;

                //商品剩餘數量
                int product_inventory = _productRepository.GetProductInfo(product_id).Results.FirstOrDefault().Inventory;

                //購物車內數量
                var result = _shopCarRepository.User_shopcart_list(uid);
                if(result.Results != null)
                {
                    var shopCar_product = result.Results.FirstOrDefault(x => x.product_id == product_id);
                    if(shopCar_product != null)
                    {
                        int shopCar_product_inventory = shopCar_product.num;
                        if ((num + shopCar_product_inventory) > product_inventory)
                        {
                            throw new Exception("商品數量不足");
                        }
                        //更新購物車
                        num += shopCar_product_inventory;
                    }
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

        public BaseQueryModel<shopcar_list_respModel> User_shopcart_list(int uid)
        {
            try
            {
                return _shopCarRepository.User_shopcart_list(uid);
                //return result;
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<shopcar_list_respModel>()
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid)
        {
            try
            {
                return _shopCarRepository.User_shopcart_listByView(uid);
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
                return _shopCarRepository.DelFromCart(Req);
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
                return _shopCarRepository.UpsertCart(Req);
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
