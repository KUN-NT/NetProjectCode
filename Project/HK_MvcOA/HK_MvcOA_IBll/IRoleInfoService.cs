using HK_MvcOA_Model;
using System.Collections.Generic;

namespace HK_MvcOA_IBll
{
	public partial interface IRoleInfoService : IBaseService<RoleInfo>
	{
		bool SetRoleActionInfo(int roleId, List<int> actionIdList);
	}
}
