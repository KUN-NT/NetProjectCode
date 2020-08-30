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
    public partial class FormOrderPay : Form
    {
        //public delegate void CloaeTab(string name);
        public Action<string> close;
        public Action Refresh;
        public FormOrderPay()
        {
            InitializeComponent();
        }

        private void FormOrderPay_Load(object sender, EventArgs e)
        {
            orderId = Convert.ToInt32(this.Tag);
            LoadList();
        }

        OrderInfoBll OrderBll = new OrderInfoBll();
        int money = 0;
        public void LoadList()
        {
            if (this.Tag != null)
            {
                money = OrderBll.GetTotalMoneyByOrderId(Convert.ToInt32(this.Tag));
                lblPayMoney.Text = money.ToString();
                lblPayMoneyDiscount.Text = lblPayMoney.Text;
            }
        }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkMember.Checked)
            {
                gbMember.Enabled = true;
            }
            else
            {
                gbMember.Enabled = false;
            }
        }

        private void txtId_MouseLeave(object sender, EventArgs e)
        {
            MemberInfoBll bll = new MemberInfoBll();
            try
            {
                MemberInfo model = bll.SelectMember(txtId.Text);

                lblMoney.Text = model.MMoney.ToString();
                lblTypeTitle.Text = model.MType;
                lblDiscount.Text = model.MDiscount;
                double sum = money * (Convert.ToDouble(model.MDiscount));
                lblPayMoneyDiscount.Text = sum.ToString();
            }
            catch
            {
                MessageBox.Show("用户不存在");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            close("page5");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close("page5");
        }
        private int orderId;
        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            OrderPayBll oBll = new OrderPayBll();
            if (oBll.Pay(cbkMoney.Checked, int.Parse(txtId.Text), Convert.ToDecimal(lblPayMoneyDiscount.Text), orderId,Convert.ToDecimal(lblDiscount.Text)))
            {
                //MessageBox.Show("结账成功");
                close("page5");
                Refresh();
            }
            else
            {
                MessageBox.Show("结账失败");
            }
           
        }

    }
}
