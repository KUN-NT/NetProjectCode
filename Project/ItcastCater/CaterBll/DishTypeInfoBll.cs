using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class DishTypeInfoBll
    {
        private DishTypeInfoDal dal;
        public DishTypeInfoBll()
        {
            dal = new DishTypeInfoDal();
        }
        public List<DishTypeInfo> GetList()
        {
            return dal.GetList();
        }
        public bool Add(DishTypeInfo model)
        {
            return dal.Add(model) > 0;
        }
        public bool Update(DishTypeInfo model)
        {
            return dal.Update(model) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
    }
}
