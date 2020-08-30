using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class RemoveInfoBll
    {
        private RemoveInfoDal dal;
        public RemoveInfoBll()
        {
            dal = new RemoveInfoDal();
        }
        public bool Add(RemoveInfo model)
        {
            return dal.Add(model) > 0;
        }
        public List<RemoveInfo> GetList()
        {
            return dal.GetList();
        }
        public bool Return(string id, string table)
        {
            return dal.ReturnData(id, table) > 0;
        }
        public bool Delete(string id, string table)
        {
            return dal.DeleteData(id, table) > 0;
        }
    }
}
