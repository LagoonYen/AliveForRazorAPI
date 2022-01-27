
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Collections.Generic;

namespace AliveStoreTemplate.Repositories
{
    public interface OrderRepository
    {
        public int UpsertAddress(AddressUpserConditionModel AddressUpserCondi);

        public BaseResponseModel AddOrderDetail(OrderProduct orderProduct);

        public BaseResponseModel InsertOrder(OrderList orderList);
    }
}
