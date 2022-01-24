using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AliveStoreTemplate.Model.ReqModel;

namespace AliveStoreTemplate.Services
{
    public class ProductServiceImpl : ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductServiceImpl(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<BaseQueryModel<ProductList>> SearchProduct(ProductListReqModel Req)
        {
            try
            {
                var category = Req.Category;
                var subCategory =  Req.SubCategory;
                var baseQueryModel = _productRepository.SearchProduct(category, subCategory);
                return new BaseQueryModel<ProductList>
                {
                    
                };
                
            }
            catch(Exception ex)
            {
                
            }
        }

        public Task<BaseQueryModel<ProductViewModel>> Product_CategoryList()
        {
            try
            {
                string category = "";
                string subCategory = "";
                var result = _productRepository.SearchProduct(category, subCategory);
                if (result.Results == null)
                {
                    throw new Exception();
                }
                var product_list = result.Results;
                List<ProductViewModel> productViewModel = new List<ProductViewModel>();

                var Category = product_list.Select(x => x.Category).Distinct();
                foreach( var eachCategory in Category)
                {
                    var EachSubCategory = product_list.Where(x => x.Category == eachCategory).Select(x => x.Subcategory).Distinct().ToList();

                    productViewModel.Add(new ProductViewModel
                    {
                        Category = eachCategory,
                        SubCategory = EachSubCategory
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

        public Task<BaseQueryModel<ProductList>> Product_Info(int id)
        {
            var result = _productRepository.Product_Info(id);
            throw new NotImplementedException();
        }
    }
}
