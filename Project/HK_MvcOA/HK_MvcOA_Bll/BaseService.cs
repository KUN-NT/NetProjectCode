using HK_MvcOA_DalFactory;
using HK_MvcOA_IDal;
using System;
using System.Linq;

namespace HK_MvcOA_Bll
{
	/// <summary>
	/// 业务基类 完成DBSession调用
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BaseService<T> where T:class,new()
	{
		/// <summary>
		/// 数据会话类对象
		/// </summary>
		public IDBSession currentDBSession
		{
			get
			{
				return DBSessionFactory.CreateDBSession();
			}
		}

		#region 获取当前业务操作类
		//BaseService并不知道要操作的数据类是什么 而子类知道 所以让子类实现SetCurrentDal抽象方法 将数据操作类的类型赋给CurrentDal 
		//然后将SetCurrentDal抽象方法放入构造方法中 这样初始化BaseService就可以取得数据操作类了
		public IBaseDal<T> CurrentDal { get; set; }
		public abstract void SetCurrentDal();
		public BaseService()
		{
			SetCurrentDal();
		} 
		#endregion

		public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
		{
			return CurrentDal.LoadEntities(whereLambda);
		}
		public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
		{
			return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
		}
		//这里的方法用于单次sql操作
		public bool DeleteEntity(T entity)
		{
			CurrentDal.DeleteEntity(entity);
			return currentDBSession.SaveChanges();
		}
		public bool UpdateEntity(T entity)
		{
			CurrentDal.UpdateEntity(entity);
			return currentDBSession.SaveChanges();
		}
		public T AddEntity(T entity)
		{
			CurrentDal.AddEntity(entity);
			currentDBSession.SaveChanges();
			return entity;
		}
	}
}
