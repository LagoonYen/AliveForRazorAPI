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

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("/api[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("action")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ToOrder(ToOrderReqModel Req)
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

        [HttpGet]
        [Route("action")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public IActionResult GetOrderList()
        {
            try
            {
                var uid = 4;
                //var uid = int.Parse(Request.Cookies["id"]);
                return Ok(_orderService.GetOrderList(uid));
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

        [HttpGet]
        [Route("action")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOrderDetail(OrderDetailReqModel Req)
        {
            try
            {
                var uid = int.Parse(Request.Cookies["id"]);
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
