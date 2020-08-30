using CaterBll;
using CaterComman;
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
    public partial class FormDishInfo : Form
    {
        public delegate void CloaeTab(string name);
        public CloaeTab close;
        #region LoadForm
        public FormDishInfo()
        {
            InitializeComponent();
        }
        DishInfoBll bll = new DishInfoBll();
        private void FormDishInfo_Load(object sender, EventArgs e)
        {
            Category();
            LoadList();
            LoadAddType();
        } 
        #endregion

        #region 分类管理
        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormDishTypeInfo type = FormDishTypeInfo.Create();
            DialogResult result = type.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadList();
                Category();
                LoadAddType();
            }
        } 
        #endregion

        #region 查询全部
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            ddlTypeSearch.SelectedIndex = 0;
            LoadList();
        } 
        #endregion

        #region 添加修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            DishInfo model = new DishInfo()
            {
                DTitle = txtTitleSave.Text,
                DTypeId = (int)ddlTypeAdd.SelectedValue,
                DPrice = Convert.ToDecimal(txtPrice.Text),
                DChar = txtChar.Text
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
                model.DId = Convert.ToInt32((txtId.Text));
                if (!bll.Update(model))
                {
                    MessageBox.Show("修改失败");
                }
            }
            Reset();
            LoadList();
        } 
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
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
                        RTable = "DishInfo"
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

        #region 其他

        private int index = -1;
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "修改";
        }

        private void Reset()
        {
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            txtPrice.Text = "";
            txtChar.Text = "";
            btnSave.Text = "添加";
        }

        private void txtTitleSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void txtTitleSave_MouseLeave(object sender, EventArgs e)
        {
            txtChar.Text = PimYinhelper.GetPinYin(txtTitleSave.Text);
        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtTitleSearch.Text))
            {
                dic.Add("DChar ", txtTitleSearch.Text);
            }
            if (ddlTypeSearch.SelectedValue.ToString() != "0")
            {
                dic.Add("DTypeId", ddlTypeSearch.SelectedValue.ToString());
            }
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList(dic);

            if (index > -1)
            {
                dgvList.Rows[index].Selected = true;
            }
        }
        private void LoadAddType()
        {
            ddlTypeAdd.DataSource = typebll.GetList();
            ddlTypeAdd.DisplayMember = "DTitle";
            ddlTypeAdd.ValueMember = "DId";
        }
        DishTypeInfoBll typebll = new DishTypeInfoBll();
        public void Category()
        {
            //DishTypeInfoBll typebll = new DishTypeInfoBll();
            List<DishTypeInfo> list = typebll.GetList();
            list.Insert(0, new DishTypeInfo()
            {
                DId = 0,
                DTitle = "全部"
            });

            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.DisplayMember = "DTitle";
            ddlTypeSearch.ValueMember = "DId";

            //Error:不能这样写 会将两个下拉框关联起来
            //ddlTypeAdd.DataSource = typebll.GetList();
            //ddlTypeAdd.DisplayMember = "DTitle";
            //ddlTypeAdd.ValueMember = "DId";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            close("page3");
        }
        #endregion

    }
}
