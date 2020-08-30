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
    public partial class FormOrderDish : Form
    {
        #region 初始化窗体
        public FormOrderDish()
        {
            InitializeComponent();
        }

        private void FormOrderDish_Load(object sender, EventArgs e)
        {
            LoadAddType();
            LoadList();
            LoadDetailList();
        }
        #endregion

        #region 加载数据
        DishInfoBll mBll = new DishInfoBll();
        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                dic.Add("DChar ", txtTitle.Text);
            }
            if (ddlType.SelectedIndex != 0)
            {
                dic.Add("DTypeId", ddlType.SelectedValue.ToString());
            }
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = mBll.GetList(dic);
        }
        DishTypeInfoBll typeBll = new DishTypeInfoBll();
        private void LoadAddType()
        {
            List<DishTypeInfo> list = typeBll.GetList();
            list.Insert(0, new DishTypeInfo()
            {
                DId = 0,
                DTitle = "全部"
            });

            ddlType.DataSource = list;
            ddlType.DisplayMember = "DTitle";
            ddlType.ValueMember = "DId";
        }
        private void LoadDetailList()
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = bll.GetDetailList(Convert.ToInt32(this.Tag));
            Square();
        }
        #endregion

        #region 查询
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }
        #endregion

        #region 双击点菜
        OrderInfoBll bll = new OrderInfoBll();
        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //菜品编号
            int Did = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);
            //订单号
            int Oid = Convert.ToInt32(this.Tag);

            if (bll.DianCai(Did, Oid))
            {
                LoadDetailList();
                Square();
            }
        }
        #endregion

        #region 删除菜品
        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            var row = dgvOrderDetail.SelectedRows;
            if (row.Count > 0)
            {
                string id=row[0].Cells[0].Value.ToString();
                if (bll.Delete(id))
                {
                    LoadDetailList();
                }
            }
            else
            {
                MessageBox.Show("请选择一行数据");
            }
        }
        #endregion

        #region 下单
        private void btnOrder_Click(object sender, EventArgs e)
        {
            //获取订单编号
            int orderId = Convert.ToInt32(this.Tag);
            //获取总金额
            decimal money = Convert.ToDecimal(lblMoney.Text);
            //更新订单
            if (bll.SetOrderMoney(orderId, money))
            {
                MessageBox.Show("下单成功");
            }
        }
        #endregion

        #region 取消订单
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要取消订单吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            bll.CancellationOfOrder(this.Tag.ToString());
            this.Close();
        } 
        #endregion

        #region 算账
        private void Square()
        {
            int num = 0;
            var rows = dgvOrderDetail.Rows;
            foreach (DataGridViewRow row in rows)
            {
                num += Convert.ToInt32(row.Cells[2].Value) * Convert.ToInt32(row.Cells[3].Value);
            }
            lblMoney.Text = num.ToString();
            //int orderId = Convert.ToInt32(this.Tag);
            //lblMoney.Text = bll.GetTotalMoneyByOrderId(orderId).ToString();
        } 
        #endregion

        #region 加量
        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var row = dgvOrderDetail.Rows[e.RowIndex];
                string oid = row.Cells[0].Value.ToString();
                string count = row.Cells[2].Value.ToString();
                bll.GetCountByOId(oid, count);
            }
            Square();
        } 
        #endregion

        public event Action CloseEvrnt;
        private void FormOrderDish_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseEvrnt();
        }

        
    }
}
