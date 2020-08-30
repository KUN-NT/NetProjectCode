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
    public partial class FormDishTypeInfo : Form
    {
        #region 初始化窗体
        private static FormDishTypeInfo form;
        public static FormDishTypeInfo Create()
        {
            if (form == null)
            {
                form = new FormDishTypeInfo();
            }
            return form;
        }
        private DialogResult resulted = DialogResult.Cancel;
        DishTypeInfoBll bll = new DishTypeInfoBll();
        private FormDishTypeInfo()
        {
            InitializeComponent();
        }

        private void FormDishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        } 
        #endregion

        #region 添加和修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            DishTypeInfo model = new DishTypeInfo()
            {
                DTitle = txtTitle.Text
            };
            if (btnSave.Text == "添加")
            {
                if (!bll.Add(model))
                {
                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                model.DId = Convert.ToInt32(txtId.Text);
                if (!bll.Update(model))
                {
                    MessageBox.Show("修改失败");
                }
            }
            Reset();
            LoadList();
            resulted = DialogResult.OK;
        } 
        #endregion

        #region 删除
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            string id = rows[0].Cells[0].Value.ToString();
            if (rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (bll.Delete(id))
                {
                    RemoveInfo remove = new RemoveInfo()
                    {
                        RId = Convert.ToInt32(id),
                        RName = rows[0].Cells[1].Value.ToString(),
                        RTable = "DishTypeInfo"
                    };
                    RemoveInfoBll removebll = new RemoveInfoBll();
                    removebll.Add(remove);
                    LoadList();
                }
            }
            resulted = DialogResult.OK;
        } 
        #endregion

        #region 辅助方法
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void FormDishTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = resulted;
        } 
        #endregion
    }
}
