using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_MvcOA_Model
{
	/// <summary>
	/// 消除重复项
	/// </summary>
	public class EqualityComparer : IEqualityComparer<ActionInfo>
	{
		public bool Equals(ActionInfo x, ActionInfo y)
		{
			return x.ID == y.ID;
		}

		public int GetHashCode(ActionInfo obj)
		{
			return obj.GetHashCode();
		}
	}
}
