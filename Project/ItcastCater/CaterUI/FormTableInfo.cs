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
    public partial class FormTableInfo : Form
    {
        public event Action LoadMain;
        #region 加载界面
        public delegate void CloaeTab(string name);
        public CloaeTab close;
        public FormTableInfo()
        {
            InitializeComponent();
        }
        TableInfoBll bll = new TableInfoBll();
        HallInfoBll hallBll = new HallInfoBll();
        private void FormTableInfo_Load(object sender, EventArgs e)
        {
            LoadFreeSearchList();
            LoadList();
        }
        #endregion
       
        #region 加载数据
        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (ddlHallSearch.SelectedIndex>0)
            {
                 dic.Add("THallId",ddlHallSearch.SelectedValue.ToString());
            }
            if (ddlFreeSearch.SelectedIndex>0)
            {
                dic.Add("TIsFree", ddlFreeSearch.SelectedValue.ToString());
            }
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList(dic);

            if (index != -1)
            {
                dgvList.Rows[index].Selected = true;
            }
        }

        private void LoadFreeSearchList()
        {
            var list = hallBll.GetList();

            list.Insert(0, new HallInfo()
            {
                HId = 0,
                HTitle = "全部"
            });
            ddlHallSearch.DataSource = list;
            ddlHallSearch.ValueMember = "hid";
            ddlHallSearch.DisplayMember = "htitle";

            ddlHallAdd.DataSource = hallBll.GetList();
            ddlHallAdd.ValueMember = "hid";
            ddlHallAdd.DisplayMember = "htitle";

            List<DdlModel> listDdl = new List<DdlModel>()
            {
                new DdlModel("-1","全部"),
                new DdlModel("1","空闲"),
                new DdlModel("0","使用中")
            };
            ddlFreeSearch.DataSource = listDdl;
            ddlFreeSearch.ValueMember = "id";
            ddlFreeSearch.DisplayMember = "title";
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "√" : "×";
            }
        }

        private void Reset()
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
            btnSave.Text = "添加";
        }
        #endregion

        #region 增删改查
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlFreeSearch.SelectedIndex = 0;
            ddlHallSearch.SelectedIndex = 0;
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TableInfo model = new TableInfo()
            {
                TTitle = txtTitle.Text,
                THallId = Convert.ToInt32(ddlHallAdd.SelectedValue.ToString()),
                TIsFree = rbFree.Checked == true ? true : false
            };
            if (btnSave.Text == "添加")
            {
                if (bll.Add(model))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败");
                    return;
                }
            }
            else
            {
                model.TId = Convert.ToInt32(txtId.Text);
                if (bll.Update(model))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                    return;
                }
            }
            LoadMain();
            Reset();
        }

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
                        RTable = "TableInfo"
                    };
                    RemoveInfoBll removebll = new RemoveInfoBll();
                    removebll.Add(remove);
                    result = DialogResult.OK;
                    LoadList();
                }
                LoadMain();
            }
            else
            {
                MessageBox.Show("请选择一行数据");
            }
        } 
        #endregion

        #region 厅包管理
        private void btnAddHall_Click(object sender, EventArgs e)
        {
            FormHallInfo hall = FormHallInfo.Create();
            DialogResult result = hall.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadFreeSearchList();
                LoadList();
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
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            var a = Convert.ToBoolean(row.Cells[3].Value) == true ? rbFree.Checked = true : rbUnFree.Checked = true;
            btnSave.Text = "修改";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close("page4");
        }

        private void ddlHallSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlFreeSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadList();
        } 
        #endregion

    }
}
