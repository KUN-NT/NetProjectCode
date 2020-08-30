using CaterBll;
using CaterModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaterUI
{
    public partial class CaterUI : Form
    {
        public CaterUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagerInfoBll bll = new ManagerInfoBll();
            int type;
            ManagerStute stute = bll.Login(textBox1.Text, textBox2.Text,out type);
            switch (stute)
            {
                case ManagerStute.OK:
                    FormMain main = new FormMain();
                    main.Tag = type;
                    main.Show();
                    this.Hide();
                    break;
                case ManagerStute.NAMEERROR:
                    MessageBox.Show("用户名错误");
                    break;
                case ManagerStute.PWDERROR:
                    MessageBox.Show("密码错误");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
