using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.DTOModel;
using No2DB.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AliveStoreTemplate.Repositories
{
    public class OrderRepositoryNo2DBImpl : OrderRepository
    {
        private readonly ShopContext _dbShop;
        public OrderRepositoryNo2DBImpl(ShopContext shopContext)
        {
            _dbShop = shopContext;
        }

        /// <summary>
        /// 更新最近三筆訂單地址
        /// </summary>
        /// <param name="AddressUpserCondi"></param>
        /// <returns></returns>
        public int UpsertAddress(AddressUpserConditionModel AddressUpserCondi)
        {
            try
            {
                //初始化回傳ID
                var AddressId = 0;

                //尋找是否有相同地址跟收件人資料的資料
                var result = _dbShop.OrderAddresses
                                .Where(x => x.Uid == AddressUpserCondi.Uid)
                                .Where(x => x.Name == AddressUpserCondi.OrderName)
                                .Where(x => x.PhoneNumber == AddressUpserCondi.OrderPhoneNumber)
                                .Where(x => x.City == AddressUpserCondi.OrderCity)
                                .Where(x => x.Town == AddressUpserCondi.OrderTown)
                                .FirstOrDefault(x => x.Address == AddressUpserCondi.OrderAddress);

                //找不到相同的 新增新的一筆
                if (result == null)
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

                //若大於3筆 刪除最舊的一筆資料 維持三筆最新
                var count = _dbShop.OrderAddresses.Where(x => x.Uid == AddressUpserCondi.Uid).Count();
                if (count > 3)
                {
                    var oldAddress = _dbShop.OrderAddresses.Where(x => x.Uid == AddressUpserCondi.Uid).FirstOrDefault();
                    _dbShop.OrderAddresses.Remove(oldAddress);
                    _dbShop.SaveChanges();
                }
                return AddressId;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        public void AddOrderDetail(OrderProduct orderProduct)
        {
            try
            {
                _dbShop.OrderProducts.Add(orderProduct);
                _dbShop.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public int InsertOrder(OrderList orderList)
        {
            try
            {
                _dbShop.OrderLists.Add(orderList);
                _dbShop.SaveChanges();
                return _dbShop.OrderLists.FirstOrDefault(x => x.OrderNumber == orderList.OrderNumber).Id;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrderList> GetOrderList(int id)
        {
            try
            {
                var result = _dbShop.OrderLists.Where(x => x.Uid == id).ToList();

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="TotalPrice"></param>
        public void UpdateTotalPrice(int orderId, int TotalPrice)
        {
            try
            {
                var result = _dbShop.OrderLists.Find(orderId);
                if (result == null)
                {
                    throw new Exception("查無商品");
                }
                result.PayPrice = TotalPrice;
                _dbShop.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 取得訂單詳細資料
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderList GetOrderInfomation(int orderId)
        {
            try
            {
                var result = _dbShop.OrderLists.FirstOrDefault(x => x.Id == orderId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 單筆訂單細項
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderProduct> GetOrderDetailList(int orderId)
        {
            try
            {
                var result = _dbShop.OrderProducts.Where(x => x.OrderId == orderId).ToList();
                if (result == null)
                {
                    throw new Exception("找不到訂單資訊，請重新搜尋");
                }
                return result;
            }
            catch
            {
                throw;
            }

        }
    }
}
