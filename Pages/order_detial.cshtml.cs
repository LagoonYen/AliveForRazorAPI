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

namespace AliveStoreTemplate.Pages
{
    public class order_detialModel : PageModel
    {
        private readonly OrderService _orderService;

        public order_detialModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public OrderList orderList { get; set; }

        public List<OrderProduct> orderProducts { get; set; }

        public void OnGet(int orderId)
        {
            OrderDetailReqModel Req  = new OrderDetailReqModel() { OrderId = orderId };
            var result = _orderService.GetOrderDetail(Req);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if(result.Results != null)
                {
                    return;
                }
                ViewData["Message"] = string.Format("Card Error");
            }
        }
    }
}