using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AliveStoreTemplate.Pages
{
    public class cartModel : PageModel
    {
        private readonly ShopCarService _shopCarService;
        private readonly OrderService _orderService;

        public cartModel(ShopCarService shopCarService, OrderService orderService)
        {
            _shopCarService = shopCarService;
            _orderService = orderService;
        }

        [BindProperty]
        public List<shopcar_list_respModel> Shopcar_list { get; set; }

        [BindProperty]
        public int UID { get; set; }

        public void OnGet()
        {
            //判斷是否登入
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                Response.Redirect("Login");
                return;
            }
            UID = userSession.Id;
        }

        public IActionResult OnPostCSToOrder([FromBody]ToOrderReqModel Req)
        {
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                return RedirectToPage("./Login");
            }
            Req.Uid = userSession.Id;
            _orderService.ToOrder(Req);
            return null;
        }
    }

    
}