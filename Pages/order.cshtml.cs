using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AliveStoreTemplate.Pages
{
    public class orderModel : PageModel
    {
        private readonly OrderService _orderService;

        public orderModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public List<OrderList> OrderLists { get; set; }

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
            var result = _orderService.GetOrderList(UID);
            if(result.Results != null)
            {
                OrderLists = result.Results.ToList();
                return;
            }
        }
    }
}