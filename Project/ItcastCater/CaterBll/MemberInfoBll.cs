using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class MemberInfoBll
    {
        private MemberinfoDal dal;
        public MemberInfoBll()
        {
            dal = new MemberinfoDal();
        }
        public List<MemberInfo> SelecyAll(Dictionary<string,string> dic)
        {
            return dal.SelectAll(dic);
        }
        public bool Add(MemberInfo model)
        {
            return dal.Add(model)>0;
        }
        public bool Update(MemberInfo model)
        {
            return dal.Update(model)>0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
        public MemberInfo SelectMember(string name)
        {
            return dal.SelectMember(name);
        }
    }
}
