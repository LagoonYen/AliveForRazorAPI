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

        public BaseQueryModel<ProductList> SearchProduct(ProductListReqModel Req)
        {
            try
            {
                var category = Req.Category;
                var subCategory =  Req.SubCategory;
                return _productRepository.SearchProduct(category, subCategory);
            }
            catch
            {
                throw;
            }
        }

        public BaseQueryModel<ProductViewModel> Product_CategoryList()
        {
            try
            {
                string category = "";
                string subCategory = "";
                var result = _productRepository.SearchProduct(category, subCategory);
                var product_list = result.Results;
                //List<ProductViewModel> productViewModel = new List<ProductViewModel>();
                var productViewModel = product_list.Select(x => new 
                {
                    Category = x.Category,
                    SubCategory = x.Subcategory
                }).GroupBy(x => x.Category).Select(x => new ProductViewModel
                {
                    Category = x.Key,
                    SubCategory = x.Select(x => x.SubCategory).Distinct().ToList()
                });
                //foreach( var eachCategory in Category)
                //{
                //    var EachSubCategory = product_list.Where(x => x.Category == eachCategory).Select(x => x.Subcategory).Distinct().ToList();

                //    productViewModel.Add(new ProductViewModel
                //    {
                //        Category = eachCategory,
                //        SubCategory = EachSubCategory
                //    });

                //}
            return new BaseQueryModel<ProductViewModel>
                {
                    Results = productViewModel,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                throw;
            }
        }

        //取卡資訊
        public BaseQueryModel<ProductList> Product_Info(int id)
        {
            try {
                return _productRepository.Product_Info(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
