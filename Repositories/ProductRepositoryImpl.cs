using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public class ProductRepositoryImpl : ProductRepository
    {
        private readonly ShopContext _shopContext;

        public ProductRepositoryImpl(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public BaseQueryModel<ProductList> SearchProduct(string category, string subCategory)
        {
            try
            {
                //利用Category及SubCategory分類
                var productList = (category != "") ? 
                    (subCategory != "") ? 
                     _shopContext.ProductLists.Where(x => x.Category == category).Where(x => x.Subcategory == subCategory).ToList()
                    : _shopContext.ProductLists.Where(x => x.Category == category).ToList()
                    :  _shopContext.ProductLists.ToList();
                if(productList == null)
                {
                    throw new Exception("這一彈沒有此類卡片喔!");
                }
                return new BaseQueryModel<ProductList>
                {
                    Results = productList,
                    Message = string.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseQueryModel<ProductList> GetProductInfo(int id)
        {
            try
            {
                var productList = _shopContext.ProductLists.FirstOrDefault(x => x.Id == id);
                if (productList == null)
                {
                    throw new Exception("找不到此卡資訊");
                }
                return new BaseQueryModel<ProductList>
                {
                    //初始化
                    Results = new List<ProductList> { productList },
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseResponseModel PatchProduct(ProductList product)
        {
            try
            {
                var dbData = _shopContext.ProductLists.FirstOrDefault(x => x.Id == product.Id);
                if(dbData == null)
                {
                    throw new Exception ("修改不成功，未取得卡片正確資訊");
                }
                dbData.CardName = (dbData.CardName != product.CardName) ? product.CardName : dbData.CardName;
                dbData.Category = (dbData.Category != product.Category) ? product.Category : dbData.Category;
                dbData.Subcategory = (dbData.Subcategory != product.Subcategory) ? product.Subcategory : dbData.Subcategory;
                dbData.Description = (dbData.Description != product.Description) ? product.Description : dbData.Description;
                dbData.Price = (dbData.Price != product.Price) ? product.Price : dbData.Price;
                dbData.Inventory = (dbData.Inventory != product.Inventory) ? product.Inventory : dbData.Inventory;
                dbData.ImgUrl = (dbData.ImgUrl != product.ImgUrl) ? product.ImgUrl : dbData.ImgUrl;
                dbData.UpdateTime = (dbData.UpdateTime != product.UpdateTime) ? product.UpdateTime : dbData.UpdateTime;
                _shopContext.SaveChanges();

                return new BaseResponseModel
                {
                    Message = "卡片修改完畢",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch
            {
                throw;
            }

        }

        public BaseResponseModel InsertProduct(ProductList product)
        {
            try
            {
                _shopContext.ProductLists.Add(product);
                _shopContext.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "卡片新增完畢",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch
            {
                throw;
            }
        }

        public BaseResponseModel DeleteProduct(int productId)
        {
            try
            {
                var dbData = _shopContext.ProductLists.Find(productId);
                if(dbData == null)
                {
                    throw new Exception("找不到該筆卡片");
                }
                _shopContext.ProductLists.Remove(dbData);
                _shopContext.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "卡片刪除完畢",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
