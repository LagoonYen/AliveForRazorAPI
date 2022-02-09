using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AliveStoreTemplate.Services;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 下訂單
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ToOrder([FromBody]ToOrderReqModel Req)
        {
            try
            {
                var uid = int.Parse(Request.Cookies["id"]);
                Req.Uid = uid;
                var result = _orderService.ToOrder(Req);
                if(result.StatusCode != HttpStatusCode.OK)
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
                    StatusCode = HttpStatusCode.BadRequest,
                });
            }
        }

        /// <summary>
        /// 取得所有購物訂單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public IActionResult GetOrderList()
        {
            try
            {
                var UID = int.Parse(Request.Cookies["UID"]);
                return Ok(_orderService.GetOrderList(UID));
            }
            catch(Exception ex)
            {
                return BadRequest(new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// 取得單筆購物清單及資訊
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOrderDetail([FromBody] OrderDetailReqModel Req)
        {
            try
            {
                var result = _orderService.GetOrderDetail(Req);
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
     }
}
