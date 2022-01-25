using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopCarController : ControllerBase
    {
        private readonly ShopCarService _shopdb;

        public ShopCarController(ShopCarService shopCarService)
        {
            _shopdb = shopCarService;
        }

        /// <summary>
        /// 新增商品至購物車
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddToCart([FromBody]AddToCartReqModel Req)
        {
            try
            {
                var uid = int.Parse(Request.Cookies["id"]);
                Req.uid = uid;
                var result = _shopdb.AddToCart(Req);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
