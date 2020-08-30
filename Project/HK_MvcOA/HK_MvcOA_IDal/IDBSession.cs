using System.Data.Entity;

namespace HK_MvcOA_IDal
{
	/// <summary>
	/// 数据会话层接口
	/// 业务层调用的是数据会话层的接口 而不是直接调用业务会话层(解耦)
	/// </summary>
	public partial interface IDBSession
	{
		/// <summary>
		/// EF数据上下文对象
		/// </summary>
		DbContext db { get; }
		//改由T4模板生成
		//IUserInfoDal userInfoDal { get; set; }
		/// <summary>
		/// 保存数据的方法 这里提供此方法是为了访问数据库一次完成多项操作
		/// </summary>
		/// <returns></returns>
		bool SaveChanges();
	}
}
