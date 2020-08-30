using System;
using System.Linq;
using System.Linq.Expressions;

namespace HK_MvcOA_IDal
{
	/// <summary>
	/// 封装各个数据操作了类常用的公共方法
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public partial interface IBaseDal<T> where T:class,new()
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="whereLambda">查询条件</param>
		/// <returns></returns>
		IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
		/// <summary>
		/// 分页
		/// </summary>
		/// <typeparam name="S">排序字段类型</typeparam>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">页容量</param>
		/// <param name="pageCount">数据总数</param>
		/// <param name="whereLambda">筛选条件</param>
		/// <param name="orderbyLambda">排序条件</param>
		/// <param name="isAsc">排序方式</param>
		/// <returns></returns>
		IQueryable<T>LoadPageEntities<S>(int pageIndex, int pageSize, out int pageCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc);
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="entity">删除数据</param>
		/// <returns></returns>
		bool DeleteEntity(T entity);
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="entity">更新数据</param>
		/// <returns></returns>
		bool UpdateEntity(T entity);
		/// <summary>
		/// 添加
		/// </summary>
		/// <param name="entity">添加数据</param>
		/// <returns></returns>
		T AddEntity(T entity);
	}
}
