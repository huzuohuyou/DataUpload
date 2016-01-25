using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using ToolFunction;
using System.IO;
using JHEMR;
using JHEMR.EmrSysCom;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using MessagePlatform;
using System.Configuration;
using DataExport.文件接口;

namespace DataExport
{
    public partial class uctlDoExport : UserControl
    {

        //DataTable m_dtPats = null;

        public uctlDoExport()
        {
            InitializeComponent();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string _strExePath = Application.StartupPath + @"\MessagePlatform.exe";
            Process.Start(_strExePath);
            RemoteMessage.InitClient();
            string _strSQL = string.Empty;
            #region 获取病人discharge_date_time
            string _strUseAdapter = uctlBaseConfig.GetConfig("UseAdapterSQL");
            if ("TRUE"==_strUseAdapter)
            {
                _strSQL = uctlBaseConfig.GetConfig("AdapterSQL");
                if (!_strSQL.Contains("GUID"))
                {
                    MessageBox.Show("个性化SQL必须包含GUID!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("必须启用个性化SQL!");
                return;
            }
            PublicVar.m_dsPatients = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            //ExportDB.m_strTimeRange = "[时间]:" + dt_sta.Text + "至" + dt_end.Text;
            dataGridView1.DataSource = PublicVar.m_dsPatients;
            //RemoteMessage.SendMessage("==========================[启动文件下载线程]==========================");
            //EmrFile ef = new EmrFile(dt_sta.Text, dt_end.Text);
            //Thread t = new Thread(ef.PreDownLoadFile);
            //t.Start();
           
            #endregion

        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            //PublicProperty.m_dtSQL = GrabInfo.GetConfigSQL();
            //Thread t1 = new Thread(new ThreadStart(GrabInfo.GetPatientData));
            //t1.Start();
            Thread t1 = new Thread(new ThreadStart(GrabInfo.GrabPatientInfo));
            t1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(GrabInfo.GetPatientData));
            t1.Start();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
