using HK_MvcOA_Model;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace HK_MvcOA_Dal
{
	/// <summary>
	/// 负责创建EF数据操作上下文实例 必须保证线程内唯一 
	/// </summary>
	/// 不用单例模式是因为 所有的用户都访问同一个EF 对象，随着访问的用户越来越多，这个 EF 对象的资源无法及时释放，导致占用的内存也会越来越大
	/// 线程内唯一 一个请求就是一个线程 为每个线程创建唯一的对象
	/// 之所以将这个类放在这是因为 DalFactory引用了Dal 如果放在DalFactory Dal要用他就要引用DalFactory 造成循环引用
	public class DBContextFactory
	{
		public static DbContext CreateDbContext()
		{
			DbContext dbContext = (DbContext)CallContext.GetData("dbContext");//HttpContext保证线程内唯一内部实现是基于CallContext的
			if (dbContext == null)
			{
				dbContext = new OAContainer12();
				CallContext.SetData("dbContext", dbContext);
			}
			return dbContext;
		}
	}
}
