using System.Collections.Generic;
using System.Linq;
using System.Net;
using AliveStoreTemplate.Common;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
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

        //商品資訊
        [BindProperty]
        public ProductList CardInfo { get; set; }

        public SelectList Options { get; set; }

        //下訂數量
        [BindProperty]
        public int OrderCount { get; set; }

        public void OnGet(int productId)
        {
            var result = _productService.GetProductInfo(productId);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Results != null)
                {
                    CardInfo = result.Results.FirstOrDefault();
                    return;
                };
            }
            ViewData["Message"] = string.Format("Card Error");
        }

        public void OnPostAddToCart()
        {
            //資料存放於資料庫
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                Response.Redirect("Login");
                return;
            }

            AddToCartReqModel Req = new AddToCartReqModel
            {
                Uid = userSession.Id,
                product_id = CardInfo.Id,
                num = OrderCount
            };

            var result = _shopCarService.AddToCart(Req);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                Response.Redirect("product?productId=" + CardInfo.Id);
                return;
            }
            ViewData["Message"] = string.Format("Card Error");
        }

        public void OnPostAddToCartBySession()
        {
            //資料存放於session
            //<ShopcarSession來自Common>
            var result = _productService.GetProductInfo(CardInfo.Id);
            
            //建立下訂商品
            CartItem item = new CartItem
            {
                Product = result.Results.FirstOrDefault(),
                Amount = OrderCount,
                SubTotal = result.Results.FirstOrDefault().Price * OrderCount
            };

            //判斷是否有購物車
            if(Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                //沒有就新增
                List<CartItem> cart = new List<CartItem>();
                cart.Add(item);
                Common.CommonUtil.SessionSetObject(HttpContext.Session, "cart", cart);
            }
            else
            {
                //如果購物車存在
                List<CartItem> cart = Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart");
                //檢查購物車中是否包含同樣商品
                int index = cart.FindIndex(x => x.Product.Id.Equals(CardInfo.Id));
                if(index != -1)
                {
                    cart[index].Amount += item.Amount;
                    cart[index].SubTotal += item.SubTotal;
                }
                else
                {
                    cart.Add(item);
                }
                Common.CommonUtil.SessionSetObject(HttpContext.Session, "cart", cart);
            }
            //return NoContent();
            Response.Redirect("product?productId=" + CardInfo.Id);
        }
    }

}