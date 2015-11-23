using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using System.Threading;

namespace DataExport
{
    public partial class uctlRestoreManage : UserControl
    {
        public uctlRestoreManage()
        {
            InitializeComponent();
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
            FluenctExport fe = new FluenctExport(GetCurrentPatientId(),GetCurrentPatientVisitId());
            Thread t = new Thread(fe.ExportOnePatInfoForAllObj);
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable _dt = GetRestorePat();
            FluenctExport fe = new FluenctExport(_dt);
            Thread t = new Thread(fe.ExportPatsInfoForAllObj);
            t.Start();
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
        public static void LogFalsePatient(string p_strPatientId, string p_strVisitId)
        {
            string _strSQL = string.Format(@"INSTERT INTO PT_RESTORE(PATIENT_ID,VISIT_ID,LOG_DATE) VALUES('{0}','{1}','{2}')", p_strPatientId, p_strVisitId, DateTime.Now.ToString());
            int _n = CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
            if (1 == _n)
            {

            }
        }
    }
}
