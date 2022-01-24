using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
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
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 取得商品清單
        /// </summary>
        /// <remarks>取得商品清單</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SearchProduct([FromBody]ProductListReqModel Req)
        {
            try
            {
                return Ok(_productService.SearchProduct(Req));
                
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseQueryModel<ProductList>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// 商品分類及其細項
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Product_CategoryList()
        {
            try
            {
                var result = _productService.Product_CategoryList();
                if (result == null)
                {
                    throw new Exception();
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
        /// <param product_id="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Product_Info(int id)
        {
            try
            {
                var result = _productService.Product_Info(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
