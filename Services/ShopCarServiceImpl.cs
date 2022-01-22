using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AliveStoreTemplate.Services
{
    public class ShopCarServiceImpl :ShopCarService
    {
        //購物車
        private readonly ShopCarRepository _shopCarRepository;
        public ShopCarServiceImpl(ShopCarRepository shopCarRepository)
        {
                _shopCarRepository = shopCarRepository;
        }

        public async Task<BaseResponseModel> AddToShopCar(ShopCarReqModel ShopCarReqModel)
        {
            try
            {
                var time = DateTime.Now;
                ProductShopcar ProductShopcar = new()
                {
                    Uid = ShopCarReqModel.Uid,
                    PrductId = ShopCarReqModel.ProductId,
                    Num = ShopCarReqModel.Num,
                    CreateTime = time,
                    UpdateTime = time
                };
                await _shopCarRepository.AddToShopCar(ProductShopcar);
                return new BaseResponseModel()
                {
                    Message = "已新增商品到購物車",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel()
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
    }
}
