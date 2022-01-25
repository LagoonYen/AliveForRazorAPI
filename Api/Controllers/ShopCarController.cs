using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
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
        private readonly ShopCarService _shopCarService;

        public ShopCarController(ShopCarService shopCarService)
        {
            _shopCarService = shopCarService;
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
                var result = _shopCarService.AddToCart(Req);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult User_shopcart_list()
        {
            try
            {
                var uid = int.Parse(Request.Cookies["id"]);
                var result = _shopCarService.User_shopcart_list(uid);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
