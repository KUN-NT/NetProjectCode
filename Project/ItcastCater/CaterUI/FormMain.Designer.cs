namespace CaterUI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.UserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.VipMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TeaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TaskMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MoneyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(773, 528);
            this.tabControl1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::CaterUI.Properties.Resources.menuBg;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserMenu,
            this.VipMenu,
            this.TeaMenu,
            this.TaskMenu,
            this.MoneyMenu,
            this.QuitMenu,
            this.aToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(773, 58);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // UserMenu
            // 
            this.UserMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UserMenu.Image = global::CaterUI.Properties.Resources.menuManager;
            this.UserMenu.Name = "UserMenu";
            this.UserMenu.Size = new System.Drawing.Size(62, 54);
            this.UserMenu.Text = "toolStripMenuItem1";
            this.UserMenu.Click += new System.EventHandler(this.UserMenu_Click);
            // 
            // VipMenu
            // 
            this.VipMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.VipMenu.Image = global::CaterUI.Properties.Resources.menuMember;
            this.VipMenu.Name = "VipMenu";
            this.VipMenu.Size = new System.Drawing.Size(62, 54);
            this.VipMenu.Text = "toolStripMenuItem2";
            this.VipMenu.Click += new System.EventHandler(this.VipMenu_Click);
            // 
            // TeaMenu
            // 
            this.TeaMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TeaMenu.Image = global::CaterUI.Properties.Resources.menuDish;
            this.TeaMenu.Name = "TeaMenu";
            this.TeaMenu.Size = new System.Drawing.Size(62, 54);
            this.TeaMenu.Text = "toolStripMenuItem4";
            this.TeaMenu.Click += new System.EventHandler(this.TeaMenu_Click);
            // 
            // TaskMenu
            // 
            this.TaskMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TaskMenu.Image = global::CaterUI.Properties.Resources.menuTable;
            this.TaskMenu.Name = "TaskMenu";
            this.TaskMenu.Size = new System.Drawing.Size(62, 54);
            this.TaskMenu.Text = "toolStripMenuItem3";
            this.TaskMenu.Click += new System.EventHandler(this.TaskMenu_Click);
            // 
            // MoneyMenu
            // 
            this.MoneyMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoneyMenu.Image = global::CaterUI.Properties.Resources.menuOrder;
            this.MoneyMenu.Name = "MoneyMenu";
            this.MoneyMenu.Size = new System.Drawing.Size(62, 54);
            this.MoneyMenu.Text = "toolStripMenuItem5";
            this.MoneyMenu.Click += new System.EventHandler(this.MoneyMenu_Click);
            // 
            // QuitMenu
            // 
            this.QuitMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.QuitMenu.Image = global::CaterUI.Properties.Resources.menuQuit;
            this.QuitMenu.Name = "QuitMenu";
            this.QuitMenu.Size = new System.Drawing.Size(62, 54);
            this.QuitMenu.Text = "toolStripMenuItem6";
            this.QuitMenu.Click += new System.EventHandler(this.QuitMenu_Click);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aToolStripMenuItem.Image = global::CaterUI.Properties.Resources.menuDish;
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(62, 54);
            this.aToolStripMenuItem.Text = "a";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "desk1.png");
            this.imageList1.Images.SetKeyName(1, "desk2.png");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 591);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UserMenu;
        private System.Windows.Forms.ToolStripMenuItem VipMenu;
        private System.Windows.Forms.ToolStripMenuItem TaskMenu;
        private System.Windows.Forms.ToolStripMenuItem TeaMenu;
        private System.Windows.Forms.ToolStripMenuItem MoneyMenu;
        private System.Windows.Forms.ToolStripMenuItem QuitMenu;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;

    }
}