using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public BaseQueryModel<ProductViewModel> Product_CategoryList()
        {
            try
            {
                var result = _productRepository.ProductList();
                if (result.Results == null)
                {
                    throw new Exception();
                }
                var product_list = result.Results;
                List<ProductViewModel> productViewModel = new List<ProductViewModel>();

                var Category = product_list.Select(x => x.Category).Distinct();
                foreach( var item in Category)
                {
                    var Sub = product_list.Where(x => x.Category == item).Select(x => x.Subcategory).Distinct().ToList();

                    productViewModel.Add(new ProductViewModel
                    {
                        Category = item,
                        SubCategory = Sub
                    });

                }
            return new BaseQueryModel<ProductViewModel>()
                {
                    Results = productViewModel,
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<ProductViewModel>()
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
