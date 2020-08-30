using CZBK.ItcastOA.Common;
using HK_MvcOA_Common;
using HK_MvcOA_IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp.Controllers
{
	public class LoginController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
		
		//使用Spring.Net依赖注入实例化对象
		IUserInfoService UserInfoService { get; set; }

		#region 完成用户登录
		public ActionResult UserLogin()
		{
			if (Response.Cookies["sessionId"].Value!= null)
			{
				return Content("ok:登录成功");
			}
			else
			{
				string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;

				if (string.IsNullOrEmpty(validateCode))
				{
					return Content("no:验证码错误!!");
				}
				Session["validateCode"] = null;
				string txtCode = Request["vCode"];
				if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
				{
					return Content("no:验证码错误!!");
				}
				string userName = Request["LoginCode"];
				string userPwd = Request["LoginPwd"];
				bool check = Request["Check"] == "checded" ? true : false;
				var userInfo = UserInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();//根据用户名找用户
				if (userInfo != null)
				{
					//Session["userInfo"] = userInfo;
					//这里用Memcache代替Session是因为一旦用户访问量大了一台机器无法满足 就要建立多台机器处理用户访问 如果用seeion保存用户数据 只能保存在一台机器中 如果用户下次请求被其他机器接收 会检测不到用户数据 用户有得重新登录 而Membercache中数据可以在多台机器中共享 (这就是分布式缓存的一种应用)
					//产生一个GUID值作为Memache的键.
					string sessionId = Guid.NewGuid().ToString();
					//这里报错了，可能是生成UserInfo的T4模板没有修改加上[JsonIgnore] 序列号不了后面几个复杂类型数据
					MemcacheHelper.Set(sessionId, SerializeHelper.SerializeToString(userInfo), DateTime.Now.AddMinutes(20));//将登录用户信息存储到Memcache中。

					#region 自动登录
					//Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以Cookie的形式返回给浏览器。
					if (check)
					{
						HttpCookie cookie = new HttpCookie("sessionId");
						cookie.Value = sessionId;
						cookie.Expires = DateTime.Now.AddDays(1);
						Response.Cookies.Add(cookie);
					}
					else
					{
						HttpCookie cookie = new HttpCookie("sessionId");
						cookie.Value = sessionId;
						Response.Cookies.Add(cookie);
					} 
					#endregion
					return Content("ok:登录成功");
				}
				else
				{
					return Content("no:登录失败");
				}
			}
		}
		#endregion

		#region 显示验证码
		public ActionResult ShowValidateCode()
		{
			ValidateCode vliateCode = new ValidateCode();
			string code = vliateCode.CreateValidateCode(4);//产生验证码
			Session["validateCode"] = code;
			byte[] buffer = vliateCode.CreateValidateGraphic(code);//将验证码画到画布上.
			return File(buffer, "image/jpeg");
		}
		#endregion
	}
}
