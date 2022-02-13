using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Model.ViewModel;

namespace AliveStoreTemplate.Services
{
    public interface OrderService
    {
        /// <summary>
        /// 下訂單
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseResponseModel ToOrder(ToOrderReqModel Req);

        /// <summary>
        /// 取得歷史訂單基本資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseQueryModel<OrderList> GetOrderList(int id);

        /// <summary>
        /// 取得單筆訂單詳細資料
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public BaseQueryModel<OrderDetailResponseModel> GetOrderDetail(OrderDetailReqModel Req);
    }
}
