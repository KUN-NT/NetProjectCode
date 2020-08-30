 
using HK_MvcOA_IBll;
using HK_MvcOA_Model;
using HK_MvcOA_Model.Search;
using System.Collections.Generic;
using System.Linq;

namespace HK_MvcOA_Bll
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.ActionInfoDal;
        }
    }   
	
	public partial class BooksService :BaseService<Books>,IBooksService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.BooksDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.DepartmentDal;
        }
    }   
	
	public partial class KeyWordsRankService :BaseService<KeyWordsRank>,IKeyWordsRankService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.KeyWordsRankDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.RoleInfoDal;
        }
    }   
	
	public partial class SearchDetailsService :BaseService<SearchDetails>,ISearchDetailsService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.SearchDetailsDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
		 public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.UserInfoDal;
        }
    }   
	
}