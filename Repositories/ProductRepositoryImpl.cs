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

        public Task<BaseQueryModel<ProductList>> SearchProduct(string category, string subCategory)
        {
            try
            {
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
                    Results = new List<ProductList> { productList },
                    Message = string.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                
            }
        }

        public Task<BaseQueryModel<ProductList>> Product_Info(int id)
        {
            try
            {
                //ProductList productList = new();
                var productList = _shopContext.ProductLists.Find(id);

                return new BaseQueryModel<ProductList>
                {
                    Results = (IEnumerable<ProductList>)productList,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<ProductList>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.OK
                };
            }
            
        }
    }
}
