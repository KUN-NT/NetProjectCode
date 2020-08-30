using HK_MvcOA_Common;
using HK_MvcOA_IBll;
using HK_MvcOA_Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp.Controllers
{
    public class BaseController : Controller
    {
		public UserInfo LoginUser { get; set; }

		/// <summary>
		/// 执行控制器中的方法之前先执行该方法。进行登录校验和权限校验
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			bool isSucess = false;
			if (Request.Cookies["sessionId"] != null)
			{
				string sessionId = Request.Cookies["sessionId"].Value;
				//根据该值查Memcache中保存的数据
				object obj = MemcacheHelper.Get(sessionId);
				if (obj != null)
				{
					UserInfo userInfo = SerializeHelper.DeserializeToObject<UserInfo>(obj.ToString());
					LoginUser = userInfo;
					isSucess = true;
					MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));//模拟出滑动过期时间.

					#region 非菜单权限过滤
					//为了方便测试加的后门
					if (LoginUser.UName == "admin")
					{
						return;
					}

					//获取当前请求Url地址
					string url = Request.Url.AbsolutePath;
					if (url.Equals("/", StringComparison.CurrentCultureIgnoreCase) ||
						url.Equals("/Home/Index", StringComparison.CurrentCultureIgnoreCase) ||
						url.Equals("/Home/GetMenu", StringComparison.CurrentCultureIgnoreCase) ||
						url.Equals("/Home/HomePage", StringComparison.CurrentCultureIgnoreCase))
					{
						return;
					}
					//获取请求方式
					string httpMethod = Request.HttpMethod;
					IApplicationContext ctx = ContextRegistry.GetContext();
					//查找访问页面的权限信息
					IActionInfoService ActionInfoService = (IActionInfoService)ctx.GetObject("ActionInfoService");
					var actionInfo = ActionInfoService.LoadEntities(a => a.Url == url && a.HttpMethod == httpMethod).FirstOrDefault();
					if (actionInfo == null) { filterContext.Result = Redirect("/Error.html"); return; }

					//查找登录用户是否具有访问权限
					IUserInfoService UserInfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
					var loginUser = UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
					//1、
					//var isExt = LoginUser.R_UserInfo_ActionInfo.Where(a => a.ActionInfoID == actionInfo.ID).FirstOrDefault();
					var isExt = (from a in loginUser.R_UserInfo_ActionInfo where a.ActionInfoID == actionInfo.ID select a).FirstOrDefault();
					if (isExt != null)
					{
						if (isExt.IsPass)
						{
							return;
						}
						else
						{
							filterContext.Result = Redirect("/Error.html");
							return;
						}
					}
					//2、
					var userRole = loginUser.RoleInfo;
					var roleAction = (from r in userRole
									  from a in r.ActionInfo
									  where a.ID == actionInfo.ID
									  select a).Count();
					if (roleAction < 1)
					{
						filterContext.Result = Redirect("/Error.html");
						return;
					} 
					#endregion
				}

				//不会返回一个ActionResult对象 还会继续运行下面的代码
				//filterContext.HttpContext.Response.Redirect("/Login/Index");
			}
			if (!isSucess)
			{
				//返回了ActionResult对象 直接跳转 不执行下面的代码
				filterContext.Result = Redirect("/Login/Index");//注意.
			}
		}
    }
}
