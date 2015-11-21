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
using DataExport.�ļ��ӿ�;


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
            //    MessageBox.Show("����ʧ��..." + "\n" + ex.ToString());
            //}
           
        }

      




        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            uctlBaseConfig bc = new uctlBaseConfig();
            //CommonFunction.AddForm2(bc);
            CommonFunction.AddForm3(pl_showcontains,bc);
        }

        private void ƽ̨����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uctlPTManage ptm = new uctlPTManage();
            //CommonFunction.AddForm2(ptm);
            CommonFunction.AddForm3(pl_showcontains,ptm);
        }

      
       

        private void ���ݵ���ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            uctlDoExport de = new uctlDoExport();
            //CommonFunction.AddForm2(de);
            CommonFunction.AddForm3(pl_showcontains,de);
        }





        private void MainForm_Load(object sender, EventArgs e)
        {
            EmrInfoManagement.InitStatus();
            //EmrInfo.SetUrl();
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strUploadFlag = config.AppSettings.Settings["UploadFlag"].Value;
            if ("TRUE" == _strUploadFlag.ToUpper())
            {
                string _strExePath = Application.StartupPath + @"\MessagePlatform.exe";
                m_proRemoteMessage = Process.Start(_strExePath);
                RemoteMessage.InitClient();
                AutoUpload au = new AutoUpload();
                au.Upload();
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
            uctlPTManage ptm = new uctlPTManage();
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
        /// ���ô��������Ϣ
        /// </summary>
        public void SetBaseInfo()
        {
            label1.Text = "������ʽ:" + uctlBaseConfig.GetConfig("ExportType");
            this.Text = "���ݵ���V3.0  ������ʽ:" + uctlBaseConfig.GetConfig("ExportType");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            uctlConfigDict de = new uctlConfigDict();
            CommonFunction.AddForm3(pl_showcontains, de);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            uctlXmlStrManage de = new uctlXmlStrManage();
            CommonFunction.AddForm3(pl_showcontains, de);

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            uctlXmlDocManage de = new uctlXmlDocManage();
            CommonFunction.AddForm3(pl_showcontains,de);
        }

      

       
       

    }
}