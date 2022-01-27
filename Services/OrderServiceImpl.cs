
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Collections.Generic;
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

                //取得地址id
                var OrderAddressId = _orderRepository.UpsertAddress(AddressUpserCondi);

                //商品區
                var shopcarDetail = _shopCarRepository.User_shopcart_list(Req.Uid);
                if (shopcarDetail.Results == null)
                {
                    throw new Exception(shopcarDetail.Message);
                }
                foreach (var item in shopcarDetail.Results)
                {
                    if (item.inventory < item.num)
                    {
                        return new BaseResponseModel
                        {
                            Message = "商品庫存不足",
                            StatusCode = HttpStatusCode.NotAcceptable
                        };
                    }
                }

                var orderId = DateTime.Now.ToString("yyMMdd") + Req.Uid;
                var TotalPrice = 0;
                foreach(var item in shopcarDetail.Results)
                {
                    TotalPrice += item.price * item.num;

                    //增加進訂單
                    OrderProduct orderProduct = new OrderProduct
                    {
                        OrderId = orderId,
                        ProductId = item.product_id,
                        ProductNum = item.num,
                        ProductPrice = item.price,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                    var result = _orderRepository.AddOrderDetail(orderProduct);


                    //修改inventory數量
                    var inventory = item.inventory - item.num;
                    ProductList productList = new ProductList
                    {
                        Id = item.product_id,
                        Inventory = inventory,
                    };
                    _productRepository.PatchProductInfo(productList);
                }

                //建立訂單
                OrderList orderList = new OrderList
                {
                    Uid = Req.Uid,
                    OrderNumber = orderId,
                    AddressId = OrderAddressId,
                    Remark = Req.Remark,
                    PayPrice = TotalPrice,
                    CreateTime = DateTime.Now,
                    UpdateTime= DateTime.Now
                };
                var OrderListResult = _orderRepository.InsertOrder(orderList);

                //清空購物車
                var CleanShopcarResult = _shopCarRepository.CleanShopcar(Req.Uid);

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
