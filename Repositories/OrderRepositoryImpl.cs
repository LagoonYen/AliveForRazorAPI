using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
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

        public BaseQueryModel<int> UpsertAddress(AddressUpserConditionModel AddressUpserCondi)
        {
            int AddressId;
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
            return new BaseQueryModel<int>
            {
                Results = AddressId,
                Message = string.Empty,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
