
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;
using System.Collections.Generic;

namespace AliveStoreTemplate.Repositories
{
    public interface OrderRepository
    {
        /// <summary>
        /// 更新最近三筆訂單地址
        /// </summary>
        /// <param name="AddressUpserCondi"></param>
        /// <returns></returns>
        public int UpsertAddress(AddressUpserConditionModel AddressUpserCondi);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        public BaseResponseModel AddOrderDetail(OrderProduct orderProduct);

        public int InsertOrder(OrderList orderList);

        public BaseQueryModel<OrderList> GetOrderList(int id);

        public BaseResponseModel UpdateTotalPrice(int orderId, int TotalPrice);

        public BaseQueryModel<OrderList> GetOrderInfomation(int orderId);

        public BaseQueryModel<OrderProduct> GetOrderDetailList(int orderIxd);

    }
}
