using CaterDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class OrderPayBll
    {
        private OrderPayDal dal;
        public OrderPayBll()
        {
            dal = new OrderPayDal();
        }
        public bool Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            return dal.Pay(isUseMoney,memberId,payMoney,orderid,discount)>0;
        }
    }
}
