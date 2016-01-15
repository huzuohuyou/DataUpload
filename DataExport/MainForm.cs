using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ConfirmFileName;
using JHEMR.EmrSysDAL;
using System.Configuration;
using ToolFunction;
using System.Diagnostics;
using DataExport.文件接口;
using System.Threading;


namespace DataExport
{
    public partial class MainForm : Form
    {
        Process m_proRemoteMessage = null;
        public MainForm()
        {
           
            InitializeComponent();
            SetBaseInfo();
            //try
            //{
            //    string s1 = ConfigurationManager.AppSettings["ClientConnectType"].ToString();
            //    DALUse.SetConnectString(ConfigurationManager.AppSettings["ClientConnectType"], ConfigurationManager.ConnectionStrings["SQLSERVER"].ToString());
            //    PublicProperty.DatabaseType = s1;
            //    //DALUse.Qecuery("seeclect * from pt_sql");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("连接失败..." + "\n" + ex.ToString());
            //}
           
        }

      




        private void 基础配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            uctlBaseConfig bc = new uctlBaseConfig();
            //CommonFunction.AddForm2(bc);
            CommonFunction.AddForm3(pl_showcontains,bc);
        }

        private void 平台管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uctlDBManagement ptm = new uctlDBManagement();
            //CommonFunction.AddForm2(ptm);
            CommonFunction.AddForm3(pl_showcontains,ptm);
        }

      
       

        private void 数据导出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            uctlDoExport de = new uctlDoExport();
            //CommonFunction.AddForm2(de);
            CommonFunction.AddForm3(pl_showcontains,de);
        }





        private void MainForm_Load(object sender, EventArgs e)
        {
            //臨時註釋123
            //EmrInfoManagement.InitStatus();
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strUploadFlag = config.AppSettings.Settings["UploadFlag"].Value;
            string _strUseAdapter = uctlBaseConfig.GetConfig("UseAdapterSQL");
            if ("TRUE" == _strUploadFlag.ToUpper() && "TRUE" == _strUseAdapter)
            {
                string _strExePath = Application.StartupPath + @"\MessagePlatform.exe";
                m_proRemoteMessage = Process.Start(_strExePath);
                RemoteMessage.InitClient();
                string _strSQL = uctlBaseConfig.GetConfig("AdapterSQL");
                PublicVar.m_dsPatients = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
                Thread t1 = new Thread(new ThreadStart(GrabInfo.GetPatientData));
                t1.Start();
            }
        }

       

        private void button1_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Green;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.WhiteSmoke;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uctlDBManagement ptm = new uctlDBManagement();
            CommonFunction.AddForm3(pl_showcontains, ptm);
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            uctlBaseConfig bc = new uctlBaseConfig(this);
            CommonFunction.AddForm3(pl_showcontains, bc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            uctlDoExport de = new uctlDoExport();
            CommonFunction.AddForm3(pl_showcontains, de);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_proRemoteMessage != null)
            {
                m_proRemoteMessage.Dispose();
            }
            
        }

        /// <summary>
        /// 设置窗体基本信息
        /// </summary>
        public void SetBaseInfo()
        {
            label1.Text = "导出方式:" + uctlBaseConfig.GetConfig("ExportType");
            this.Text = "数据导出V3.0  导出方式:" + uctlBaseConfig.GetConfig("ExportType");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            uctlDictManage de = new uctlDictManage();
            CommonFunction.AddForm3(pl_showcontains, de);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            uctlXmlStrManage de = new uctlXmlStrManage();
            CommonFunction.AddForm3(pl_showcontains, de);

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            uctlEXCELManagement ptm = new uctlEXCELManagement();
            CommonFunction.AddForm3(pl_showcontains, ptm);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            uctlRestoreManage de = new uctlRestoreManage();
            CommonFunction.AddForm3(pl_showcontains, de);
        }


    }
}