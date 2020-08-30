namespace UDPClient
{
    partial class frmUdp
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
            this.tbxlocalip = new System.Windows.Forms.TextBox();
            this.lblocalIp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxlocalPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxSendtoIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSendtoport = new System.Windows.Forms.TextBox();
            this.chkbxAnonymous = new System.Windows.Forms.CheckBox();
            this.tbxMessageSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lstbxMessageView = new System.Windows.Forms.ListBox();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxlocalip
            // 
            this.tbxlocalip.Location = new System.Drawing.Point(63, 31);
            this.tbxlocalip.Name = "tbxlocalip";
            this.tbxlocalip.Size = new System.Drawing.Size(100, 20);
            this.tbxlocalip.TabIndex = 0;
            // 
            // lblocalIp
            // 
            this.lblocalIp.AutoSize = true;
            this.lblocalIp.Location = new System.Drawing.Point(13, 38);
            this.lblocalIp.Name = "lblocalIp";
            this.lblocalIp.Size = new System.Drawing.Size(44, 13);
            this.lblocalIp.TabIndex = 1;
            this.lblocalIp.Text = "本地IP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "：";
            // 
            // tbxlocalPort
            // 
            this.tbxlocalPort.Location = new System.Drawing.Point(189, 31);
            this.tbxlocalPort.Name = "tbxlocalPort";
            this.tbxlocalPort.Size = new System.Drawing.Size(78, 20);
            this.tbxlocalPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "发送到：";
            // 
            // tbxSendtoIp
            // 
            this.tbxSendtoIp.Location = new System.Drawing.Point(63, 76);
            this.tbxSendtoIp.Name = "tbxSendtoIp";
            this.tbxSendtoIp.Size = new System.Drawing.Size(100, 20);
            this.tbxSendtoIp.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "：";
            // 
            // tbxSendtoport
            // 
            this.tbxSendtoport.Location = new System.Drawing.Point(189, 79);
            this.tbxSendtoport.Name = "tbxSendtoport";
            this.tbxSendtoport.Size = new System.Drawing.Size(78, 20);
            this.tbxSendtoport.TabIndex = 7;
            // 
            // chkbxAnonymous
            // 
            this.chkbxAnonymous.AutoSize = true;
            this.chkbxAnonymous.Location = new System.Drawing.Point(16, 123);
            this.chkbxAnonymous.Name = "chkbxAnonymous";
            this.chkbxAnonymous.Size = new System.Drawing.Size(50, 17);
            this.chkbxAnonymous.TabIndex = 8;
            this.chkbxAnonymous.Text = "匿名";
            this.chkbxAnonymous.UseVisualStyleBackColor = true;
            // 
            // tbxMessageSend
            // 
            this.tbxMessageSend.Location = new System.Drawing.Point(63, 123);
            this.tbxMessageSend.Name = "tbxMessageSend";
            this.tbxMessageSend.Size = new System.Drawing.Size(143, 20);
            this.tbxMessageSend.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(212, 121);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(55, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lstbxMessageView
            // 
            this.lstbxMessageView.FormattingEnabled = true;
            this.lstbxMessageView.Location = new System.Drawing.Point(16, 194);
            this.lstbxMessageView.Name = "lstbxMessageView";
            this.lstbxMessageView.Size = new System.Drawing.Size(251, 95);
            this.lstbxMessageView.TabIndex = 11;
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(206, 165);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(61, 23);
            this.btnReceive.TabIndex = 12;
            this.btnReceive.Text = "接收";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 165);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(45, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(63, 165);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 23);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // frmUdp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 311);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.lstbxMessageView);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxMessageSend);
            this.Controls.Add(this.chkbxAnonymous);
            this.Controls.Add(this.tbxSendtoport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxSendtoIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxlocalPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblocalIp);
            this.Controls.Add(this.tbxlocalip);
            this.Name = "frmUdp";
            this.Text = "UDP编程";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxlocalip;
        private System.Windows.Forms.Label lblocalIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxlocalPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxSendtoIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSendtoport;
        private System.Windows.Forms.CheckBox chkbxAnonymous;
        private System.Windows.Forms.TextBox tbxMessageSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstbxMessageView;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnStop;
    }
}

