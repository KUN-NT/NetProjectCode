using HK_MvcOA_IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_MvcOA_IBll
{
	/// <summary>
	/// 封装业务操作类的接口
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IBaseService<T> where T:class,new()
	{
		/// <summary>
		/// 数据会话类对象
		/// </summary>
		IDBSession currentDBSession { get; }
		IBaseDal<T> CurrentDal { get; set; }
		IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
		IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);
		bool DeleteEntity(T entity);
		bool UpdateEntity(T entity);
		T AddEntity(T entity);
	}
}
