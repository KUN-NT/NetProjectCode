using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class HallInfoBll
    {
        private HallInfoDal dal;
        public HallInfoBll()
        {
            dal = new HallInfoDal();
        }
        public List<HallInfo> GetList()
        {
            return dal.GetList();
        }
        public bool Add(HallInfo model)
        {
            return dal.Add(model) > 0;
        }
        public bool Update(HallInfo model)
        {
            return dal.Update(model) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
    }
}
