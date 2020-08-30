using HK_MvcOA_IBll;
using HK_MvcOA_Model;
using System.Collections.Generic;
using System.Linq;

namespace HK_MvcOA_Bll
{
	public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
	{
		/// <summary>
		/// 获取角色信息.
		/// </summary>
		/// <param name="roleId">角色ID</param>
		/// <param name="actionIdList">要分配的权限集合</param>
		/// <returns></returns>
		public bool SetRoleActionInfo(int roleId, List<int> actionIdList)
		{
			var roleInfo = this.currentDBSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
			if (roleInfo != null)
			{
				roleInfo.ActionInfo.Clear();
				foreach (int actionId in actionIdList)
				{
					var actionInfo = this.currentDBSession.ActionInfoDal.LoadEntities(a => a.ID == actionId).FirstOrDefault();
					roleInfo.ActionInfo.Add(actionInfo);
				}
				return this.currentDBSession.SaveChanges();
			}
			return false;
		}
	}
}
