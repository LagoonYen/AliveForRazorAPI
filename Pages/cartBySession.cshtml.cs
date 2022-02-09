using AliveStoreTemplate.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AliveStoreTemplate.Pages
{
    public class cartBySessionModel : PageModel
    {

        [BindProperty]
        public List<CartItem> CartItems { get; set; }
        
        [BindProperty]
        public int TotalPrice { get; set; }

        public void OnGet()
        {
            //向 Session 取得商品列表
            if(Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                CartItems = Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart");
            }

            //計算商品總額
            if (CartItems != null)
            {
                TotalPrice = CartItems.Sum(m => m.SubTotal);
            }
            else
            {
                TotalPrice = 0;
            }
        }

        public void OnPostUpdateItem()
        {

        }

        public void OnPostDeleteItem(int id)
        {
            CartItems = Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart");
            
            //index定位後移除
            int index = CartItems.FindIndex(x => x.Product.Id.Equals(id));
            CartItems.RemoveAt(index);

            //購物車小於1項商品時移除購物車 否則重新寫入session
            if (CartItems.Count < 1)
            {
                Common.CommonUtil.Remove(HttpContext.Session, "cart");
            }
            else
            {
                Common.CommonUtil.SessionSetObject(HttpContext.Session, "cart", CartItems);
            }
            RedirectToPage();
        }
    }
}
