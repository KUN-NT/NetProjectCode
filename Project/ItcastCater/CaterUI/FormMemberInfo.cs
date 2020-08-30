using CaterBll;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CaterUI
{
    public partial class FormMemberInfo : Form
    {
        #region 初始化窗体
        public delegate void CloaeTab(string name);
        public CloaeTab close;
        public FormMemberInfo()
        {
            InitializeComponent();
        }

        MemberInfoBll bll = new MemberInfoBll();
        private void FormMemberInfo_Load(object sender, EventArgs e)
        {
            LoadList();

            LoadTypeList();
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void txtPhoneSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadTypeList()
        {
            MemberTypeInfoBll typeBll = new MemberTypeInfoBll();
            ddlType.DataSource = typeBll.GetList();
            ddlType.ValueMember = "mid";
            ddlType.DisplayMember = "Mtitle";
        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                dic.Add("mname", txtNameSearch.Text);
            }
            if (!string.IsNullOrEmpty(txtPhoneSearch.Text))
            {
                dic.Add("Mphone", txtPhoneSearch.Text);
            }
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.SelecyAll(dic);

            if (dgvSelect > -1)
            {
                dgvList.Rows[dgvSelect].Selected = true;
            }
        }
        #endregion

        #region 查询全部
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            txtPhoneSearch.Text = "";
            LoadList();
        }
        #endregion

        #region 添加修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberInfo model = new MemberInfo()
            {
                MTypeId = Convert.ToInt32(ddlType.SelectedValue),
                MName = txtNameAdd.Text,
                MPhone = txtPhoneAdd.Text,
                MMoney = Convert.ToDecimal(txtMoney.Text),
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
            LoadList();
            Reset();
        }
        private int dgvSelect = -1;
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSelect = e.RowIndex;
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "修改";
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
                if (bll.Delete(id))
                {
                    RemoveInfo remove = new RemoveInfo()
                    {
                        RId = Convert.ToInt32(id),
                        RName = rows[0].Cells[1].Value.ToString(),
                        RTable = "MemberInfo"
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
        }
        #endregion

        #region 类型管理
        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormMemberTypeInfo type = FormMemberTypeInfo.Create();
            DialogResult result = type.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadList();
                LoadTypeList();
            }
        } 
        #endregion

        private void Reset()
        {
            txtId.Text = "添加时无编号";
            txtNameAdd.Text = "";
            txtMoney.Text = "";
            txtPhoneAdd.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "添加";
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            close("page2");
        }

        private void FormMemberInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //info = null;
        }
    }
}
