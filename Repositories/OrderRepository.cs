
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

        public int InsertOrder(OrderList orderList);

        public BaseQueryModel<OrderList> GetOrderList(int id);

        public BaseResponseModel UpdateTotalPrice(int orderId, int TotalPrice);

        public BaseQueryModel<OrderList> GetOrderInfomation(int orderId);

        public BaseQueryModel<OrderProduct> GetOrderDetailList(int orderIxd);

    }
}
