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

        public BaseQueryModel<ProductList> ProductList()
        {
            try
            {
                var product = _shopContext.ProductLists.ToList();
                return new BaseQueryModel<ProductList>
                {
                    Results = product,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
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
