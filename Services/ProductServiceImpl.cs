using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public class ProductServiceImpl : ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductServiceImpl(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public BaseQueryModel<ProductList> ProductList()
        {
            try
            {
                var baseQueryModel = _productRepository.ProductList();
                if(baseQueryModel.Results == null)
                {
                    return baseQueryModel;
                }
                return baseQueryModel;
            }
            catch(Exception ex)
            {
                return new BaseQueryModel<ProductList>()
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
