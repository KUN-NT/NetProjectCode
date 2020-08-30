using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace HK_MvcOA_Dal
{
	/// <summary>
	/// Dal基类 封装了数据操作类的增删改查分页方法
	/// </summary>
	/// <typeparam name="T">操作数据类型</typeparam>
	public class BaseDal<T> where T : class,new()//因为BaseDal是对 实现IXXDal接口方法的XXDAl 的封装，所以没必要再实现IBaseDal接口了
	{
		//DBContainer db = new DBContainer();
		DbContext db = DBContextFactory.CreateDbContext();//保证线程内唯一
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="whereLambda"></param>
		/// <returns></returns>
		public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
		{
			return db.Set<T>().Where<T>(whereLambda);//Set为指定类型创建DbSet EF才能操作指定类型数据
		}

		/// <summary>
		/// 分页
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageCount"></param>
		/// <param name="whereLambda"></param>
		/// <param name="orderbyLambda"></param>
		/// <param name="isAsc"></param>
		/// <returns></returns>
		public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int pageCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, S>> orderbyLambda, bool isAsc)
		{
			var temp = db.Set<T>().Where<T>(whereLambda);
			pageCount = temp.Count();
			if (isAsc)
			{
				temp = temp.OrderBy<T, S>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
			}
			else
			{
				temp = temp.OrderByDescending<T, S>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
			}
			return temp;
		}

		//仅仅打上操作标记 在数据会话层再保存数据
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool DeleteEntity(T entity)
		{
			db.Entry<T>(entity).State = EntityState.Deleted;
			//return db.SaveChanges() > 0;
			return true;
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateEntity(T entity)
		{
			db.Entry<T>(entity).State = EntityState.Modified;
			//return db.SaveChanges() > 0;
			return true;
		}

		/// <summary>
		/// 添加
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public T AddEntity(T entity)
		{
			db.Set<T>().Add(entity);
			//db.SaveChanges();
			return entity;
		}
	}
}
