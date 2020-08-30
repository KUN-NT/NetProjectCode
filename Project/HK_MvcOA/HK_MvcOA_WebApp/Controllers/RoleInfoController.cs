using HK_MvcOA_IBll;
using HK_MvcOA_Model;
using HK_MvcOA_Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp.Controllers
{
	public class RoleInfoController : BaseController
	{
		IRoleInfoService RoleInfoService { get; set; }
		IActionInfoService ActionInfoService { get; set; }
		public ActionResult Index()
		{
			return View();
		}
		//加载角色信息
		public ActionResult GetRoleInfoList()
		{
			int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
			int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
			int totalCount;
			short delFlag = (short)DeleteEnumType.Normarl;
			var roleList = RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == delFlag, c => c.ID, true);
			var temp = from r in roleList
					   select new { ID = r.ID, RoleName = r.RoleName, Sort = r.Sort, SubTime = r.SubTime, Remark = r.Remark };
			return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
		}
		//添加角色信息
		public ActionResult ShowAddInfo()
		{
			return View();
		}
		public ActionResult AddRoleInfo(RoleInfo roleInfo)
		{
			roleInfo.ModifiedOn = DateTime.Now;
			roleInfo.SubTime = DateTime.Now;
			roleInfo.DelFlag = 0;
			RoleInfoService.AddEntity(roleInfo);
			return Content("ok");
		}
		//为角色分配权限
		public ActionResult ShowRoleAction()
		{
			int id = int.Parse(Request["id"]);
			var roleInfo = RoleInfoService.LoadEntities(r => r.ID == id).FirstOrDefault();//获取要分配的权限的角色信息。
			ViewBag.RoleInfo = roleInfo;
			//获取所有的权限。
			short delFlag = (short)DeleteEnumType.Normarl;
			var actionInfoList = ActionInfoService.LoadEntities(a => a.DelFlag == delFlag).ToList();
			//要分配权限的角色以前有哪些权限。
			var actionIdList = (from a in roleInfo.ActionInfo
								select a.ID).ToList();
			ViewBag.ActionInfoList = actionInfoList;
			ViewBag.ActionIdList = actionIdList;
			return View();
		}
		public ActionResult SetRoleAction()
		{
			int roleId = int.Parse(Request["roleId"]);//获取角色编号
			string[] allKeys = Request.Form.AllKeys;//获取所有表单元素name属性的值。
			List<int> list = new List<int>();
			foreach (string key in allKeys)
			{
				if (key.StartsWith("cba_"))
				{
					string k = key.Replace("cba_", "");
					list.Add(Convert.ToInt32(k));
				}
			}
			if (RoleInfoService.SetRoleActionInfo(roleId, list))
			{
				return Content("ok");
			}
			else
			{
				return Content("no");
			}
		}
	}
}
