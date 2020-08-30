using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
//using XKW_E.Tool;
//using Starts2000.WindowsClassName.Core;
namespace Beautify
{
    public partial class MyForm : Form
    {
        //#region 基本信息
        //[Category("ControlBox")]
        //[Description("设置标题栏图标的菜单")]
        //[DisplayName("CaptionMenu")]
        //public ContextMenuStrip menu { get; set; }
        //public Bitmap TitleImage;
        //public Bitmap CloseButtonImage;
        //public Bitmap CloseButtonPressDownImage;
        //public Bitmap CloseButtonHoverImage;
        //public Bitmap MaximumButtonImage;
        //public Bitmap MaximumButtonHoverImage;
        //public Bitmap MaximumButtonPressDownImage;
        //public Bitmap MaximumNormalButtonImage;
        //public Bitmap MaximumNormalButtonHoverImage;
        //public Bitmap MaximumNormalButtonPressDownImage;
        //public Bitmap MinimumButtonImage;
        //public Bitmap MinimumButtonHoverImage;
        //public Bitmap MinimumButtonPressDownImage;
        //public Bitmap HelpButtonImage;
        //public Bitmap HelpButtonHoverImage;
        //public Bitmap HelpButtonPressDownImage;
        //struct NonClientSizeInfo
        //{
        //    public Size CaptionButtonSize;
        //    public Size BorderSize;
        //    public int CaptionHeight;
        //    public Rectangle CaptionRect;
        //    public Rectangle Rect;
        //    public Rectangle ClientRect;
        //    public int Width;
        //    public int Height;
        //};

        //#endregion

        //#region 主要
        //public MyForm()
        //    : base()
        //{
        //    this.ControlBox = false;
        //    TitleImage = Properties.Resources.Main_Title;

        //    CloseButtonImage = Properties.Resources.Close_Normal;
        //    CloseButtonHoverImage = Properties.Resources.Close_On;
        //    CloseButtonPressDownImage = Properties.Resources.Close_Down;


        //    MaximumButtonImage = Properties.Resources.Max_Normal;
        //    MaximumButtonHoverImage = Properties.Resources.Max_On;
        //    MaximumButtonPressDownImage = Properties.Resources.Max_Down;


        //    MaximumNormalButtonImage = Properties.Resources.Restore_Normal;
        //    MaximumNormalButtonHoverImage = Properties.Resources.Restore_On;
        //    MaximumNormalButtonPressDownImage = Properties.Resources.Restore_Down;

        //    MinimumButtonImage = Properties.Resources.Min_Normal;
        //    MinimumButtonHoverImage = Properties.Resources.Min_On;
        //    MinimumButtonPressDownImage = Properties.Resources.Min_Down;
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case WindowsMessage.WM_NCPAINT: //绘制
        //            PaintBorder(m);
        //            return;
        //        case WindowsMessage.WM_NCACTIVATE: //标题栏激活
        //            if (m.WParam == (IntPtr)WindowsMessage.WM_FALSE)
        //            {
        //                m.Result = (IntPtr)WindowsMessage.WM_TRUE;
        //            }
        //            return;
        //        case WindowsMessage.WM_PAINT:
        //            if (this.WindowState == FormWindowState.Normal)
        //            {
        //                PaintBorder(m);
        //            }
        //            break;
        //        case WindowsMessage.WM_SIZE:
        //            PaintBorder(m);
        //            break;
        //        case WindowsMessage.WM_ACTIVATE: //窗体激活
        //            PaintBorder(m);
        //            break;
        //        case WindowsMessage.WM_NCCALCSIZE: //边框大小改变
        //            PaintBorder(m);
        //            break;
        //        case WindowsMessage.WM_EXITSIZEMOVE: //结束大小改变
        //            PaintBorder(m);
        //            break;
        //        case WindowsMessage.WM_SETCURSOR: //光标设置
        //            if (SetCursor(m)) return;
        //            break;
        //        case WindowsMessage.WM_NCRBUTTONDOWN:  //标题栏鼠标右键按下
        //            IconClick(m);
        //            break;
        //        case WindowsMessage.WM_NCLBUTTONUP://标题栏左键释放
        //            UpButton(m);
        //            break;
        //        case WindowsMessage.WM_NCMOUSEMOVE://标题栏鼠标移动
        //            MoveButton(m);
        //            break;
        //        case WindowsMessage.WM_NCLBUTTONDOWN://标题栏左键按下
        //            if (DownButton(m)) return;
        //            break;
        //    }
        //    base.WndProc(ref m);
        //}
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        return cp;
        //    }
        //}
        //public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        //{
        //    if (IntPtr.Size > 4)
        //        return Win32API.SetClassLongPtr64(hWnd, nIndex, dwNewLong);
        //    else
        //        return new IntPtr(Win32API.SetClassLongPtr32(hWnd, nIndex, unchecked((uint)dwNewLong.ToInt32())));
        //}
        //#endregion

        //#region 处理
        //private NonClientSizeInfo GetNonClientInfo(IntPtr hwnd)
        //{
        //    NonClientSizeInfo info = new NonClientSizeInfo();
        //    info.CaptionButtonSize = SystemInformation.CaptionButtonSize;
        //    info.CaptionHeight = SystemInformation.CaptionHeight;

        //    switch (this.FormBorderStyle)
        //    {
        //        case FormBorderStyle.Fixed3D:
        //            info.BorderSize = SystemInformation.FixedFrameBorderSize;
        //            break;
        //        case FormBorderStyle.FixedDialog:
        //            info.BorderSize = SystemInformation.FixedFrameBorderSize;
        //            break;
        //        case FormBorderStyle.FixedSingle:
        //            info.BorderSize = SystemInformation.FixedFrameBorderSize;
        //            break;
        //        case FormBorderStyle.FixedToolWindow:
        //            info.BorderSize = SystemInformation.FixedFrameBorderSize;
        //            info.CaptionButtonSize = SystemInformation.ToolWindowCaptionButtonSize;
        //            info.CaptionHeight = SystemInformation.ToolWindowCaptionHeight;
        //            break;
        //        case FormBorderStyle.Sizable:
        //            info.BorderSize = SystemInformation.FrameBorderSize;
        //            break;
        //        case FormBorderStyle.SizableToolWindow:
        //            info.CaptionButtonSize = SystemInformation.ToolWindowCaptionButtonSize;
        //            info.BorderSize = SystemInformation.FrameBorderSize;
        //            info.CaptionHeight = SystemInformation.ToolWindowCaptionHeight;
        //            break;
        //        default:
        //            info.BorderSize = SystemInformation.BorderSize;
        //            break;
        //    }
        //    RECT areatRect = new RECT();
        //    Win32API.GetWindowRect(hwnd, ref areatRect);

        //    int width = areatRect.right - areatRect.left;
        //    int height = areatRect.bottom - areatRect.top;

        //    info.Width = width;
        //    info.Height = height;

        //    Point xy = new Point(areatRect.left, areatRect.top);
        //    xy.Offset(-areatRect.left, -areatRect.top);

        //    info.CaptionRect = new Rectangle(xy.X, xy.Y + info.BorderSize.Height, width, info.CaptionHeight);
        //    info.Rect = new Rectangle(xy.X, xy.Y, width, height);
        //    info.ClientRect = new Rectangle(xy.X + info.BorderSize.Width,
        //        xy.Y + info.CaptionHeight + info.BorderSize.Height,
        //        width - info.BorderSize.Width * 2,
        //        height - info.CaptionHeight - info.BorderSize.Height * 2);
        //    return info;
        //}
        //#endregion

        //#region 绘制
        //bool painting = true;

        //private void PaintBorder(Message m)
        //{
        //    try
        //    {
        //        PAINTSTRUCT paint = new PAINTSTRUCT();
        //        paint.rcPaint = new Rectangle(0, 0, this.Width, this.Height);
        //        if (painting)
        //        {

        //            Win32API.BeginPaint(m.HWnd, ref paint);
        //            painting = false;
        //            NonClientSizeInfo info = GetNonClientInfo(m.HWnd);
        //            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
        //            IntPtr hDC = Win32API.GetWindowDC(m.HWnd);
        //            DrawLeft(hDC, context);
        //            DrawRight(hDC, context);
        //            DrawBottom(hDC, context);
        //            DrawTitle(hDC, context, info);
        //            Win32API.EndPaint(m.HWnd, ref paint);
        //            painting = true;
        //            Win32API.ReleaseDC(Handle, hDC);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}


        //#region 边框
        //Rectangle title;
        //Rectangle btnRect;
        //Rectangle maxRect;
        //Rectangle minRect;
        //Rectangle helpRect;
        //Rectangle iconRect;
        //RectangleF rectText;
        //private void DrawTitleInfo(Graphics g, NonClientSizeInfo info)
        //{
        //    int titleX;
        //    int iconW = 1;
        //    int iconH = 1;

        //    if (info.CaptionHeight < 20)
        //    {
        //        iconW = 15;
        //        iconH = 20;
        //    }
        //    else
        //    {
        //        iconW = 20;
        //        iconH = 25;
        //    }
        //    Size captionTitleSize = TextRenderer.MeasureText(this.Text, SystemFonts.CaptionFont);
        //    Size iconSize = new Size(iconW, iconH);

        //    if (this.WindowState == FormWindowState.Maximized)
        //    {
        //        if (this.ShowIcon &&
        //          this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //          this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //        {
        //            iconRect = new Rectangle(new Point(info.BorderSize.Width + 9, info.BorderSize.Height / 2), iconSize);
        //            g.DrawIcon(this.Icon, iconRect);
        //            titleX = info.BorderSize.Width + iconSize.Width + info.BorderSize.Width;
        //        }
        //        else
        //        {
        //            titleX = info.BorderSize.Width;
        //        }
        //        rectText = new RectangleF(titleX + 7,
        //               (info.BorderSize.Height + info.CaptionHeight - captionTitleSize.Height) / 2 + 2 + info.BorderSize.Height / 2,
        //               info.CaptionRect.Width - info.BorderSize.Width * 2 - SystemInformation.MinimumWindowSize.Width,
        //               info.CaptionRect.Height);
        //    }
        //    else
        //    {
        //        if (this.ShowIcon &&
        //            this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //            this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //        {
        //            iconRect = new Rectangle(new Point(info.BorderSize.Width + 9, info.BorderSize.Height / 2), iconSize);
        //            g.DrawIcon(this.Icon, iconRect);
        //            titleX = info.BorderSize.Width + iconSize.Width + info.BorderSize.Width;
        //        }
        //        else
        //        {
        //            titleX = info.BorderSize.Width;
        //        }
        //        rectText = new RectangleF(titleX + 7,
        //                (info.BorderSize.Height + info.CaptionHeight - captionTitleSize.Height) / 2 + 2,
        //                info.CaptionRect.Width - info.BorderSize.Width * 2 - SystemInformation.MinimumWindowSize.Width,
        //                info.CaptionRect.Height);
        //    }

        //    Rectangle titleText = new Rectangle(iconRect.Right, iconRect.Height, captionTitleSize.Width > 0 ? captionTitleSize.Width : 1, captionTitleSize.Height > 0 ? captionTitleSize.Height : 1);
        //    LinearGradientBrush brush = new LinearGradientBrush(titleText, Color.White, Color.Gold, LinearGradientMode.Vertical);
        //    SolidBrush brushBorder = new SolidBrush(Color.Gray);
        //    g.DrawString(this.Text, new Font(SystemFonts.CaptionFont, FontStyle.Bold), brushBorder, rectText, StringFormat.GenericTypographic);
        //    rectText.Offset(1, 1);
        //    g.DrawString(this.Text, new Font(SystemFonts.CaptionFont, FontStyle.Bold), brush, rectText, StringFormat.GenericTypographic);

        //}
        //private void DrawControlBox(Graphics g, NonClientSizeInfo info, bool closeBtn, bool maxBtn, bool minBtn, bool helpBtn)
        //{
        //    Size iconSize = SystemInformation.IconSize;

        //    int closeBtnPosX = info.CaptionRect.Width - info.BorderSize.Width - CloseButtonImage.Width;
        //    int maxBtnPosX = closeBtnPosX - MaximumButtonImage.Width;
        //    int minBtnPosX = maxBtnPosX - MinimumButtonImage.Width;
        //    // int helpBtnPosX = minBtnPosX - HelpButtonImage.Width;
        //    int btnPosY = 0;
        //    if (this.WindowState == FormWindowState.Maximized)
        //    {
        //        btnPosY = info.BorderSize.Height;
        //    }
        //    btnRect = new Rectangle(new Point(closeBtnPosX, btnPosY), CloseButtonImage.Size);
        //    maxRect = new Rectangle(new Point(maxBtnPosX, btnPosY), MaximumButtonImage.Size);
        //    minRect = new Rectangle(new Point(minBtnPosX, btnPosY), MinimumButtonImage.Size);
        //    //  helpRect = new Rectangle(new Point(helpBtnPosX, btnPosY), HelpButtonImage.Size);
        //    g.DrawImage(CloseButtonImage, btnRect);

        //    if (this.MaximizeBox || this.MinimizeBox)
        //    {
        //        if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //            this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //        {
        //            if (this.WindowState == FormWindowState.Maximized)
        //            {
        //                g.DrawImage(MaximumNormalButtonImage, maxRect);
        //            }
        //            else
        //            {
        //                g.DrawImage(MaximumButtonImage, maxRect);
        //            }
        //            g.DrawImage(MinimumButtonImage, minRect);
        //        }
        //    }
        //    //if (this.HelpButton)
        //    //{
        //    //    if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //    //        this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //    //    {
        //    //        g.DrawImage(HelpButtonImage, helpRect);
        //    //    }
        //    //}
        //}
        //private void DrawTitle(IntPtr hDC, BufferedGraphicsContext context, NonClientSizeInfo info)
        //{
        //    int leftAndRightWidth = (this.Width - this.ClientRectangle.Width) / 2;
        //    int height = this.Height - this.ClientRectangle.Height - leftAndRightWidth > 0 ? this.Height - this.ClientRectangle.Height - leftAndRightWidth : 1;
        //    int width = this.Width > 0 ? this.Width : 1;
        //    title = new Rectangle(0, 0, width, height);
        //    BufferedGraphics bg = context.Allocate(hDC, title);
        //    Graphics g = bg.Graphics;
        //    g.CompositingQuality = CompositingQuality.HighSpeed;
        //    g.DrawImage(TitleImage, title);
        //    DrawTitleInfo(g, info);
        //    DrawControlBox(g, info, this.ControlBox, this.MaximizeBox, this.MinimizeBox, this.HelpButton);
        //    bg.Render();
        //    bg.Dispose();
        //}
        //private void DrawLeft(IntPtr hDC, BufferedGraphicsContext context)
        //{
        //    int leftAndRightWidth = (this.Width - this.ClientRectangle.Width) / 2;
        //    int height = this.Height;
        //    int Width = (this.Width - this.ClientRectangle.Width) / 2;
        //    int y = this.Height - this.ClientRectangle.Height - leftAndRightWidth;
        //    Rectangle left = new Rectangle(0, y, Width, height);
        //    BufferedGraphics bg = context.Allocate(hDC, left);
        //    Graphics g = bg.Graphics;
        //    g.CompositingQuality = CompositingQuality.HighSpeed;
        //    g.DrawImage(Properties.Resources.Main_Left, left);
        //    bg.Render();
        //    bg.Dispose();
        //}
        //private void DrawRight(IntPtr hDC, BufferedGraphicsContext context)
        //{
        //    int leftAndRightWidth = (this.Width - this.ClientRectangle.Width) / 2;
        //    int height = this.Height;
        //    int Width = (this.Width - this.ClientRectangle.Width) / 2;
        //    int x = this.ClientRectangle.Width + (this.Width - this.ClientRectangle.Width) / 2;
        //    int y = this.Height - this.ClientRectangle.Height - leftAndRightWidth;
        //    Rectangle right = new Rectangle(x, y, Width, height);
        //    BufferedGraphics bg = context.Allocate(hDC, right);
        //    Graphics g = bg.Graphics;
        //    g.CompositingQuality = CompositingQuality.HighSpeed;
        //    g.DrawImage(Properties.Resources.Main_Right, right);
        //    bg.Render();
        //    bg.Dispose();
        //}
        //private void DrawBottom(IntPtr hDC, BufferedGraphicsContext context)
        //{
        //    int leftAndRightWidth = (this.Width - this.ClientRectangle.Width) / 2;
        //    int width = this.Width - 2;
        //    int height = leftAndRightWidth;
        //    int x = 1;
        //    int y = this.Height - leftAndRightWidth;
        //    Rectangle buttom = new Rectangle(x, y, width, height);
        //    BufferedGraphics bg = context.Allocate(hDC, buttom);
        //    Graphics g = bg.Graphics;
        //    g.CompositingQuality = CompositingQuality.HighSpeed;
        //    g.FillRectangle(new SolidBrush(Color.FromArgb(133, 199, 247)), buttom);
        //    //g.DrawImage(Properties.Resources.Main_Bottom, buttom);
        //    bg.Render();
        //    bg.Dispose();
        //}
        //#endregion

        //#region 按钮
        //Point pClick;
        //private void IconClick(Message m)
        //{
        //    pClick = new Point((int)m.LParam);
        //    pClick.Offset(-this.Left, -this.Top);
        //    int locationX = pClick.X + this.Location.X;
        //    int locationY = pClick.Y + this.Location.Y;
        //    if (iconRect.Contains(pClick))
        //    {
        //        if (menu != null)
        //        {
        //            menu.Show(locationX, locationY);
        //        }

        //    }
        //}
        //private void MoveButton(Message m)
        //{
        //    pClick = new Point((int)m.LParam);
        //    pClick.Offset(-this.Left, -this.Top);
        //    if (title.Contains(pClick))
        //    {
        //        IntPtr dc = Win32API.GetWindowDC(m.HWnd);
        //        Graphics g = Graphics.FromHdc(dc);

        //        #region 关闭按钮
        //        if (btnRect.Contains(pClick))
        //        {
        //            g.DrawImage(CloseButtonHoverImage, btnRect);
        //        }
        //        else
        //        {
        //            g.DrawImage(CloseButtonImage, btnRect);
        //        }
        //        #endregion

        //        #region 最大化最小化
        //        if (this.MaximizeBox || this.MinimizeBox)
        //        {
        //            if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //                this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //            {
        //                if (this.WindowState == FormWindowState.Maximized)
        //                {
        //                    if (this.MaximizeBox)
        //                    {
        //                        if (maxRect.Contains(pClick))
        //                        {
        //                            g.DrawImage(MaximumNormalButtonHoverImage, maxRect);
        //                        }
        //                        else
        //                        {
        //                            g.DrawImage(MaximumNormalButtonImage, maxRect);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        g.DrawImage(MaximumNormalButtonImage, maxRect);
        //                    }
        //                }
        //                else
        //                {
        //                    if (this.MaximizeBox)
        //                    {
        //                        if (maxRect.Contains(pClick))
        //                        {
        //                            g.DrawImage(MaximumButtonHoverImage, maxRect);
        //                        }
        //                        else
        //                        {
        //                            g.DrawImage(MaximumButtonImage, maxRect);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        g.DrawImage(MaximumButtonImage, maxRect);
        //                    }
        //                }

        //                if (this.MinimizeBox)
        //                {
        //                    if (minRect.Contains(pClick))
        //                    {
        //                        g.DrawImage(MinimumButtonHoverImage, minRect);
        //                    }
        //                    else
        //                    {
        //                        g.DrawImage(MinimumButtonImage, minRect);
        //                    }
        //                }
        //                else
        //                {
        //                    g.DrawImage(MinimumButtonImage, minRect);
        //                }
        //            }
        //        }
        //        #endregion

        //        #region 帮助
        //        //if (this.HelpButton)
        //        //{
        //        //    if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //        //        this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //        //    {
        //        //        if (helpRect.Contains(pClick))
        //        //        {
        //        //            g.DrawImage(HelpButtonHoverImage, helpRect);
        //        //        }
        //        //        else
        //        //        {
        //        //            g.DrawImage(HelpButtonImage, helpRect);
        //        //        }
        //        //    }
        //        //}
        //        #endregion;
        //        g.Dispose();
        //        Win32API.ReleaseDC(Handle, dc);
        //    }
        //}
        //private void UpButton(Message m)
        //{
        //    pClick = new Point((int)m.LParam);
        //    pClick.Offset(-this.Left, -this.Top);

        //    if (btnRect.Contains(pClick))
        //    {
        //        this.Close();
        //    }
        //    if (this.MaximizeBox)
        //        if (maxRect.Contains(pClick))
        //        {
        //            if (this.WindowState == FormWindowState.Maximized)
        //            {
        //                this.WindowState = FormWindowState.Normal;
        //            }
        //            else
        //            {
        //                this.WindowState = FormWindowState.Maximized;
        //            }
        //        }
        //    if (this.MinimizeBox)
        //        if (minRect.Contains(pClick))
        //        {
        //            if (this.WindowState == FormWindowState.Minimized)
        //            {
        //                this.WindowState = FormWindowState.Normal;
        //            }
        //            else
        //            {
        //                this.WindowState = FormWindowState.Minimized;
        //            }
        //        }
        //    if (this.HelpButton)
        //        if (helpRect.Contains(pClick))
        //        {
        //            if (this.WindowState == FormWindowState.Minimized)
        //            {
        //                this.WindowState = FormWindowState.Normal;
        //            }
        //            else
        //            {
        //                this.WindowState = FormWindowState.Minimized;
        //            }
        //        }
        //}
        //private bool DownButton(Message m)
        //{
        //    bool ret = false; //是否触发点击
        //    pClick = new Point((int)m.LParam);
        //    pClick.Offset(-this.Left, -this.Top);
        //    if (title.Contains(pClick))
        //    {
        //        IntPtr dc = Win32API.GetWindowDC(m.HWnd);
        //        Graphics g = Graphics.FromHdc(dc);
        //        #region 关闭按钮
        //        if (btnRect.Contains(pClick))
        //        {
        //            g.DrawImage(CloseButtonPressDownImage, btnRect);
        //            ret = true;
        //        }
        //        else
        //        {
        //            g.DrawImage(CloseButtonImage, btnRect);
        //        }
        //        #endregion

        //        #region 最大化最小化
        //        if (this.MaximizeBox || this.MinimizeBox)
        //        {
        //            if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow &&
        //                this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow)
        //            {
        //                if (this.WindowState == FormWindowState.Maximized)
        //                {
        //                    if (maxRect.Contains(pClick) && this.MaximizeBox)
        //                    {
        //                        g.DrawImage(MaximumNormalButtonPressDownImage, maxRect);
        //                        ret = true;
        //                    }
        //                    else
        //                    {
        //                        g.DrawImage(MaximumNormalButtonImage, maxRect);
        //                    }
        //                }
        //                else
        //                {
        //                    if (maxRect.Contains(pClick) && this.MaximizeBox)
        //                    {
        //                        g.DrawImage(MaximumButtonPressDownImage, maxRect);
        //                        ret = true;
        //                    }
        //                    else
        //                    {
        //                        g.DrawImage(MaximumButtonImage, maxRect);
        //                    }
        //                }
        //                if (minRect.Contains(pClick) && this.MinimizeBox)
        //                {
        //                    g.DrawImage(MinimumButtonPressDownImage, minRect);
        //                    ret = true;
        //                }
        //                else
        //                {
        //                    g.DrawImage(MinimumButtonImage, minRect);
        //                }
        //            }
        //        }
        //        #endregion

        //        #region 帮助
        //        if (this.HelpButton)
        //        {
        //            if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedToolWindow &&
        //                this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.SizableToolWindow)
        //            {
        //                if (helpRect.Contains(pClick))
        //                {
        //                    g.DrawImage(HelpButtonPressDownImage, helpRect);
        //                    ret = true;
        //                }
        //                else
        //                {
        //                    g.DrawImage(HelpButtonImage, helpRect);
        //                }
        //            }
        //        }
        //        #endregion;
        //        g.Dispose();
        //        Win32API.ReleaseDC(m.HWnd, dc);
        //    }
        //    return ret;
        //}
        //#endregion

        //#region  边缘设置

        //private bool SetCursor(Message m)
        //{
        //    bool flag = false;
        //    if (btnRect.Contains(pClick))
        //    {
        //        Win32API.SetCursor(Cursors.Default.Handle);
        //        flag = true;
        //    }
        //    if (this.MaximizeBox)
        //        if (maxRect.Contains(pClick))
        //        {
        //            Win32API.SetCursor(Cursors.Default.Handle);
        //            flag = true;
        //        }
        //    if (this.MinimizeBox)
        //        if (minRect.Contains(pClick))
        //        {
        //            Win32API.SetCursor(Cursors.Default.Handle);
        //            flag = true;
        //        }
        //    if (this.HelpButton)
        //        if (helpRect.Contains(pClick))
        //        {
        //            Win32API.SetCursor(Cursors.Default.Handle);
        //            flag = true;
        //        }
        //    if (menu != null)
        //    {
        //        if (iconRect.Contains(pClick))
        //        {
        //            Win32API.SetCursor(Cursors.Default.Handle);
        //            flag = true;
        //        }
        //    }
        //    return flag;
        //}
        //#endregion

        //#endregion
        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // MyForm
        //    // 
        //    this.ClientSize = new System.Drawing.Size(438, 395);
        //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        //    this.Name = "MyForm";
        //    this.ResumeLayout(false);

        //}
    }
}