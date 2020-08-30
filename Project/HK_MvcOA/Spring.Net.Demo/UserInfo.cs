
namespace Spring.Net.Demo
{
	public class UserInfo:IUserInfo
	{
		public string str { get; set; }
		public Person person { get; set; }
		public string ShowMsg()
		{
			return "Hello"+str+person.Age;
		}
	}
}
