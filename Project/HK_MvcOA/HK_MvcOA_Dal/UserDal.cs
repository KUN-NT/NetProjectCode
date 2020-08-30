using HK_MvcOA_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_MvcOA_Dal
{
	public class UserDal
	{
		OAContainer12 db = new OAContainer12();
		public IQueryable<UserInfo> Search(int s)
		{
			return db.UserInfo.Where(u => u.ID == s);
		}
	}
}
