
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Linq;
using System.Net;

namespace AliveStoreTemplate.Services
{
    public class OrderServiceImpl : OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ShopCarRepository _shopCarRepository;
        private readonly ProductRepository _productRepository;
        public OrderServiceImpl(OrderRepository orderRepositroy, ShopCarRepository shopCarRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepositroy;
            _shopCarRepository = shopCarRepository;
            _productRepository = productRepository;
        }

        public BaseResponseModel ToOrder(ToOrderReqModel Req)
        {
            try
            {
                var shopcarDetail = _shopCarRepository.User_shopcart_list(Req.Uid);
                if(shopcarDetail.Results == null)
                {
                    throw new Exception(shopcarDetail.Message);
                }
                foreach(var item in shopcarDetail.Results)
                {
                    if(item.inventory < item.num)
                    {
                        return new BaseResponseModel
                        {
                            Message = "商品庫存不足",
                            StatusCode = HttpStatusCode.NotAcceptable
                        };
                    }
                }

                //寫入地址 
                AddressUpserConditionModel AddressUpserCondi = new AddressUpserConditionModel
                {
                    OrderCity = Req.OrderCity,
                    OrderName = Req.OrderName,
                    OrderAddress = Req.OrderAddress,
                    OrderPhoneNumber = Req.OrderPhoneNumber,
                    OrderTown = Req.OrderTown,
                    Uid = Req.Uid,
                    DateTime = DateTime.Now,
                };

                var OrderAddressId = _orderRepository.UpsertAddress(AddressUpserCondi);
                
                return new BaseResponseModel
                {
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK  
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
