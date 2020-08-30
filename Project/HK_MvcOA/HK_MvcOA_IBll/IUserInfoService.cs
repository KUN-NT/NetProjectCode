using HK_MvcOA_Model;
using HK_MvcOA_Model.Search;
using System.Collections.Generic;
using System.Linq;

namespace HK_MvcOA_IBll
{
	/// <summary>
	/// 表现层调用业务层接口方法
	/// </summary>
	public partial interface IUserInfoService : IBaseService<UserInfo>
	{
		bool DeleteEntities(List<int> list);
		IQueryable<UserInfo> LoadSearchEntities(UserInfoSearch userInfoSearch, short delFlag);
		bool SetUserRoleInfo(int userId, List<int> roleIdList);
		bool SetUserActionInfo(int actionId, int userId, bool isPass);
	}
}
