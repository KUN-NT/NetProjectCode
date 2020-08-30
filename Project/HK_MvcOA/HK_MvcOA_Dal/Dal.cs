 
using HK_MvcOA_IDal;
using HK_MvcOA_Model;

namespace HK_MvcOA_Dal
{
		
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class BooksDal :BaseDal<Books>,IBooksDal
    {

    }
		
	public partial class DepartmentDal :BaseDal<Department>,IDepartmentDal
    {

    }
		
	public partial class KeyWordsRankDal :BaseDal<KeyWordsRank>,IKeyWordsRankDal
    {

    }
		
	public partial class R_UserInfo_ActionInfoDal :BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class SearchDetailsDal :BaseDal<SearchDetails>,ISearchDetailsDal
    {

    }
		
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {

    }
	
}