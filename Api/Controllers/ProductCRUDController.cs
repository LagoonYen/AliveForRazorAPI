using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCRUDController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductCRUDController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 取得商品清單
        /// </summary>
        /// <param name="Req">主次分類</param>
        /// <remarks>取得商品清單</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SearchProduct([FromBody] ProductListReqModel Req)
        {
            try
            {
                return Ok(_productService.SearchProduct(Req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="Req">只傳必需的項目</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertProduct([FromBody] ProductReqModel Req)
        {
            try
            {
                return Ok(_productService.InsertProduct(Req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchProductAllInfo([FromBody] ProductReqModel product)
        {
            try
            {
                return Ok(_productService.PatchProductAllInfo(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct([FromBody]int productId, string ImgUrl)
        {
            try
            {
                DeleteProductReqModel Req = new DeleteProductReqModel()
                {
                    productId = productId,
                    ImgUrl = ImgUrl
                };
                return Ok(_productService.DeleteProduct(Req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
