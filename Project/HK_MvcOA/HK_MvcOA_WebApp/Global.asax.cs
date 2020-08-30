using HK_MvcOA_WebApp.Models;
using log4net;
using Spring.Web.Mvc;
using System;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HK_MvcOA_WebApp
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	//public class MvcApplication : System.Web.HttpApplication
	public class MvcApplication : SpringMvcApplication//SpringMvcApplication会读取配置文件创建IApplicationContext容器
	{
		protected void Application_Start()
		{
			//在程序开始时读取log4net的配置信息
			log4net.Config.XmlConfigurator.Configure();

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			#region 开启一个线程 扫描异常信息队列 有保存到日志文件 没有休息一段时间再扫
			string filePath = Server.MapPath("/Log");
			ThreadPool.QueueUserWorkItem((a) =>
			{
				while (true)
				{
					//判断错误队列是否有数据
					if (MyExceptionAttribute.exceptionQueue.Count > 0)
					//if(MyExceptionAttribute.redisClient.GetListCount("errorMsg")>0)
					{
						Exception ex = MyExceptionAttribute.exceptionQueue.Dequeue();
						//string ex = MyExceptionAttribute.redisClient.DequeueItemFromList("errorMsg");
						//队列中有数据 写入日志
						if (ex != null)
						//if(!string.IsNullOrEmpty(ex))
						{
							//将数据保存到文件中
							//string fileName = DateTime.Now.ToString("yyyy-MM-dd");
							//File.AppendAllText(fileName, ex.ToString(), System.Text.Encoding.UTF8);

							//使用log4net保存数据
							ILog logger = LogManager.GetLogger("errorMsg");
							logger.Error(ex.ToString());
						}
						else
						{
							//如果队列中没有数据 休息3秒
							Thread.Sleep(3000);
						}
					}
					else
					{
						//如果队列中没有数据 休息3秒
						Thread.Sleep(3000);
					}
				}
			}, filePath); 
			#endregion
		}
	}
}