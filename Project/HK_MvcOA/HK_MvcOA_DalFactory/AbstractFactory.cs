using HK_MvcOA_IDal;
using System.Configuration;
using System.Reflection;

namespace HK_MvcOA_DalFactory
{
	/// <summary>
	/// 抽象工厂类 通过反射的形式创建类的实例
	/// </summary>
	public partial class AbstractFactory
	{
		private static readonly string assemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
		private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpacePath"];
		//改由T4模板中生成
		//public static IUserInfoDal CreateUserInfoDal()
		//{
		//	string fullName = nameSpacePath + ".UserInfoDal";
		//	return CreateMyInstace(fullName) as IUserInfoDal;
		//}
		private static object CreateInstance(string fullName)
		{
			var assembly = Assembly.Load(assemblyPath);//加载程序集
			return assembly.CreateInstance(fullName);//创建该程序集下相应(命名空间+类名)实例
		}
	}
}
