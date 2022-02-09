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
            //�V Session ���o�ӫ~�C��
            if(Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                CartItems = Common.CommonUtil.SessionGetObject<List<CartItem>>(HttpContext.Session, "cart");
            }

            //�p��ӫ~�`�B
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
            
            //index�w��Ჾ��
            int index = CartItems.FindIndex(x => x.Product.Id.Equals(id));
            CartItems.RemoveAt(index);

            //�ʪ����p��1���ӫ~�ɲ����ʪ��� �_�h���s�g�Jsession
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
