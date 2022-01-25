using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ProductRepository
    {
        BaseQueryModel<ProductList> SearchProduct(string category, string subCategory);

        BaseQueryModel<ProductList> Product_Info(int id);
    }
}
