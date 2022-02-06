using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AliveStoreTemplate.Pages
{
    public class productModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly ShopCarService _shopCarService;
        public productModel(ProductService productService, ShopCarService shopCarService)
        {
            _productService = productService;
            _shopCarService = shopCarService;
        }

        [BindProperty]
        public ProductList cardInfo { get; set; }

        public SelectList Options { get; set; }

        public void OnGet(int productId)
        {
            var result = _productService.Product_Info(productId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Results != null)
                {
                    cardInfo = result.Results.FirstOrDefault();
                    
                    return;
                };
            }
            ViewData["Message"] = string.Format("Card Error");
        }

        public void OnPostAddToCart()
        {
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                Response.Redirect("Login");
                return;
            }

            var x = (int)Options.SelectedValue;

            AddToCartReqModel Req = new AddToCartReqModel
            {
               Uid = userSession.Id,
               product_id = cardInfo.Id,
               num = (int)Options.SelectedValue,
            };

            var result =  _shopCarService.AddToCart(Req);
            if(result.StatusCode == HttpStatusCode.OK)
            {
                return;
            }
            ViewData["Message"] = string.Format("Card Error");
        }
    }

}