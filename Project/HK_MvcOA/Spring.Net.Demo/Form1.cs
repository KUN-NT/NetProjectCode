using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spring.Net.Demo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Test_Click(object sender, EventArgs e)
		{
			IApplicationContext ctx = ContextRegistry.GetContext();
			IUserInfo lister = (IUserInfo)ctx.GetObject("UserInfo");
			MessageBox.Show(lister.ShowMsg());
		}
	}
}
