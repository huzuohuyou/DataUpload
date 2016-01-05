using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using System.Threading;
using System.Diagnostics;

namespace DataExport
{
    public partial class uctlRestoreManage : UserControl
    {
        public uctlRestoreManage()
        {
            //string _strExePath = Application.StartupPath + @"\MessagePlatform.exe";
            //Process.Start(_strExePath);
            //RemoteMessage.InitClient();
            
            InitializeComponent();
            DataTable _dt= GetRestorePat();
            if (_dt == null) { CommonFunction.WriteError("无需恢复数据"); } else { dataGridView1.DataSource = _dt.DefaultView; }
            
        }

        public string GetCurrentObject()
        {
            return dataGridView1.CurrentRow.Cells["OBJECT_NAME"].Value.ToString();
        }

        public string GetCurrentPatientId()
        {
            return dataGridView1.CurrentRow.Cells["PATIENT_ID"].Value.ToString();
        }

        public string GetCurrentPatientVisitId()
        {
            return dataGridView1.CurrentRow.Cells["VISIT_ID"].Value.ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //DataTable _dt = GetRestorePat();
            FluenctExport fe = new FluenctExport();
            fe.ExportOnePatInfoForOneObj(GetCurrentObject(), GetCurrentPatientId(), GetCurrentPatientVisitId());
            //Thread t = new Thread(fe.ExportOnePatInfoForOneObj);
            //t.Start();
        }

        /// <summary>
        /// 恢复上传成功
        /// 移出记录
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        public static void RemoveRecord(string p_strObjectName, string p_strPatientId, string p_strVisitId)
        {
            string _strSQL = string.Format(@"delete from pt_restore where object_name = '{0}' and patient_id = '{1}' and visit_id = '{2}'", p_strObjectName, p_strPatientId, p_strVisitId);
            int _n = CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable _dt = GetRestorePat();
            foreach (DataRow var in _dt.Rows)
            {
                FluenctExport fe = new FluenctExport();
                fe.ExportOnePatInfoForOneObj(var["object_name"].ToString(), var["patient_id"].ToString(), var["visit_id"].ToString());
            }
        }

        public DataTable GetRestorePat()
        {
            string _strSQL = string.Format(@"SELECT * FROM PT_RESTORE ");
            DataTable _dt = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            return _dt;
        }

        /// <summary>
        /// 将导出失败病人的patient_id 和visit_id 记录下来
        /// </summary>
        public static void LogFalsePatient(string p_strObject,string p_strPatientId, string p_strVisitId)
        {
            string _strSQL = string.Format(@"INSERT INTO PT_RESTORE(PATIENT_ID,VISIT_ID,LOG_DATE,OBJECT_NAME) VALUES('{0}','{1}','{2}','{3}')", p_strPatientId, p_strVisitId, DateTime.Now.ToString(), p_strObject);
            int _n = CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
            if (1 == _n)
            {

            }
        }
    }
}
