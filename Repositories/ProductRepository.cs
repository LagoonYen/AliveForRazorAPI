using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ProductRepository
    {
        public BaseQueryModel<ProductList> SearchProduct(string category, string subCategory);

        public BaseQueryModel<ProductList> Product_Info(int id);

        public BaseResponseModel PatchProductInfo(ProductList productList);
    }
}
