using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface ProductService
    {
        /// <summary>
        /// 搜尋商品
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        BaseQueryModel<ProductList> SearchProduct(ProductListReqModel Req);

        /// <summary>
        /// 取得不同的分類
        /// </summary>
        /// <returns></returns>
        BaseQueryModel<ProductResultModel> Product_CategoryList();

        /// <summary>
        /// 取得單件商品的資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseQueryModel<ProductList> Product_Info(int id);

        /// <summary>
        /// 修改卡片資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        BaseResponseModel PatchProductAllInfo(ProductList product);

        /// <summary>
        /// 新增卡片資料
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        BaseResponseModel InsertProduct(ProductReqModel Req);

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        BaseResponseModel DeleteProduct(int productId);
    }
}
