using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ToolFunction
{
    public partial class uctlMessageBox : UserControl
    {
        public uctlMessageBox( string mess)
        {
            InitializeComponent();
            disappeartime.Start();//时间空间开始运行
            lab_mess.Text = mess;
        }

        private void disappeartime_Tick(object sender, EventArgs e)
        {
                this.FindForm().Opacity = this.FindForm().Opacity - 0.5;//改变窗体透明度
                if (this.FindForm().Opacity == 0)//当窗体透明度为0时(看不到窗体了)
                {
                    this.FindForm().Close();//关闭窗体
                }
            
        }
        /// <summary>
        /// 只有选择Yes才进行信息显示
        /// </summary>
        /// <param name="dr">所选的项</param>
        /// <param name="message">消息内容</param>
        public static void frmDisappearShow(DialogResult dr,string message)
        {
            if (dr ==DialogResult.Yes)
            {
                Form f = new Form();
                f.Size = new Size(344, 113);
                f.FormBorderStyle = FormBorderStyle.None;
                uctlMessageBox umb = new uctlMessageBox(message);
                CommonFunction.AddForm(f, umb);
            }
           
        }
        /// <summary>
        /// 上一个方法的重载，所选择的项有程序判断，只提供消息内容设置。
        /// </summary>
        /// <param name="message"></param>
        public static void frmDisappearShow(string message)
        {
            Form f = new Form();
            f.Size = new Size(344, 113);
            f.FormBorderStyle = FormBorderStyle.None;
            uctlMessageBox umb = new uctlMessageBox(message);
            CommonFunction.AddForm(f, umb);
           
        }
    }
}
