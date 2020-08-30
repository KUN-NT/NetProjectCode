using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class TableInfoBll
    {
        private TableInfoDal dal;
        public TableInfoBll()
        {
            dal = new TableInfoDal();
        }
        public List<TableInfo> GetList(Dictionary<string, string> dic)
        {
            return dal.GetList(dic);
        }
        public bool Add(TableInfo model)
        {
            return dal.Add(model) > 0;
        }
        public bool Update(TableInfo model)
        {
            return dal.Update(model) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
    }
}
