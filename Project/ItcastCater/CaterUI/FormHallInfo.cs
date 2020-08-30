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
    public partial class FormHallInfo : Form
    {
        private static FormHallInfo form;
        public static FormHallInfo Create()
        {
            if (form == null)
            {
                form = new FormHallInfo();
            }
            return form;
        }
        private FormHallInfo()
        {
            InitializeComponent();
        }
        HallInfoBll bll = new HallInfoBll();
        private DialogResult result = DialogResult.Cancel;
        private void FormHallInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo model = new HallInfo()
            {
                HTitle=txtTitle.Text
            };
            if (btnSave.Text == "添加")
            {
                if (!bll.Add(model))
                {
                    MessageBox.Show("添加失败");
                    return;
                }
                else
                {
                    result = DialogResult.OK;
                    LoadList();
                }
            }
            else
            {
                model.HId = Convert.ToInt32(txtId.Text);
                if (!bll.Update(model))
                {
                    MessageBox.Show("修改失败");
                    return;
                }
                else
                {
                    result = DialogResult.OK;
                    LoadList();
                }
            }
            Reset();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows;
            if (row.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                string id = row[0].Cells[0].Value.ToString();
                if (bll.Delete(id))
                {
                    RemoveInfo remove = new RemoveInfo()
                    {
                        RId = Convert.ToInt32(id),
                        RName = row[0].Cells[1].Value.ToString(),
                        RTable = "HallInfo"
                    };
                    RemoveInfoBll removebll = new RemoveInfoBll();
                    removebll.Add(remove);
                    result = DialogResult.OK;
                    LoadList();
                }
            }
            else
            {
                MessageBox.Show("请选择一条数据");
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "修改";
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }

        private void Reset()
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void FormHallInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = result;
        }
    }
}
