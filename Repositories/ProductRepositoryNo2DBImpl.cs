using AliveStoreTemplate.Model;
using System.Collections.Generic;
using No2DB.Base;
using System;
using System.Linq;

namespace AliveStoreTemplate.Repositories
{
    public class ProductRepositoryNo2DBImpl : ProductRepository
    {
        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(int productId)
        {
            try
            {
                No2DB.Transaction.Operator op = new No2DB.Transaction.Operator("aaa");
                var collection = new DRole("PokemonCardInfo");
                op.Delete(collection, "Product", productId+"");
                op.Done();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 利用ProductId取得卡片完整資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductList GetProductInfo(int id)
        {
            try
            {
                var collection = new DRole("PokemonCardInfo");
                var allObjList = collection.Query<ProductList>("Product").AllDatas();
                var obj = allObjList.FirstOrDefault(x => x.Id == id);
                if(obj == null)
                {
                    throw new Exception("找不到卡片資訊");
                }
                return obj;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="product"></param>
        public void InsertProduct(ProductList product)
        {
            try
            {
                var collection = new DRole("PokemonCardInfo");
                var DataKeys = collection.Query<ProductList>("Product").DataCount();
                product.Id = DataKeys + 1;
                collection.GetOp("Product").Update(product.CardName + product.Category + "", product);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 修改單一卡片資訊
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public void PatchProduct(ProductList product)
        {
            try
            {
                No2DB.Transaction.Operator op = new No2DB.Transaction.Operator("aaa");

                var collection = new DRole("PokemonCardInfo");
                var obj = collection.Query<ProductList>("Product").DataByKey(product.Id+"");

                if(obj == null)
                {
                    throw new Exception("找不到卡片資訊");
                }
                obj.CardName = (obj.CardName != product.CardName) ? product.CardName : obj.CardName;
                obj.Category = (obj.Category != product.Category) ? product.Category : obj.Category;
                obj.Subcategory = (obj.Subcategory != product.Subcategory) ? product.Subcategory : obj.Subcategory;
                obj.Description = (obj.Description != product.Description) ? product.Description : obj.Description;
                obj.Price = (obj.Price != product.Price) ? product.Price : obj.Price;
                obj.Inventory = (obj.Inventory != product.Inventory) ? product.Inventory : obj.Inventory;
                obj.ImgUrl = (obj.ImgUrl != product.ImgUrl) ? product.ImgUrl : obj.ImgUrl;
                obj.UpdateTime = (obj.UpdateTime != product.UpdateTime) ? product.UpdateTime : obj.UpdateTime;
                op.Update(collection, "Product", obj.Id+"" , obj);
                op.Done();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 利用category及subcategory搜尋
        /// </summary>
        /// <param name="category">主分類</param>
        /// <param name="subCategory">次分類</param>
        /// <returns></returns>
        public IEnumerable<ProductList> SearchProduct(string category, string subCategory)
        {
            try
            {
                var collection = new DRole("PokemonCardInfo");

                var allObjList = collection.Query<ProductList>("Product").AllDatas();
                var objList  = (category != "") ?
                    (subCategory != "") ?
                         allObjList.Where(x => x.Category == category).Where(x => x.Subcategory == subCategory).ToList() :
                             allObjList.Where(x => x.Category == category).ToList() :
                                allObjList.ToList();

                if (objList == null)
                {
                    throw new Exception("這一彈沒有卡片喔");
                }

                return objList;
            }
            catch
            {
                throw;
            }
        }
    }
}
