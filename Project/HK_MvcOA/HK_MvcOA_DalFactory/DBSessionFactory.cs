using HK_MvcOA_IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HK_MvcOA_DalFactory
{
	/// <summary>
	/// 创建数据会话层对象的工厂(解耦)
	/// </summary>
	public class DBSessionFactory
	{
		/// <summary>
		/// 创建DBSession对象(线程内唯一)
		/// </summary>
		/// <returns></returns>
		public static IDBSession CreateDBSession()
		{
			IDBSession dbSession = (IDBSession)CallContext.GetData("dbSession");
			if (dbSession == null)
			{
				dbSession = new DBSession();
				CallContext.SetData("dbSession", dbSession);
			}
			return dbSession;
		}
	}
}
