using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System;
using AliveStoreTemplate.Model.DTOModel;

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
                //找到USER下的購物清單
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == ProductShopcar.Uid).FirstOrDefault(x => x.ProductId == ProductShopcar.ProductId);
                //有同商品 更新
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

                //無同商品 新增
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

        public BaseQueryModel<ShopcarListConditionModel> GetUserShopcartList(int uid)
        {
            try
            {
                //製作ShopcarListConditionModel
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == uid).Join(_shopContext.ProductLists,
                    x => x.ProductId,
                    o => o.Id,
                    (x, o) => new ShopcarListConditionModel { uid = x.Uid, product_id = x.ProductId, cardName = o.CardName, num = x.Num, imgUrl = o.ImgUrl, price = o.Price, inventory = o.Inventory }).ToList();
                if (result.Count ==0 )
                {
                    return new BaseQueryModel<ShopcarListConditionModel>
                    {
                        Results = null,
                        Message = "目前購物車無商品",
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new BaseQueryModel<ShopcarListConditionModel>
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

        /// <summary>
        /// 廢棄用
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
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
                var result = _shopContext.ProductShopcars.Remove(_shopContext.ProductShopcars.Where(x => x.Uid == Req.uid).FirstOrDefault(x => x.ProductId == Req.product_id));
                if (result == null)
                {
                    throw new Exception("查無此商品");
                }
                _shopContext.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "資料已刪除",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseResponseModel UpsertCart(UpsertCartReqModel Req)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == Req.uid).FirstOrDefault(x => x.ProductId == Req.product_id);
                if (result == null)
                {
                    throw new Exception("查無此商品");
                }
                result.UpdateTime = Req.UpdateTime;
                result.Num = Req.num;
                _shopContext.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "資料已更新",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseResponseModel CleanShopcar(int Uid)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Where(x => x.Uid == Uid);
                if(result == null)
                {
                    throw new Exception("查無此商品");
                }
                _shopContext.ProductShopcars.RemoveRange(result);
                _shopContext.SaveChanges();

                return new BaseResponseModel
                {
                    Message = "已清空購物車",
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
