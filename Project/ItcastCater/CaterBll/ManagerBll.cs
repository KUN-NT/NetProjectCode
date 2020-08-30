using CaterComman;
using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class ManagerInfoBll
    {
        public ManagerInfoDal dal = new ManagerInfoDal();
        public List<ManagerInfo> GetList()
        {
            return dal.GetList();
        }
        public bool Insert(ManagerInfo manager)
        {
            return dal.Insert(manager) > 0;
        }
        public bool Update(ManagerInfo manager)
        {
            return dal.Update(manager) > 0;
        }
        public bool Delete(string id)
        {
            return dal.Delete(id) > 0;
        }
        public ManagerStute Login(string name,string pwd,out int type)
        {
            type = 0;
            ManagerStute stute = new ManagerStute();
            ManagerInfo mi=dal.Login(name);
            if (mi != null)
            {
                if (mi.MPwd.Equals(MD5Helper.EncryptString(pwd)))
                {
                    stute = ManagerStute.OK;
                    type = mi.MType;
                }
                else
                {
                    stute = ManagerStute.PWDERROR;
                }
            }
            else
            {
                stute = ManagerStute.NAMEERROR;
            }
            return stute;
        }
    }
}
