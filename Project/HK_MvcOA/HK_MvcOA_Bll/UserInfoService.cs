using HK_MvcOA_IBll;
using HK_MvcOA_Model;
using HK_MvcOA_Model.Search;
using System.Collections.Generic;
using System.Linq;

namespace HK_MvcOA_Bll
{
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
	{
		//在T4模板中生成
		//public override void SetCurrentDal()
		//{
		//	CurrentDal = this.currentDBSession.UserInfoDal;
		//}

		/// <summary>
		/// 批量删除
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public bool DeleteEntities(List<int> list)
		{
			var userInfoList = this.currentDBSession.UserInfoDal.LoadEntities(u => list.Contains(u.ID));
			foreach (var user in userInfoList)
			{
				this.currentDBSession.UserInfoDal.DeleteEntity(user);
			}
			return this.currentDBSession.SaveChanges();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="userInfoSearch"></param>
		/// <param name="delFlag"></param>
		/// <returns></returns>
		public IQueryable<UserInfo> LoadSearchEntities(UserInfoSearch userInfoSearch, short delFlag)
		{
			var temp = this.currentDBSession.UserInfoDal.LoadEntities(c => c.DelFlag==delFlag);
			//根据用户名来搜索
			if (!string.IsNullOrEmpty(userInfoSearch.UserName))
			{
				temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoSearch.UserName));
			}
			if (!string.IsNullOrEmpty(userInfoSearch.UserId))
			{
				int id;
				bool isInt = int.TryParse(userInfoSearch.UserId, out id);
				if(isInt)
				temp = temp.Where<UserInfo>(u => u.ID==id);
			}
			userInfoSearch.TotalCount = temp.Count();
			return temp.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoSearch.PageIndex - 1) * userInfoSearch.PageSize).Take<UserInfo>(userInfoSearch.PageSize);
		}

		/// <summary>
		/// 设置角色
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="roleIdList"></param>
		/// <returns></returns>
		public bool SetUserRoleInfo(int userId, List<int> roleIdList)
		{
			var userInfo = this.currentDBSession.UserInfoDal.LoadEntities(u => u.ID == userId).FirstOrDefault();//根据用户的编号查找用户的信息
			if (userInfo != null)
			{
				userInfo.RoleInfo.Clear();
				foreach (int roleId in roleIdList)
				{
					var roleInfo = this.currentDBSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
					userInfo.RoleInfo.Add(roleInfo);
				}
				return this.currentDBSession.SaveChanges();
			}
			return false;

		}

		/// <summary>
		/// 设置权限
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="userId"></param>
		/// <param name="isPass"></param>
		/// <returns></returns>
		public bool SetUserActionInfo(int actionId, int userId, bool isPass)
		{
			//判断userId以前是否有了该actionId,如果有了只需要修改isPass状态，否则插入。
			var r_userInfo_actionInfo = this.currentDBSession.R_UserInfo_ActionInfoDal.LoadEntities(a => a.ActionInfoID == actionId && a.UserInfoID == userId).FirstOrDefault();
			if (r_userInfo_actionInfo == null)
			{
				R_UserInfo_ActionInfo userInfoActionInfo = new R_UserInfo_ActionInfo();
				userInfoActionInfo.ActionInfoID = actionId;
				userInfoActionInfo.UserInfoID = userId;
				userInfoActionInfo.IsPass = isPass;
				this.currentDBSession.R_UserInfo_ActionInfoDal.AddEntity(userInfoActionInfo);
			}
			else
			{
				r_userInfo_actionInfo.IsPass = isPass;
				this.currentDBSession.R_UserInfo_ActionInfoDal.UpdateEntity(r_userInfo_actionInfo);
			}
			return this.currentDBSession.SaveChanges();

		}
	}
}
