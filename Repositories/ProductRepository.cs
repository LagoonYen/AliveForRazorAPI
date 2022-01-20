using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ProductRepository
    {
        BaseQueryModel<ProductList> ProductList();
    }
}
