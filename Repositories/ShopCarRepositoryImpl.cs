using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System;

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

        public BaseQueryModel<shopcar_list_respModel> User_shopcart_list(int uid)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == uid).Join(_shopContext.ProductLists,
                    x => x.ProductId,
                    o => o.Id,
                    (x, o) => new shopcar_list_respModel { uid = x.Uid, product_id = x.ProductId, cardName = o.CardName, num = x.Num, imgUrl = o.ImgUrl, price = o.Price, inventory = o.Inventory }).ToList();
                if (result.Count ==0 )
                {
                    return new BaseQueryModel<shopcar_list_respModel>
                    {
                        Results = null,
                        Message = "目前購物車無商品",
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new BaseQueryModel<shopcar_list_respModel>
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

        public BaseQueryModel<MemberShopcar> User_shopcart_listByView(int uid)
        {
            try
            {
                var result = _shopContext.MemberShopcars.Where(x => x.Uid == uid).ToList();
                if (result.Count == 0)
                {
                    return new BaseQueryModel<MemberShopcar>
                    {
                        Results = null,
                        Message = "目前購物車無商品",
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new BaseQueryModel<MemberShopcar>
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
        public BaseResponseModel DelFromCart(DelFromCartReqModel Req)
        {
            try
            {
                _shopContext.ProductShopcars.Remove(_shopContext.ProductShopcars.Where(x => x.Uid == Req.uid).FirstOrDefault(x => x.ProductId == Req.product_id));
                _shopContext.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "資料已刪除",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                 return new BaseResponseModel
                 {
                     Message = ex.Message,
                     StatusCode = HttpStatusCode.BadRequest,
                 };
            }
        }

        public BaseResponseModel UpsertCart(UpsertCartReqModel Req)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == Req.uid).FirstOrDefault(x => x.ProductId == Req.product_id);
                result.UpdateTime = Req.UpdateTime;
                result.Num = Req.num;
                _shopContext.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "資料已更新",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        }

        public BaseResponseModel CleanShopcar(int Uid)
        {
            var result = _shopContext.ProductShopcars.Where(x => x.Uid == Uid);
            _shopContext.ProductShopcars.RemoveRange(result);
            _shopContext.SaveChanges();

            return new BaseResponseModel
            {
                Message = "已清空購物車",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
