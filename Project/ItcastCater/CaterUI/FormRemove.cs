using CaterBll;
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
    public partial class FormRemove : Form
    {
        #region 初始化窗体
        private static FormRemove form;
        public static FormRemove Create()
        {
            if (form == null)
            {
                form = new FormRemove();
            }
            return form;
        }
        private FormRemove()
        {
            InitializeComponent();
        }

        private void FormRemove_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        private void LoadList()
        {
            RemoveInfoBll bll = new RemoveInfoBll();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }
        RemoveInfoBll bll = new RemoveInfoBll(); 
        #endregion

        #region 还原
        private void ReturnData_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            string id = rows[0].Cells[0].Value.ToString();
            string table = rows[0].Cells[2].Value.ToString();
            if (bll.Return(id, table))
            {
                LoadList();
            }
        } 
        #endregion

        #region 删除
        private void DelData_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            string id = rows[0].Cells[0].Value.ToString();
            string table = rows[0].Cells[2].Value.ToString();
            if (bll.Delete(id, table))
            {
                LoadList();
            }
        } 
        #endregion

        private void FormRemove_FormClosing(object sender, FormClosingEventArgs e)
        {
            form = null;
        }
    }
}
