using System;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net;

namespace AliveStoreTemplate.Pages
{
    public class ProductIndexModel : PageModel
    {
        private readonly ProductService _productService;
        public ProductIndexModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public List<ProductList> CardList { get; set; }

        public void OnGet()
        {
            //先帶全部卡片 後續修改
            var Category = "";
            var Subcategory = "";
            ProductListReqModel Req = new ProductListReqModel
            {
                Category = Category,
                Subcategory = Subcategory
            };
            var baseQueryModel = _productService.SearchProduct(Req);
            if(baseQueryModel.StatusCode != HttpStatusCode.BadRequest)
            {
                if(baseQueryModel.Results != null)
                {
                    CardList = (List<ProductList>)baseQueryModel.Results;
                    return;
                }
            }
            ViewData["Message"] = string.Format(baseQueryModel.Message);
        }
    }
}
