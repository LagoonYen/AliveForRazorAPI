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
        private readonly ShopCarService _shopCarService;

        public ShopCarController(ShopCarService shopCarService)
        {
            _shopCarService = shopCarService;
        }

        /// <summary>
        /// 新增商品至購物車
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToShopCar(int product_id, int number)
        {
            try
            {
                var uid = int.Parse(Request.Cookies["id"]);
                ShopCarReqModel ShopCarReqModel = new()
                {
                    Uid = uid,
                    ProductId = product_id,
                    Num = number
                };
                var result = await _shopCarService.AddToShopCar(ShopCarReqModel);
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
