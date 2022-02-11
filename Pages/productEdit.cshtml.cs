using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Net;

namespace AliveStoreTemplate.Pages
{
    public class productEditModel : PageModel
    {
        private readonly ProductService _productService;

        public productEditModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductList productReq { get; set; }

        public void OnGet(int productId)
        {
            try
            {
                var result = _productService.GetProductInfo(productId);

                if(result.StatusCode == HttpStatusCode.OK)
                {
                    productReq = result.Results.FirstOrDefault();
                    return;
                }
            }
            catch(Exception ex)
            {
                ViewData["Message"] = string.Format(ex.Message);
            }
        }


        [BindProperty]
        public IFormFile CardImg { get; set; }

        public void OnPostPatchProductAllInfo()
        {
            try
            {
                ProductReqModel productList = new ProductReqModel
                {
                    Id = productReq.Id,
                    Category = productReq.Category,
                    Subcategory = productReq.Subcategory,
                    CardName = productReq.CardName,
                    Description = productReq.Description,
                    Price = productReq.Price,
                    Inventory = productReq.Inventory,
                    ImgUrl = productReq.ImgUrl,
                    CardImg = CardImg
                };

                var result = _productService.PatchProductAllInfo(productList);
                if( result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Message);
                }
                Response.Redirect("productIndex");
                return;
            }
            catch (Exception ex)
            {
                ViewData["Message"] = string.Format(ex.Message);
            }
        }
    }
}
