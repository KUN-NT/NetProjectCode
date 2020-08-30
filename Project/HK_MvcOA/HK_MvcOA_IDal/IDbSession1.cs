 
using System.Data.Entity;

namespace HK_MvcOA_IDal
{
	public partial interface IDBSession
    {
	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IBooksDal BooksDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IKeyWordsRankDal KeyWordsRankDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		ISearchDetailsDal SearchDetailsDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	}	
}