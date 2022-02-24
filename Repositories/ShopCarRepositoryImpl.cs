using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System;
using AliveStoreTemplate.Model.DTOModel;
using System.Collections.Generic;

namespace AliveStoreTemplate.Repositories
{
    public class ShopCarRepositoryImpl : ShopCarRepository
    {
        private readonly ShopContext _shopContext;

        public ShopCarRepositoryImpl(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        /// <summary>
        /// 新增商品至購物車
        /// </summary>
        /// <param name="ProductShopcar"></param>
        /// <returns></returns>
        public void AddToCart(ProductShopcar ProductShopcar)
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
                };

                //無同商品 新增
                _shopContext.ProductShopcars.Add(ProductShopcar);
                _shopContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 讀取購物車清單
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<ShopcarListConditionModel> GetUserShopcartList(int uid)
        {
            try
            {
                //製作ShopcarListConditionModel
                var dbData = _shopContext.ProductShopcars.Where(x => x.Uid == uid).Join(_shopContext.ProductLists,
                    x => x.ProductId,
                    o => o.Id,
                    (x, o) => new ShopcarListConditionModel { uid = x.Uid, product_id = x.ProductId, cardName = o.CardName, num = x.Num, imgUrl = o.ImgUrl, price = o.Price, inventory = o.Inventory }).ToList();
                return dbData;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 刪除單項商品
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public void DelFromCart(DelFromCartReqModel Req)
        {
            try
            {
                var result = _shopContext.ProductShopcars.Remove(_shopContext.ProductShopcars.Where(x => x.Uid == Req.uid).FirstOrDefault(x => x.ProductId == Req.product_id));
                if (result == null)
                {
                    throw new Exception("查無此商品");
                }
                _shopContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 修改購物車商品數量
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public void UpsertCart(UpsertCartReqModel Req)
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
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 清空購物車
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public void CleanShopcar(int Uid)
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
            }
            catch
            {
                throw;
            }
        }
    }
}
