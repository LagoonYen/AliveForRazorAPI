
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
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

        /// <summary>
        /// 下訂單
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel ToOrder(ToOrderReqModel Req)
        {
            try
            {
                //建立新的地址
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

                //取得購物車 及 庫存資料
                var shopcarDetail = _shopCarRepository.GetUserShopcartList(Req.Uid);
                if (shopcarDetail == null)
                {
                    throw new Exception("購物車內無商品");
                }
                foreach (var item in shopcarDetail)
                {
                    if (item.inventory < item.num)
                    {
                        throw new Exception("庫存不足");
                    }
                }

                //訂單編號
                var orderNumber = "OrderNum" + DateTime.Now.ToString("yyMMddhhmmss") + Req.Uid;

                //建立訂單 並取得訂單編號
                OrderList orderList = new OrderList
                {
                    Uid = Req.Uid,
                    OrderNumber = orderNumber,
                    AddressId = OrderAddressId,
                    Remark = Req.Remark,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                var orderId = _orderRepository.InsertOrder(orderList);

                //建立訂單內的
                var TotalPrice = 0;
                foreach(var item in shopcarDetail)
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
                    _orderRepository.AddOrderDetail(orderProduct);


                    //修改inventory數量
                    var inventory = item.inventory - item.num;
                    ProductList productList = new ProductList
                    {
                        Id = item.product_id,
                        Inventory = inventory,
                    };
                    _productRepository.PatchProduct(productList);
                }

                _orderRepository.UpdateTotalPrice(orderId, TotalPrice);

                //清空購物車
                _shopCarRepository.CleanShopcar(Req.Uid);

                return new BaseResponseModel
                {
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK  
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 取得歷史訂單基本資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseQueryModel<OrderList> GetOrderList(int id)
        {
            try
            {
                if(id == 0)
                {
                    throw new Exception("id錯誤");
                }
                var orderList = _orderRepository.GetOrderList(id);
                return new BaseQueryModel<OrderList>
                {
                    Results = orderList,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseQueryModel<OrderList>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        /// <summary>
        /// 取得單筆訂單詳細資料
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseQueryModel<OrderDetailResponseModel> GetOrderDetail(OrderDetailReqModel Req)
        {
            //取得訂單資料
            var OrderInfo = _orderRepository.GetOrderInfomation(Req.OrderId);
            //取得訂單詳細商品資料
            var ProductDetail = _orderRepository.GetOrderDetailList(Req.OrderId);
            OrderDetailResponseModel Resp = new OrderDetailResponseModel()
            {
                OrderNumber = OrderInfo.OrderNumber,
                Uid = OrderInfo.Uid,
                Recipient = OrderInfo.Recipient,
                RecipientPhone = OrderInfo.RecipientPhone,
                RecipientCity = OrderInfo.RecipientCity,
                RecipientTown = OrderInfo.RecipientTown,
                RecipientAddress = OrderInfo.RecipientAddress,
                Remark = OrderInfo.Remark,
                PayPrice = OrderInfo.PayPrice,
                IsPay = OrderInfo.IsPay,
                PayTime = OrderInfo.PayTime,
                IsShip = OrderInfo.IsShip,
                ShipTime =  OrderInfo.ShipTime,
                IsReceipt = OrderInfo.IsReceipt,
                ReceiptTime = OrderInfo.ReceiptTime,
                ShipNumber = OrderInfo.ShipNumber,
                CreateTime = OrderInfo.CreateTime,
                UpdateTime = OrderInfo.UpdateTime,
                Products = ProductDetail
            };
            return new BaseQueryModel<OrderDetailResponseModel>
            {
                Message = String.Empty,
                Results = new List<OrderDetailResponseModel> { Resp },
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
