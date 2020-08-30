using HK_MvcOA_IBll;
using HK_MvcOA_Model;
using HK_MvcOA_Model.EnumType;
using System.Linq;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp.Controllers
{
    public class HomeController : BaseController
    {
		IUserInfoService UserInfoService { get; set; }
		public ActionResult Index()
		{
			ViewData["name"] = LoginUser.UName;
			return View();
		}
		public ActionResult HomePage()
		{
			return View();
		}
		
		#region 菜单权限过滤
		//过滤登陆用户的菜单权限
		//1、用户-角色-权限
		//2、用户-权限
		//清除总集合中 禁止项
		//清除重复权限
		public ActionResult GetMenu()
		{
			//1: 可以按照用户---角色---权限这条线找出登录用户的权限，放在一个集合中。
			//获取登录用户的信息
			var userInfo = UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
			//根据导航属性获取登录用户的角色.
			var userRoleInfo = userInfo.RoleInfo;
			//根据登录用户的角色获取对应的菜单权限。
			short actionTypeEnum = (short)ActionTypeEnum.MenumActionType;
			var loginUserMenuActions = (from r in userRoleInfo
										from a in r.ActionInfo
										where a.ActionTypeEnum == actionTypeEnum
										select a).ToList();
			#region MyRegion
			//上述语句相当于
			//foreach(角色 in 用户角色)
			//{
			//  foreach(权限 in 角色权限)
			//  {
			//    if(权限类型=菜单权限)
			//       插入集合
			//  }
			//}

			//下面语句是错误的，allUserActions是一个大集合该集合中包含了很多小的集合，所以变量b为集合类型
			//获取角色权限集合
			//var allUserActions = from r in userRoleInfo
			//                     select r.ActionInfo;
			//根据角色找权限 角色有多个权限 所以b是一个集合
			//var mm = from b in allUserActions
			//         where b.ActionTypeEnum == actionTypeEnum
			//         select b;
			#endregion        


			// 2：可以按照用户---权限这条线找出用户的权限，放在一个集合中。
			//遍历用户权限
			var userActions = from a in userInfo.R_UserInfo_ActionInfo
							  select a.ActionInfo;
			//每个权限对应一条数据所以a不是集合
			var userMenuActions = (from a in userActions
								   where a.ActionTypeEnum == actionTypeEnum
								   select a).ToList();
			#region MyRegion
			//var user=userInfo.R_UserInfo_ActionInfo.Select(a=>a.ActionInfo).Where(b=>b.ActionTypeEnum==actionTypeEnum).ToList();

			// a.ActionInfo不是一个集合,注意理解权限表与用户权限关系表之间的对应关系
			//var userMenuActionse = from a in userInfo.R_UserInfo_ActionInfo
			//                       from b in a.ActionInfo
			//                       where b.ActionTypeEnum == actionTypeEnum
			//     
			#endregion                   select b;

			//3：将这两个集合合并成一个集合。
			loginUserMenuActions.AddRange(userMenuActions);

			//4：把禁止的权限从总的集合中清除。
			var forbidActions = (from a in userInfo.R_UserInfo_ActionInfo
								 where a.IsPass == false
								 select a.ActionInfoID).ToList();
			var loginUserAllowActions = loginUserMenuActions.Where(a => !forbidActions.Contains(a.ID));

			//5：将总的集合中的重复权限清除。
			var lastLoginUserActions = loginUserAllowActions.Distinct(new EqualityComparer());
			//6：把过滤好的菜单权限生成JSON返回。
			var temp = from a in lastLoginUserActions
					   select new { icon = a.MenuIcon, title = a.ActionInfoName, url = a.Url };
			return Json(temp, JsonRequestBehavior.AllowGet);
		} 
		#endregion
    }
}
