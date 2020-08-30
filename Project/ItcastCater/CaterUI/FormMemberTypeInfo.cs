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
    public partial class FormMemberTypeInfo : Form
    {
        private DialogResult resulted = DialogResult.Cancel;
        #region 创建UI
        private static FormMemberTypeInfo type;
        public static FormMemberTypeInfo Create()
        {
            if (type == null)
            {
                type = new FormMemberTypeInfo();
            }
            return type;
        }
        private FormMemberTypeInfo()
        {
            InitializeComponent();
        }

        private void FormMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        private void LoadList()
        {
            MemberTypeInfoBll bll = new MemberTypeInfoBll();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }

        private void FormMemberTypeInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            type = null;
        } 
        #endregion

        #region 添加修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberTypeInfoBll bll = new MemberTypeInfoBll();
            MemberTypeInfo model = new MemberTypeInfo()
            {
                MTitle = txtTitle.Text,
                MDiscount = Convert.ToDecimal(txtDiscount.Text),
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
                model.MId = Convert.ToInt32(txtId.Text);
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

        #region 软删除
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
                MemberTypeInfoBll bll = new MemberTypeInfoBll();
                if (bll.Delete(id))
                {
                    RemoveInfo remove = new RemoveInfo()
                    {
                        RId=Convert.ToInt32(id),
                        RName=rows[0].Cells[1].Value.ToString(),
                        RTable="MemberTypeInfo"
                    };
                    RemoveInfoBll removebll = new RemoveInfoBll();
                    removebll.Add(remove);
                    LoadList();
                }
            }
            else
            {
                MessageBox.Show("请选择一行数据");
            }
            resulted = DialogResult.OK;
        } 
        #endregion

        private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
            btnSave.Text = "修改";
        }

        public void Reset()
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMemberTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = resulted;
        }
    }
}
