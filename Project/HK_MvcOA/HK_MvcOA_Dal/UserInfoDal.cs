using HK_MvcOA_IDal;
using HK_MvcOA_Model;

namespace HK_MvcOA_Dal
{
	/// <summary>
	/// 数据操作类
	/// </summary>
	public partial class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal//继承IUserInfoDal 是为了通过IUserInfoDal访问UserInfoDal
	{
		
	}
}
