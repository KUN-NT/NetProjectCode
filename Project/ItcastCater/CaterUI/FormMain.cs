using CaterBll;
using CaterModel;
using SimplePlayerApp.Controls;
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
    public partial class FormMain : Form
    {
        #region 初始化窗体
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadHallInfo();
            NPOIShow();
            //访问权限
            int type = Convert.ToInt32(this.Tag);
            if (type == 0)
            {
                UserMenu.Visible = false;
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region 加载厅包 点菜
        private void LoadHallInfo()
        {
            //2.1、获取所有的厅包对象
            HallInfoBll hiBll = new HallInfoBll();
            var list = hiBll.GetList();
            //2.2、遍历集合，向标签页中添加信息
            tabControl1.TabPages.Clear();
            TableInfoBll tiBll = new TableInfoBll();
            foreach (var hi in list)
            {
                //根据厅包对象创建标签页对象
                TabPage tp = new TabPage(hi.HTitle);

                //3.1、动态创建列表添加到标签页上
                ListView lvTableInfo = new ListView();
                //添加双击事件，完成开单功能
                lvTableInfo.DoubleClick += lvTableInfo_DoubleClick;
                //3.2、让列表使用图片
                lvTableInfo.LargeImageList = imageList1;
                lvTableInfo.Dock = DockStyle.Fill;
                //3.3、将listview添加到标签页中
                tp.Controls.Add(lvTableInfo);

                //4.1、获取当前厅包对象的所有餐桌
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("thallid", hi.HId.ToString());
                var listTableInfo = tiBll.GetList(dic);
                //4.2、向列表中添加餐桌信息
                foreach (var ti in listTableInfo)
                {
                    var lvi = new ListViewItem(ti.TTitle, ti.TIsFree ? 0 : 1);
                    //后续操作需要用到餐桌编号，所以在这里存一下
                    lvi.Tag = ti.TId;

                    lvTableInfo.Items.Add(lvi);
                }

                //2.3、将标签页加入标签容器
                tabControl1.TabPages.Add(tp);
            }
        }
        void lvTableInfo_DoubleClick(object sender, EventArgs e)
        {
            //获取被点的餐桌项
            var lv1 = sender as ListView;
            var lvi = lv1.SelectedItems[0];
            //获取餐桌编号
            int tableId = Convert.ToInt32(lvi.Tag);
            OrderInfoBll oiBll = new OrderInfoBll();

            if (lvi.ImageIndex == 0)
            {
                //当前餐桌为空闲则开单
                //1、开单，向orderinfo表插入数据
                //2、修改餐桌状态为占用
                int orderId = oiBll.KaiDan(tableId);
                //单号
                lvi.Tag = orderId;

                //3、更新项的图标为占用
                lv1.SelectedItems[0].ImageIndex = 1;
            }
            else
            {
                //当前餐桌已经占用，则进行点菜操作 根据tableid获取orderid
                lvi.Tag = oiBll.GetOrderIdByTableId(tableId);
            }

            //4、打开点菜窗体
            FormOrderDish formOrderDish = new FormOrderDish();
            //将单号传入
            formOrderDish.Tag = lvi.Tag;
            formOrderDish.CloseEvrnt += LoadHallInfo;
            formOrderDish.Show();
        }
        #endregion

        #region 选项卡 NPOI
        private void NPOIShow()
        {
            TabPage tabPage = new TabPage();
            //设置选项卡文本  
            tabPage.Text = "NPOI";
            //选项卡名字（可以通过这个名字来访问到tabControl1中的选项卡）  
            tabPage.Name = "page6";
            if (!IsInto(tabPage.Name))
            {
                //如果选项卡内的控件比较多，则可以添加一个Form控件，但是Form空间的TopLevel要设置为false  
                NOPITest page = new NOPITest();
                //page.close += Close;
                page.Name = "formpage";
                page.TopLevel = false;
                //给Form去边框  
                page.FormBorderStyle = FormBorderStyle.None;
                //把page添加到tabPage中  
                tabPage.Controls.Add(page);
                //在tabPage选项卡中显示出来  
                page.Show();
                //添加选项卡tabPage到TabControl中  
                tabControl1.TabPages.Add(tabPage);
            }
            this.tabControl1.SelectedTab = tabControl1.TabPages["page6"];
        }
        #endregion

        #region 选项卡 员工管理
        private void UserMenu_Click(object sender, EventArgs e)
        {
            //CaterMain main = CaterMain.Create();
            //main.Show();
            //main.Focus();

            #region 选项卡方式显示
            //创建一个TabPage  
            TabPage tabPage = new TabPage();
            //设置选项卡文本  
            tabPage.Text = "店员管理";
            //选项卡名字（可以通过这个名字来访问到tabControl1中的选项卡）  
            tabPage.Name = "page1";
            if (!IsInto(tabPage.Name))
            {
                //如果选项卡内的控件比较多，则可以添加一个Form控件，但是Form空间的TopLevel要设置为false  
                CaterMain page = new CaterMain();
                page.close += Close;
                page.Name = "formpage";
                page.TopLevel = false;
                //给Form去边框  
                page.FormBorderStyle = FormBorderStyle.None;
                //把page添加到tabPage中  
                tabPage.Controls.Add(page);
                //在tabPage选项卡中显示出来  
                page.Show();
                //添加选项卡tabPage到TabControl中  
                tabControl1.TabPages.Add(tabPage);
            }
            this.tabControl1.SelectedTab = tabControl1.TabPages["page1"];
            #endregion
        }
        #endregion

        #region 选项卡 Vip管理
        private void VipMenu_Click(object sender, EventArgs e)
        {
            //FormMemberInfo info = FormMemberInfo.Create();
            //info.Show();
            //info.Focus();

            TabPage tabPage = new TabPage();
            tabPage.Text = "Vip管理";
            tabPage.Name = "page2";

            if (!IsInto(tabPage.Name))
            {
                FormMemberInfo page = new FormMemberInfo();
                //注册关闭事件
                page.close += Close;
                page.Name = "formpage";
                page.TopLevel = false;
                page.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(page);
                page.Show();
                tabControl1.TabPages.Add(tabPage);
            }
            //选中该选项卡
            this.tabControl1.SelectedTab = tabControl1.TabPages["page2"];
        }
        #endregion

        #region 选项卡 菜单管理
        private void TeaMenu_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = "菜单管理";
            tabPage.Name = "page3";

            if (!IsInto(tabPage.Name))
            {
                FormDishInfo page = new FormDishInfo();
                //注册关闭事件
                page.close += Close;
                page.Name = "formpage";
                page.TopLevel = false;
                page.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(page);
                page.Show();
                tabControl1.TabPages.Add(tabPage);
            }
            //选中该选项卡
            this.tabControl1.SelectedTab = tabControl1.TabPages["page3"];
        }
        #endregion

        #region 选项卡 餐桌管理
        private void TaskMenu_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = "餐桌管理";
            tabPage.Name = "page4";

            if (!IsInto(tabPage.Name))
            {
                FormTableInfo page = new FormTableInfo();
                //注册关闭事件
                page.close += Close;
                page.Name = "formpage";
                page.TopLevel = false;
                page.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(page);
                page.LoadMain += NPOIShow;
                page.LoadMain += LoadHallInfo;
                page.Show();
                tabControl1.TabPages.Add(tabPage);
            }
            //选中该选项卡
            this.tabControl1.SelectedTab = tabControl1.TabPages["page4"];

        }
        #endregion

        #region 选项卡 结账
        private void MoneyMenu_Click(object sender, EventArgs e)
        {
            var listView = tabControl1.SelectedTab.Controls[0] as ListView;
            try
            {
                var lvTable = listView.SelectedItems[0];
                if (lvTable.ImageIndex == 0)
                {
                    MessageBox.Show("餐桌还未使用，无法结账");
                    return;
                }
                OrderInfoBll bll = new OrderInfoBll();
                int tableId = Convert.ToInt32(lvTable.Tag);
                int orderId = bll.GetOrderIdByTableId(tableId);

                #region 显示结账界面
                TabPage tabPage = new TabPage();
                tabPage.Text = "结账付款";
                tabPage.Name = "page5";

                if (!IsInto(tabPage.Name))
                {
                    FormOrderPay page = new FormOrderPay();
                    //注册关闭事件
                    page.close += Close;
                    page.Refresh += LoadHallInfo;
                    page.Name = "formpage";
                    page.TopLevel = false;
                    page.FormBorderStyle = FormBorderStyle.None;
                    tabPage.Controls.Add(page);
                    page.Tag = orderId;
                    page.Show();
                    tabControl1.TabPages.Add(tabPage);
                }
                //选中该选项卡
                this.tabControl1.SelectedTab = tabControl1.TabPages["page5"];
                #endregion
            }
            catch
            {
                MessageBox.Show("请选择要结账的厅包");
            }
        }
        #endregion

        #region 退出登录
        private void QuitMenu_Click(object sender, EventArgs e)
        {
            CaterUI form = new CaterUI();
            form.Show();
            this.Hide();
        }
        #endregion

        #region 窗体单例 回收站
        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRemove remove = FormRemove.Create();
            remove.Show();
            remove.Focus();
        }
        #endregion

        #region 判断选项卡名称是否已存在
        private bool IsInto(string name)
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                if (name == page.Name)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 关闭选项卡
        public void Close(string tabName)
        {
            this.tabControl1.TabPages.Remove(tabControl1.TabPages[tabName]);
        }
        #endregion
    }
}
