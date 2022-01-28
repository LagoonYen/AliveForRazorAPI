using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AliveStoreTemplate.Repositories
{
    public class OrderRepositoryImpl : OrderRepository
    {
        private readonly ShopContext _dbShop;
        public OrderRepositoryImpl(ShopContext shopContext)
        {
            _dbShop = shopContext;
        }

        public int UpsertAddress(AddressUpserConditionModel AddressUpserCondi)
        {
            var AddressId = 0;
            var result = _dbShop.OrderAddresses
                            .Where(x => x.Uid == AddressUpserCondi.Uid)
                            .Where(x => x.Name == AddressUpserCondi.OrderName)
                            .Where(x => x.PhoneNumber == AddressUpserCondi.OrderPhoneNumber)
                            .Where(x => x.City == AddressUpserCondi.OrderCity)
                            .Where(x => x.Town == AddressUpserCondi.OrderTown)
                            .FirstOrDefault(x => x.Address == AddressUpserCondi.OrderAddress);
            if(result == null)
            {
                OrderAddress orderAddress = new OrderAddress
                {
                    Uid = AddressUpserCondi.Uid,
                    Name = AddressUpserCondi.OrderName,
                    PhoneNumber = AddressUpserCondi.OrderPhoneNumber,
                    City = AddressUpserCondi.OrderCity,
                    Town = AddressUpserCondi.OrderTown,
                    Address = AddressUpserCondi.OrderAddress,
                    CreateTime = AddressUpserCondi.DateTime,
                    UpdateTime = AddressUpserCondi.DateTime
                };
                var newAddress = _dbShop.OrderAddresses.Add(orderAddress);
                _dbShop.SaveChanges();
                AddressId = _dbShop.OrderAddresses
                            .Where(x => x.Uid == AddressUpserCondi.Uid)
                            .Where(x => x.Name == AddressUpserCondi.OrderName)
                            .Where(x => x.PhoneNumber == AddressUpserCondi.OrderPhoneNumber)
                            .Where(x => x.City == AddressUpserCondi.OrderCity)
                            .Where(x => x.Town == AddressUpserCondi.OrderTown)
                            .FirstOrDefault(x => x.Address == AddressUpserCondi.OrderAddress).Id;
            }
            else
            {
                AddressId = result.Id;
            }
            var count = _dbShop.OrderAddresses.Where(x => x.Uid == AddressUpserCondi.Uid).Count();
            if(count > 3)
            {
                var oldAddress = _dbShop.OrderAddresses.Where(x => x.Uid == AddressUpserCondi.Uid).FirstOrDefault();
                _dbShop.OrderAddresses.Remove(oldAddress);
                _dbShop.SaveChanges();
            }
            return AddressId;
        }

        public BaseResponseModel AddOrderDetail(OrderProduct orderProduct)
        {
            _dbShop.OrderProducts.Add(orderProduct);
            _dbShop.SaveChanges();
            return new BaseResponseModel
            {
                Message = "已購買",
                StatusCode = HttpStatusCode.OK
            };
        }

        public int InsertOrder(OrderList orderList)
        {
            _dbShop.OrderLists.Add(orderList);
            _dbShop.SaveChanges();
            return _dbShop.OrderLists.FirstOrDefault(x => x.OrderNumber == orderList.OrderNumber).Id;
        }

        public BaseQueryModel<OrderList> GetOrderList(int id)
        {
            try
            {
                var result = _dbShop.OrderLists.Where(x => x.Uid == id);
                if(!result.Any())
                {
                    return new BaseQueryModel<OrderList>
                    {
                        Results = null,
                        Message = "暫無歷史訂單",
                        StatusCode = HttpStatusCode.Accepted
                    };
                }
                return new BaseQueryModel<OrderList> {
                    Results = result,
                    Message = string.Empty,
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

        public BaseResponseModel UpdateTotalPrice(int orderId, int TotalPrice)
        {
            try
            {
                var result = _dbShop.OrderLists.Find(orderId);
                result.PayPrice = TotalPrice;
                _dbShop.SaveChanges();
                return new BaseResponseModel
                {
                    Message = "已更新售價",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public OrderProduct GetOrderDetailList(int orderId)
        {
            //return _dbShop.OrderLists.Where(x => x.OrderNumber ==orderId);
            return new OrderProduct
            {
                OrderId = orderId,
            };
        }
    }
}
