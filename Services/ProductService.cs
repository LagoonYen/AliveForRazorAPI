using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface ProductService
    {
        //取全部商品資料
        BaseQueryModel<ProductList> SearchProduct(ProductListReqModel Req);

        BaseQueryModel<ProductViewModel> Product_CategoryList();

        BaseQueryModel<ProductList> Product_Info(int id);
    }
}
