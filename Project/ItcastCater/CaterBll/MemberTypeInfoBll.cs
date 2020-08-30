using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class MemberTypeInfoBll
    {
        private MemberTypeInfoDal dal;
        public MemberTypeInfoBll()
        {
            dal = new MemberTypeInfoDal();
        }
        public List<MemberTypeInfo> GetList()
        {
            return dal.GetList();
        }
        public bool Add(MemberTypeInfo model)
        {
            return dal.Add(model) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
        public bool Update(MemberTypeInfo model)
        {
            return dal.Update(model) > 0;
        }
    }
}
