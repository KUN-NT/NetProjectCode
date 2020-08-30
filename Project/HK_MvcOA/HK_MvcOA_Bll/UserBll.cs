using HK_MvcOA_Dal;
using HK_MvcOA_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_MvcOA_Bll
{
	public class UserBll
	{
		UserDal dal = new UserDal();
		public IQueryable<UserInfo> Search(int s)
		{
			return dal.Search(s);
		}
	}
}
