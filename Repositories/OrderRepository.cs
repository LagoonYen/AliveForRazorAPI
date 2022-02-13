
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Collections.Generic;

namespace AliveStoreTemplate.Repositories
{
    public interface OrderRepository
    {
        public int UpsertAddress(AddressUpserConditionModel AddressUpserCondi);

        public void AddOrderDetail(OrderProduct orderProduct);

        public int InsertOrder(OrderList orderList);

        public List<OrderList> GetOrderList(int id);

        public void UpdateTotalPrice(int orderId, int TotalPrice);

        public OrderList GetOrderInfomation(int orderId);

        public List<OrderProduct> GetOrderDetailList(int orderIxd);


    }
}
