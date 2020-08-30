using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlListDemo
{
    public partial class FormData : Form
    {
        public delegate void CloaeTab(string name);
        public CloaeTab close;
        public FormData()
        {
            InitializeComponent();
        }

        private void FormData_Load(object sender, EventArgs e)
        {
            #region 手动连接
            ////string conStr = @"data source=E:\AllDocuments\ITClass\15year\Means\ItcastCater.db;version=3";//Sqlite数据库连接语句
            //string conStr = ConfigurationManager.ConnectionStrings["conter"].ConnectionString;
            ////0、准备好存储数据的集合
            //List<ManagerInfo> manager = new List<ManagerInfo>();
            ////1、创建连接对象
            //using (SQLiteConnection con = new SQLiteConnection(conStr))
            //{
            //    //2、创建命令对象
            //    SQLiteCommand com = new SQLiteCommand("select * from ManagerInfo", con);
            //    //3、打开连接
            //    con.Open();
            //    //4、执行命令
            //    SQLiteDataReader reader = com.ExecuteReader();
            //    //5、判断是否有数据
            //    if (reader.HasRows)
            //    {
            //        //6、逐行读取数据
            //        while (reader.Read())
            //        {
            //            //7、将数据添加到集合中
            //            manager.Add(new ManagerInfo()
            //            {
            //                MId = Convert.ToInt32(reader["MId"]),
            //                MName = reader["MName"].ToString(),
            //                MPwd = reader["MPwd"].ToString(),
            //                MType = Convert.ToInt32(reader["MType"])
            //            });
            //        }
            //        reader.Close();
            //    }
            //}
            //8、在DataGridView中显示数据
            //dataGridView1.DataSource = manager; 
            #endregion

            #region 借助sqlitehelper连接
            dataGridView1.DataSource = SqliteHelper.GetDataTable("select * from ManagerInfo");
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close("page2");
        }

    }
}
