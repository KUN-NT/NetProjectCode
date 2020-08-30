using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class DishInfoBll
    {
        private DishInfoDal dal;
        public DishInfoBll()
        {
            dal = new DishInfoDal();
        }
        public List<DishInfo> GetList(Dictionary<string, string> dic)
        {
            return dal.GetList(dic);
        }
        public bool Add(DishInfo model)
        {
            return dal.Add(model) > 0;
        }
        public bool Update(DishInfo model)
        {
            return dal.Update(model) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
    }
}
