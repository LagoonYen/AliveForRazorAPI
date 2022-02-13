using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public interface ProductRepository
    {
        public IEnumerable<ProductList> SearchProduct(string category, string subCategory);

        public ProductList GetProductInfo(int id);

        public void PatchProduct(ProductList product);

        public void InsertProduct(ProductList product);

        public void DeleteProduct(int productId);
    }
}
