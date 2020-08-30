using HK_MvcOA_Dal;
using HK_MvcOA_IDal;
using System.Data.Entity;

namespace HK_MvcOA_DalFactory
{
	/// <summary>
	/// 数据会话层(就是一个工厂类)
	/// 1、负责完成数据操作类实例的创建(业务层、数据层解耦)
	/// </summary>
	public partial class DBSession:IDBSession
	{
		//DBContainer db = new DBContainer();
		public DbContext db
		{
			get
			{
				return DBContextFactory.CreateDbContext();
			}
		}
		//改由T4模板生成
		//private IUserInfoDal _userInfoDal;
		//public IUserInfoDal userInfoDal
		//{
		//	get
		//	{
		//		if (_userInfoDal == null)
		//		{
		//			//_userInfoDal = new UserInfoDal();
		//			_userInfoDal = AbstractFactory.CreateIUserInfoDal();//通过抽象工厂封装类实例的创建 将数据层与数据会话层解耦
		//		}
		//		return _userInfoDal;
		//	}
		//	set
		//	{
		//		_userInfoDal = value;
		//	}
		//}
		/// <summary>
		///2、 一个业务经常要操作多张表 为提高效率 一次请求完成多张表的操作(工作单元模式)
		/// </summary>
		/// <returns></returns>
		public bool SaveChanges()
		{
			return db.SaveChanges()>0;
		}
	}
}
