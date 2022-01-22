using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Repositories
{
    public class ShopCarRepositoryImpl : ShopCarRepository
    {
        private readonly ShopContext _shopContext;

        public ShopCarRepositoryImpl(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task AddToShopCar(ProductShopcar ProductShopcar)
        {
            await _shopContext.ProductShopcars.AddAsync(ProductShopcar);
            await _shopContext.SaveChangesAsync();
        }
    }
}
