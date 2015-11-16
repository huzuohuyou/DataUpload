using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
namespace ToolFunction
{
    public partial class uctlPleaseWaiting : UserControl
    {
        public static string s = "";
        public uctlPleaseWaiting()
        {
            InitializeComponent();
        }
        public void setMess(string mess)
        {
            this.lab_mess.Text = mess;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lab_mess.Text ="正在插入第" +clsProperty.insertcount.ToString()+"条数据";
        }
    }
}
