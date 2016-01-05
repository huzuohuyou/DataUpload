using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JHEMR.EmrSysDAL;
using System.Threading;
using ToolFunction;
using System.Configuration;

namespace DataExport
{
    class AutoUpload
    {
        private DataSet m_dsAllPatsInfo = null;
        private DataSet m_dsPtas = null;
        private DataSet m_dsSQL = null;
        private DataSet m_dsConfig = null;
        string m_strExportType = string.Empty;
        string m_strDbfPath = string.Empty;
        string m_strExcelPath = string.Empty;
        string m_strXmlPath = string.Empty;

        public void Upload()
        {
            GetPats();
            GetConfigSQL();
            CheckOutPutType();
            GetPatientData();
            ExeExport();
        }

        public void GetPats()
        {
            m_dsPtas = new DataSet();
            string sql = string.Format("select m.PATIENT_ID,m.VISIT_ID ,n.NAME from  pat_visit m ,pat_master_index n where {0} >'{1}' and {0}< '{2}' and m.patient_id = n.patient_id", "DISCHARGE_DATE_TIME", DateTime.Today.AddDays(-1), DateTime.Today);
            m_dsPtas = DALUse.Query(sql);
            ExportDB.m_strTimeRange = "[ʱ��]:" + DateTime.Today.AddDays(-1).ToString() + "��" + DateTime.Today;
            RemoteMessage.SendMessage("===���ز����б�\nSQL��" + sql + "\n===��" + m_dsPtas.Tables[0].Rows.Count.ToString() + "��");
        }


        /// <summary>
        /// ��ȡƽ̨���õ�sql
        /// </summary>
        /// <returns>����sql�б�</returns>
        public void GetConfigSQL()
        {
            string strSQL = string.Format("select * from pt_sql a,pt_tables_dict b where a.table_id = b.id and b.exportflag ='TRUE'   ");
            m_dsSQL = DALUse.Query(strSQL);
            RemoteMessage.SendMessage("===�����������\nSQL��" + strSQL);
        }


        /// <summary>
        /// ��ȡ��sql��ѯ���Ĳ�����Ϣ
        /// </summary>
        /// <param name="dql">sql��</param>
        /// <returns>������Ϣ</returns>
        public void GetPatientData()
        {
            m_dsAllPatsInfo = new DataSet();
            foreach (DataRow dr in m_dsSQL.Tables[0].Rows)
            {
                DataTable _dtPatsInfo = new DataTable();
                foreach (DataRow drpat in m_dsPtas.Tables[0].Rows)
                {
                    string _strPatientId = drpat["PATIENT_ID"].ToString();
                    string _strVisitId = drpat["VISIT_ID"].ToString();
                    string _strSql = string.Format(dr["sql"].ToString().Replace("@PATIENT_ID", _strPatientId).Replace("@VISIT_ID", _strVisitId));
                    _dtPatsInfo.Merge(DALUse.Query(_strSql).Tables[0]);
                    RemoteMessage.SendMessage("===��ѯ������Ϣ\nSQL��" + _strSql);
                }
                DataTable _dt = _dtPatsInfo.Copy();
                _dt.TableName = "@" + dr["SQL_NAME"].ToString() + "@" + dr["table_id"].ToString();
                m_dsAllPatsInfo.Tables.Add(_dt);
            }
        }

        /// <summary>
        /// ִ�е���
        /// </summary>
        public void ExeExport()
        {

            string _strOutPutType = string.Empty, _strExceltPath = string.Empty, _strDbfPath = string.Empty;
            _strOutPutType = m_strExportType;
            _strExceltPath = m_strExcelPath;
            _strDbfPath = m_strDbfPath;
            IExport ie = null;
            switch (_strOutPutType)
            {
                case PublicVar.CExportDB:
                    ie = new ExportDB();
                    PublicVar.ExportParam[0] = m_dsAllPatsInfo;                    
                    break;
                case PublicVar.CExportDBF:
                    ie = new ExportDBF(m_dsAllPatsInfo.Tables[0]);
                    break;
                case PublicVar.CExportExcel:
                    ie = new ExportExcel();
                    PublicVar.ExcelPath = _strExceltPath;
                    //PublicProperty.ExcelSource = m_dsAllPatsInfo;
                    break;
                default:
                    break;
            }
            ie.Export();
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>��������</returns>
        public void CheckOutPutType()
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            m_strExportType = config.AppSettings.Settings["ExportType"].Value;
            m_strDbfPath = config.AppSettings.Settings["DbfPath"].Value;
            m_strExcelPath = config.AppSettings.Settings["ExcelPath"].Value;
            m_strXmlPath = config.AppSettings.Settings["XmlPath"].Value;
            RemoteMessage.SendMessage(
                    "===����ϵͳ������Ϣ\n===��������:" + m_strExportType
                + "\n===ExcelPath:" + m_strXmlPath
                + "\n===DbfPath:" + m_strDbfPath
                + "\n===XmlPath:" + m_strXmlPath);
        }


    }
}
