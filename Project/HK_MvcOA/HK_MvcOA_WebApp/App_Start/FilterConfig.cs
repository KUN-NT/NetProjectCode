using HK_MvcOA_WebApp.Models;
using System.Web;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp
{
	/// <summary>
	/// 错误
	/// </summary>
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//filters.Add(new HandleErrorAttribute());
			filters.Add(new MyExceptionAttribute());
		}
	}
}