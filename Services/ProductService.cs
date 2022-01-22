using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public interface ProductService
    {
        //取全部商品資料
        BaseQueryModel<ProductList> ProductList();

        BaseQueryModel<ProductViewModel> Product_CategoryList();
    }
}
