using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
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

        public BaseResponseModel InsertOrder(OrderList orderList)
        {
            _dbShop.OrderLists.Add(orderList);
            _dbShop.SaveChanges();
            return new BaseResponseModel
            {
                Message = "已建立訂單",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
