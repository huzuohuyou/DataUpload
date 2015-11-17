using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using ToolFunction;

namespace DataExport
{
    public class FluenctExport
    {
        string m_strObjectName = string.Empty;
        DataTable m_dtPats = new DataTable();

        public FluenctExport()
        {
        }

        public FluenctExport(string p_strObjectName,DataTable p_dtPats)
        {
            m_strObjectName = p_strObjectName;
            m_dtPats = p_dtPats;
        }

        public FluenctExport( DataTable p_dtPats)
        {
            m_dtPats = p_dtPats;
        }

       
        public void ExportOnePatInfo(string p_strObjectName,string p_strPatientId, string p_strVisitId)
        {
            
            DataSet _dsOnePatInfo = GrabInfo.GrabPatientInfo(p_strObjectName, p_strPatientId, p_strVisitId);
            _dsOnePatInfo = ConversionData.ExchangeData(_dsOnePatInfo);
            //_dsOnePatInfo.TableName = p_strObjectName;
            //DataSet _dsOnePatInfo = new DataSet();
            //_dsOnePatInfo.Tables.Add(_dsOnePatInfo);
            GrabInfo.ExeExport(_dsOnePatInfo, p_strObjectName, p_strPatientId, p_strVisitId);
        }


        //public void ExportOnePatInfoNoParam()
        //{
        //    string _strSQL = string.Format("select * from PT_TABLES_DICT where exportflag  = 'TRUE'");
        //    DataTable _dtObject = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
        //    foreach (DataRow var in _dtObject.Rows)
        //    {
        //        m_strObjectName = var["TABLE_NAME"].ToString();
        //        DataTable _dtOnePatInfo = GrabInfo.GrabPatientInfo(m_strObjectName, m_strPatinetId, m_strVisitId);
        //        _dtOnePatInfo = ConversionData.ExchangeData(_dtOnePatInfo);
        //        DataSet _dsOnePatInfo = new DataSet();
        //        _dsOnePatInfo.Tables.Add(_dtOnePatInfo);
        //        GrabInfo.ExeExport(_dsOnePatInfo);
        //    }
        //    //DataTable _dtOnePatInfo = GrabInfo.GrabPatientInfo(m_strObjectName, m_strPatinetId, m_strVisitId);
        //    //_dtOnePatInfo = ConversionData.ExchangeData(_dtOnePatInfo);
        //    //GrabInfo.ExeExport(_dtOnePatInfo);
        //}

        public void ExportPatsInfo()
        {
            string _strSQL = string.Format("select * from PT_TABLES_DICT where exportflag  = 'TRUE'");
            DataTable _dtObject = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            DataTable _dtPats = m_dtPats;
            foreach (DataRow _drOBject in _dtObject.Rows)
            {
                string _strObjectName = _drOBject["TABLE_NAME"].ToString();
                _dtPats.TableName = _strObjectName;
                foreach (DataRow var in _dtPats.Rows)
                {
                    string _strPatinetId = var["PATIENT_ID"].ToString();
                    string _strVisitId = var["VISIT_ID"].ToString();
                    ExportOnePatInfo(_strObjectName, _strPatinetId, _strVisitId);
                }
            }
        }


    }
}
