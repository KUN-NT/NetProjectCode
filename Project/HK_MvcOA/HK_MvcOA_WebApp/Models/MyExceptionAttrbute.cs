using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp.Models
{
	/// <summary>
	/// 捕获异常 在FilterConfig类中注册就不用在每个类上加特性标签了
	/// </summary>
	public class MyExceptionAttribute : HandleErrorAttribute
	{
		public static Queue<Exception> exceptionQueue = new Queue<Exception>();
		//public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
		//public static IRedisClient redisClient = clientManager.GetClient();

		public override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
			Exception ex = filterContext.Exception;
			//将错误写到队列
			exceptionQueue.Enqueue(ex);
			//redisClient.EnqueueItemOnList("errorMsg", ex.ToString());
			//跳转到错误页
			filterContext.HttpContext.Response.Redirect("/Error.html");
		}
	}
}