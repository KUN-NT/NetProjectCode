using CaterDal;
using CaterModel;
using System.Collections.Generic;

namespace CaterBll
{
    public partial class OrderInfoBll
    {
        private OrderInfoDal dal;
        public OrderInfoBll()
        {
            dal = new OrderInfoDal();
        }
        public int KaiDan(int id)
        {
            return dal.KaiDan(id);
        }
        public bool DianCai(int Did, int Oid)
        {
            return dal.DianCai(Did, Oid) > 0;
        }
        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            return dal.GetDetailList(orderId);
        }
        public int GetOrderIdByTableId(int tableId)
        {
            return dal.GetOrderIdByTableId(tableId);
        }
        public bool GetCountByOId(string oid, string count)
        {
            return dal.GetCountByOId(oid, count) > 0;
        }
        public int GetTotalMoneyByOrderId(int Oid)
        {
            return dal.GetTotalMoneyByOrderId(Oid);
        }
        public bool SetOrderMoney(int orderid, decimal money)
        {
            return dal.SetOrderMomey(orderid, money) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id)>0;
        }
        public bool CancellationOfOrder(string Oid)
        {
            return dal.CancellationOfOrder(Oid) > 0;
        }
    }
}
