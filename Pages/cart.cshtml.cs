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
    public class cartModel : PageModel
    {
        private readonly ShopCarService _shopCarService;

        public cartModel(ShopCarService shopCarService)
        {
            _shopCarService = shopCarService;
        }

        [BindProperty]
        public List<shopcar_list_respModel> shopcar_list { get; set; }

        [BindProperty]
        public int TotalCountOrder { get; set; }

        [BindProperty]
        public int TotalOrderPrice { get; set; }


        public void OnGet()
        {
            //判斷是否登入
            var userSession = Common.CommonUtil.SessionGetObject<MemberInfo>(HttpContext.Session, Common.SessionKeys.LoginSession);
            if (userSession == null)
            {
                Response.Redirect("Login");
                return;
            }
            var UID = userSession.Id;

            FreshShopCar(UID);
        }

        public void FreshShopCar(int UID)
        {
            var result = _shopCarService.User_shopcart_list(UID);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                //購物車商品總數
                TotalCountOrder = 0;
                //目前計算金額
                TotalOrderPrice = 0;

                shopcar_list = (List<shopcar_list_respModel>)result.Results;
                for (int i = 0; i < shopcar_list.Count(); i++)
                {
                    //單項小計
                    var Total = shopcar_list[i].num * shopcar_list[i].price;
                    shopcar_list[i].total = Total;
                    TotalOrderPrice += Total;
                    TotalCountOrder += shopcar_list[i].num;
                }
                return;
            }
            ViewData["Message"] = string.Format("Login Error");
        }
    }

    
}