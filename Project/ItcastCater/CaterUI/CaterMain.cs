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
    public partial class CaterMain : Form
    {
        public delegate void CloaeTab(string name);
        public CloaeTab close;
        #region CreateUI
        //private static CaterMain _form;
        //public static CaterMain Create()
        //{
        //    if (_form == null)
        //    {
        //        _form = new CaterMain();
        //    }
        //    return _form;
        //}
        public CaterMain()
        {
            InitializeComponent();
        }

        private void CaterMain_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        //显示数据
        private void LoadList()
        {
            ManagerInfoBll bll = new ManagerInfoBll();
            //禁用列的自动生成
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }
        #endregion

        #region 添加和修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            ManagerInfo manager = new ManagerInfo()
                {
                    //MId=Convert.ToInt32(txtId.Text),
                    MName = txtName.Text,
                    MPwd = txtPwd.Text,
                    MType = rb1.Checked ? 1 : 0
                };
            ManagerInfoBll bll = new ManagerInfoBll();

            if (btnSave.Text == "添加")
            {
                if (bll.Insert(manager))
                {
                    LoadList();
                    //MessageBox.Show("添加成功");
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                manager.MId = Convert.ToInt32(txtId.Text);
                if (bll.Update(manager))
                {
                    LoadList();
                    //MessageBox.Show("添加成功");
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
            txtId.Text = "添加时无编号";
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "添加";
        } 
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "添加";
        } 
        #endregion

        #region 删除
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                string id = rows[0].Cells[0].Value.ToString();
                ManagerInfoBll bll = new ManagerInfoBll();
                if (bll.Delete(id))
                {
                    LoadList();
                }
            }
            else
            {
                MessageBox.Show("请选择一行数据");
            }
        } 
        #endregion

        #region 鼠标双击事件
        private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //根据当前点击的单元格 找到行与列 进行赋值
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtPwd.Text = "thisold";
            if (row.Cells[2].Value.ToString().Equals("1"))
            {
                rb1.Checked = true;
            }
            else
            {
                rb2.Checked = true;
            }
            btnSave.Text = "修改";
        } 
        #endregion

        #region 关闭窗体
        private void CaterMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_form = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close("page1");
        } 
        #endregion

        //需要设置单元格内容显示格式时发生
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //对类型列格式化处理
            if (e.ColumnIndex == 2)
            {
                e.Value = Convert.ToInt32(e.Value) == 1 ? "经理" : "店员";
            }
        }
    }
}
