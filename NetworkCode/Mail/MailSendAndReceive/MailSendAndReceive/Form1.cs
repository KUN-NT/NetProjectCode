using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// 添加命名空间
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.IO;
using jmail;

namespace MailSendAndReceive
{
    public partial class mainfrm : Form
    {
        // 定义邮件发送类
        private SmtpClient smtpClient;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;

        // 下面是Dimac Jmail组件中的对象
        // 用来接收邮件
        // 定义 POP3Class这个类时需要设置下Jmail程序集的“嵌入互操作类型”属性为flase
        // 因为Jmail 是一个COM组件， 这样就存在.net平台下的代码（托管代码）与非托管代码的交互问题，也就是互操作性问题的
        // Visual Studio 默认的方式把加入的COM组件的“嵌入互操作类型”属性设置为true
        // 设置为true的含义就是编译过程中不引入互操作程序集， 只在托管代码中引入互操作类型，以至于编译过程中找不到该类的类型
        // 因为POP3Class类在引入的.Net类库中没有定义，它只在互操作程序集Jmail中定义的，所以要通过编译必须把“嵌入互操作类型”设置为false
        // 设置为false的含义就是引入互操作程序集到项目中，这样POP3Class类就可以到Jamil找到相应的类型了， 微软默认把“嵌入互操作类型”设置为true,
        // 也是有它的原因的——原因是微软希望减轻将COM 互操作程序集与您的应用程序一起部署的负担。
        // 有了以上的分析，大家应该可以明白“为什么”把这个属性设置为false后就不报错了
        // 如果大家还有什么疑问的话，可以看看CLR via C#的第一章来了解CLR的执行模型，同时也欢迎大家留言和我讨论
        
        // 定义接收邮件对象
        private POP3Class popClient;
        
        // 定义邮件信息接口
        private jmail.Message messageMail;
        
        // 定义邮件附件集合接口
        private Attachments attachments;

        // 定义邮件附件接口
        private jmail.Attachment attachment;
        public mainfrm()
        {
            InitializeComponent();

            // 初始化界面
            tbxUserMail.Text = "mytest1989@sina.cn";
            
            tbxSmtpServer.Text = "smtp.sina.com";
            tbxPOP3Server.Text = "pop.sina.com";
            // 这里收件人地址是Sina邮箱，你可以根据自己情况选择发送到自己的邮箱中
            txbSendTo.Text = "794170314@qq.com";
            txbSubject.Text = "测试邮件";
            richtbxBody.Text = "这是一封测试邮件，由系统自动发出，无须回复";

            // 界面控件控制
            btnLogout.Enabled = false;
            richtbxMailContentReview.Enabled = false;
            btnReplyCurrentMail.Enabled = false;
            btnDelCurrentMail.Enabled = false;
            tabControlMyMailbox.Enabled = false;
        }

        #region 邮件发送功能代码
        // 添加附件
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            // 只接受有效的文件名
            openFileDialog.ValidateNames = true;
            // 允许一次选择多个文件作为附件
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (openFileDialog.FileNames.Length > 0)
            {
                // 因为这里允许选择多个文件，所以这里用AddRange而没有用Add方法
                cmbAttachment.Items.AddRange(openFileDialog.FileNames);
            }
        }

        // 删除附件
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int index = cmbAttachment.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("请选择要删除的附件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                cmbAttachment.Items.RemoveAt(index);
            }
        }

        // 发送邮件
        private void btnSend_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            // 实例化一个发送的邮件
            // 相当于与现实生活中先写信，程序中把信（邮件）抽象为邮件类了
            MailMessage mailMessage = new MailMessage();
            // 指明邮件发送的地址，主题，内容等信息
            // 发信人的地址为登录收发器的地址，这个收发器相当于我们平时Web版的邮箱或者是OutLook中配置的邮箱
            mailMessage.From = new MailAddress(tbxUserMail.Text);
            mailMessage.To.Add(txbSendTo.Text);
            mailMessage.Subject = txbSubject.Text;
            mailMessage.SubjectEncoding = Encoding.Default;
            mailMessage.Body = richtbxBody.Text;
            mailMessage.BodyEncoding = Encoding.Default;
            // 设置邮件正文不是Html格式的内容
            mailMessage.IsBodyHtml = false;
            // 设置邮件的优先级为普通优先级
            mailMessage.Priority = MailPriority.Normal;
            //mailMessage.ReplyTo = new MailAddress(tbxUserMail.Text);

            // 封装发送的附件
            System.Net.Mail.Attachment attachment = null;
            if (cmbAttachment.Items.Count > 0)
            {
                for (int i = 0; i < cmbAttachment.Items.Count; i++)
                {
                    string fileNamePath = cmbAttachment.Items[i].ToString();
                    string extName = Path.GetExtension(fileNamePath).ToLower();
                    if (extName == ".rar" || extName == ".zip")
                    {
                        attachment = new System.Net.Mail.Attachment(fileNamePath, MediaTypeNames.Application.Zip);
                    }
                    else
                    {
                        attachment = new System.Net.Mail.Attachment(fileNamePath,MediaTypeNames.Application.Octet);
                    }

                    // 表示MIMEContent-Disposition标头信息
                    // 对于ContentDisposition具体类的解释大家可以参考MSDN
                    // 这里我就不重复贴出来了，给个地址: http://msdn.microsoft.com/zh-cn/library/System.Net.Mime.ContentDisposition.aspx (着重看备注部分)
                    ContentDisposition cd = attachment.ContentDisposition;
                    cd.CreationDate = File.GetCreationTime(fileNamePath);
                    cd.ModificationDate = File.GetLastWriteTime(fileNamePath);
                    cd.ReadDate = File.GetLastAccessTime(fileNamePath);
                    // 把附件对象加入到邮件附件集合中
                    mailMessage.Attachments.Add(attachment);
                }
            }

            // 发送写好的邮件
            try
            {
                // SmtpClient类用于将邮件发送到SMTP服务器
                // 该类封装了SMTP协议的实现，
                // 通过该类可以简化发送邮件的过程，只需要调用该类的Send方法就可以发送邮件到SMTP服务器了。
                smtpClient.Send(mailMessage);
                MessageBox.Show("邮件发送成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(SmtpException smtpError)
            {
                MessageBox.Show("邮件发送失败：[" + smtpError.StatusCode + "]；[" 
                    + smtpError.Message+"];\r\n["+smtpError.StackTrace+"]."
                    ,"错误",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
            finally
            {
                mailMessage.Dispose();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        // 登录邮箱（这里是本程序——邮件收发器）
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            // 与POP3服务器建立TCP连接
            // 建立连接后把服务器上的邮件下载到本地
            // 设置当前界面的光标为等待光标（就是我们看到的一个动的圆形）
            Cursor.Current = Cursors.WaitCursor;
            lsttbxStatus.Items.Clear();
            try
            {
                // POP3服务器通过监听TCP110端口来提供POP3服务的
                // 向POP3服务器发出tcp请求
                tcpClient = new TcpClient(tbxPOP3Server.Text, 110);
                lsttbxStatus.Items.Add("正在连接...");
            }
            catch
            {
                MessageBox.Show("连接失败", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                lsttbxStatus.Items.Add("连接失败！");
                return;
            }

            // 连接成功的情况
            networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream, Encoding.Default);
            streamWriter = new StreamWriter(networkStream, Encoding.Default);
            streamWriter.AutoFlush = true;
            string str;
            // 读取服务器返回的响应连接信息
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                lsttbxStatus.Items.Add("服务器拒接了连接请求");
                return;
            }
            // 如果服务器接收请求
            // 向服务器发送凭证——用户名和密码

            // 向服务器发送用户名，请求确认
            lsttbxStatus.Items.Add("核实用户名阶段...");
            SendToServer("USER " + tbxUserMail.Text);
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                lsttbxStatus.Items.Add("用户名错误.");
                return;
            }

            // 用户名审核通过后再发送密码等待确认
            // 向服务器发送密码，请求确认
            SendToServer("PASS "+txbPassword.Text);
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                lsttbxStatus.Items.Add("密码错误！");
                return;
            }

            lsttbxStatus.Items.Add("身份验证成功，可以开始会话");
            // 向服务器发送LIST 命令，请求获得邮件列表和大小
            lsttbxStatus.Items.Add("获取邮件....");
            SendToServer("LIST");
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                lsttbxStatus.Items.Add("获取邮件列表失败");
                return;
            }

            lsttbxStatus.Items.Add("邮件获取成功");

            // 窗口控件控制
            tabControlMyMailbox.Enabled = true;
            btnReadMail.Enabled = false;
            btnDownLoad.Enabled = false;
            btnDeleteMail.Enabled = false;

            // 登陆成功后实例化邮件发送对象，以便后面完成发送邮件的操作
            // 实例化邮件发送类（SmtpClient）对象
            if (smtpClient == null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Host = tbxSmtpServer.Text;
                smtpClient.Port = 25;
                    
                // 不使用默认凭证，即需要认证登陆
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(tbxUserMail.Text, txbPassword.Text);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            }

            // 登陆成功后，自动接收新邮件
            // 开始接收邮件
            try
            {
                btnRefreshMailList.PerformClick();
            }
            catch
            {
                MessageBox.Show("读取邮件列表失败！", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            lsttbxStatus.Items.Add("登陆成功!");
            lsttbxStatus.TopIndex = lsttbxStatus.Items.Count - 1;
            Cursor.Current = Cursors.Default;

            // 窗口控件控制
            richtbxMailContentReview.Enabled = true;
            tbxUserMail.Enabled = false;
            txbPassword.Enabled = false;
            btnLogin.Enabled = false;
            btnLogout.Enabled = true;
            tbxSmtpServer.Enabled = false;
            tbxPOP3Server.Enabled = false;
            btnReadMail.Enabled = true;
            btnDownLoad.Enabled = true;
            btnDeleteMail.Enabled = true;
            tabControlMyMailbox.Focus();
        }

        #region 处理与POP3服务器交互事件
        // 获取服务器响应的信息
        private string GetResponse()
        {
            string str = null;
            try
            {
                str = streamReader.ReadLine();
                if (str == null)
                {
                    lsttbxStatus.Items.Add("连接失败——服务器没有响应");
                }
                else
                {
                    lsttbxStatus.Items.Add("收到：[" + str + "]");
                    if (str.StartsWith("-ERR"))
                    {
                        str = null;
                    }
                }
            }
            catch(Exception err)
            {
                lsttbxStatus.Items.Add("连接失败：[" + err.Message + "]");
            }

            return str;
        }

        // 检查响应信息
        private bool CheckResponse(string responseString)
        {
            if (responseString == null)
            {
                return false;
            }
            else
            {
                if (responseString.StartsWith("+OK"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // 向服务器发送命令
        private bool SendToServer(string str)
        {
            try
            {
                // 这里必须使用WriteLine方法的，因为POP3协议中定义的命令是以回车换行结束的
                // 如果客户端发送的命令没有以回车换行结束，POP3服务器就不能识别，也就不能响应客户端的请求了
                // 如果想用Write方法，则str输入的参数字符中必须包含“\r\n”,也就是回车换行字符串。
                streamWriter.WriteLine(str);
                streamWriter.Flush();
                lsttbxStatus.Items.Add("发送：[" + str + "]");
                return true;
            }
            catch(Exception ex)
            {
                lsttbxStatus.Items.Add("发送失败：[" + ex.Message + "]");
                return false;
            }
        }

        #endregion

        // 退出登陆
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // 断开与POP3服务器的TCP连接
            lsttbxStatus.Items.Add("结束会话，进入更新状态...");
            SendToServer("QUIT");
            lsttbxStatus.Items.Add("正在关闭连接...");
            streamReader.Close();
            streamWriter.Close();
            networkStream.Close();
            tcpClient.Close();

            // SmtpClient 对象销毁
            if (smtpClient != null)
            {
                smtpClient.Dispose();
            }

            // POP3Class 对象销毁
            // Dimac官网上下载的免费版的Jmail组件
            // 在调用Connect方法和Disconnect出现说这个方法没在该组件中定义的错误
            // 后面通过网上的答案说下载破解版的Jmail 4.4 Pro后解决了这个问题的。
            popClient.Disconnect();
            lstViewMailList.Items.Clear();
            lsttbxStatus.Items.Add("退出登陆.");
            lsttbxStatus.TopIndex = lsttbxStatus.Items.Count - 1;
            tbxMailboxInfo.Text = "";

            // 窗口控件控制
            tabControlMyMailbox.Enabled = false;
            richtbxMailContentReview.Text = "";
            richtbxMailContentReview.Enabled = false;
            btnReplyCurrentMail.Enabled = false;
            btnDelCurrentMail.Enabled = false;
            tbxUserMail.Enabled = true;
            txbPassword.Enabled = true;
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            tbxSmtpServer.Enabled = true;
            tbxPOP3Server.Enabled = true;
        }

        #region 邮件操作
        // 阅读邮件内容
        // 这里也可以实现ListView Click事件来阅读邮件的
        private void btnReadMail_Click(object sender, EventArgs e)
        {
            // 登陆成功后 popClient的连接就一直保持着，因此无须重复连接
            if (lstViewMailList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择阅读的邮件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            int index = lstViewMailList.SelectedItems[0].Index;
            messageMail = popClient.Messages[index + 1];
            richtbxMailContentReview.Text = messageMail.Body;
            btnReplyCurrentMail.Enabled = true;
            btnDelCurrentMail.Enabled = true;
            lstViewMailList.Focus();
        }
        
        // 回复邮件
        private void btnReplyCurrentMail_Click(object sender, EventArgs e)
        {
            int index = lstViewMailList.SelectedItems[0].Index;
            messageMail = popClient.Messages[index + 1];

            // 使写信选项卡成为当前选项卡
            tabControlMyMailbox.SelectTab(tabPageWriteLetter);
            txbSendTo.Text = lstViewMailList.SelectedItems[0].SubItems[1].Text;
            txbSubject.Text = "Re:"+messageMail.Subject;
            richtbxBody.Text = "";
            richtbxBody.Focus();
        }

       
        // 取消发送
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txbSendTo.Text = "mytest1989@sina.cn";
            txbSubject.Text = "测试邮件";
            richtbxBody.Text = "这是一封测试邮件，由系统自动发送，无须回复。";
            if (cmbAttachment.Items.Count > 0)
            {
                cmbAttachment.Items.Clear();
            }

            // 使收件箱选项卡成为当前选项卡
            tabControlMyMailbox.SelectTab(tabPageInbox);
        }

        // 下载附件
        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (lstViewMailList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择阅读的邮件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (lstViewMailList.SelectedItems[0].SubItems[3].Text == "无")
            {
                MessageBox.Show("该邮件没有附件","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            int index = lstViewMailList.SelectedItems[0].Index;
            messageMail = popClient.Messages[index + 1];
            attachments = messageMail.Attachments;
            for (int i = 0; i < attachments.Count; i++)
            {
                attachment = attachments[i];
                string attachName = attachment.Name;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = attachName;
                saveFileDialog.Filter = "所有文件(*.*)|(*.*)";
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    continue;
                }

                string filepath = saveFileDialog.FileName;
                attachment.SaveToFile(filepath);
                MessageBox.Show("以保存：\r\n" + attachment.Name, "下载完毕", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 删除邮件
        private void btnDeleteMail_Click(object sender, EventArgs e)
        {
            if (lstViewMailList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择阅读的邮件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = lstViewMailList.SelectedItems[0].Index;
            messageMail = popClient.Messages[index + 1];
            if (MessageBox.Show("确认要删除邮件" + messageMail.Subject + "吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                popClient.DeleteSingleMessage(index + 1);
                popClient.Disconnect();
                btnRefreshMailList.PerformClick();
            }
        }

        // 刷新邮件列表
        private void btnRefreshMailList_Click(object sender, EventArgs e)
        {
            // 实例化邮件接收类POP3Class
            popClient = new POP3Class();
            // 连接服务器
            popClient.Connect(tbxUserMail.Text, txbPassword.Text, tbxPOP3Server.Text, 110);
            if (popClient != null)
            {
                if (popClient.Count > 0)
                {
                    lstViewMailList.Items.Clear();
                    tbxMailboxInfo.Text = "共" + popClient.Count + "封邮件";
                    for (int i = 0; i < popClient.Count; i++)
                    {
                        messageMail = popClient.Messages[i + 1];
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Add(messageMail.From);
                        item.SubItems.Add(messageMail.Subject);
                        attachments = messageMail.Attachments;
                        if (attachments.Count > 0)
                        {
                            item.SubItems.Add(attachments.Count.ToString());
                        }
                        else
                        {
                            item.SubItems.Add("无");
                        }

                        item.SubItems.Add(messageMail.Date.ToString());
                        lstViewMailList.Items.Add(item);
                    }
                }
            }
        }

        // 删除当前预览邮件
        private void btnDelCurrentMail_Click(object sender, EventArgs e)
        {
            int index = lstViewMailList.SelectedItems[0].Index;
            messageMail = popClient.Messages[index + 1];
            if (MessageBox.Show("确认要删除邮件" + messageMail.Subject + "吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                popClient.DeleteSingleMessage(index + 1);
                tbxMailboxInfo.Text = "删除了主题为“"+messageMail.Subject+"”的邮件";
                popClient.Disconnect();
                richtbxMailContentReview.Text = "";
                btnReplyCurrentMail.Enabled = false;
                btnDelCurrentMail.Enabled = false;
            }
        }
        #endregion
       
    }
}
