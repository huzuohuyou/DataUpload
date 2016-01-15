using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using ToolFunction;

namespace DataExport
{
    public class FluenctExport:IExport
    {
        string m_strObjectName = string.Empty;
        DataTable m_dtPats = new DataTable();
        string m_strPatientId = string.Empty;
        string m_strVisitId = string.Empty;

        public FluenctExport()
        {
        }

        /// <summary>
        /// 导出病人集 的所有可用对象
        /// </summary>
        /// <param name="p_dtPats"></param>
        public FluenctExport(DataTable p_dtPats)
        {
            m_dtPats = p_dtPats;
        } 

        /// <summary>
        /// 导出病人数据集的指定对象
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <param name="p_dtPats"></param>
        public FluenctExport(string p_strObjectName,DataTable p_dtPats)
        {
            m_strObjectName = p_strObjectName;
            m_dtPats = p_dtPats;
        }

        /// <summary>
        /// 导出指定病人的可用对象
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        public FluenctExport(string p_strPatientId, string p_strVisitId)
        {
            m_strPatientId = p_strPatientId;
            m_strVisitId = p_strVisitId;
        }

        /// <summary>
        /// 导出指定病人的指定对象
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        public FluenctExport(string p_strObjectName, string p_strPatientId, string p_strVisitId)
        {
            m_strObjectName = p_strObjectName;
            m_strPatientId = p_strPatientId;
            m_strVisitId = p_strVisitId;
        }

        

       

       /// <summary>
       /// 导出指定病人的指定对象
       /// </summary>
       /// <param name="p_strObjectName"></param>
       /// <param name="p_strPatientId"></param>
       /// <param name="p_strVisitId"></param>
        public void ExportOnePatInfoForOneObj(string p_strObjectName,string p_strPatientId, string p_strVisitId)
        {
            GrabInfo.InitPatDBInfo(p_strPatientId, p_strVisitId);
            DataSet _dsOnePatInfo = GrabInfo.GrabPatientInfo(p_strObjectName, p_strPatientId, p_strVisitId);
            DataTable _dt   = ConversionData.ExchangeData(_dsOnePatInfo.Tables[0]);
            GrabInfo.ExeExport(_dt.DataSet, p_strObjectName, p_strPatientId, p_strVisitId);
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

        /// <summary>
        /// 导出病人集 的所有可用对象
        /// </summary>
        public void ExportPatsInfoForAllObj()
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
                    PublicVar.m_strCurrentPatientId = _strPatinetId;
                    PublicVar.m_strCurrentVisitId = _strVisitId;
                    ExportOnePatInfoForOneObj(_strObjectName, _strPatinetId, _strVisitId);
                }
            }
        }

        /// <summary>
        /// 导出制定病人的所有对象
        /// </summary>
        public void ExportOnePatInfoForAllObj()
        {
            string _strSQL = string.Format("select * from PT_TABLES_DICT where exportflag  = 'TRUE'");
            DataTable _dtObject = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            DataTable _dtPats = m_dtPats;
            foreach (DataRow _drOBject in _dtObject.Rows)
            {
                string _strObjectName = _drOBject["TABLE_NAME"].ToString();
                _dtPats.TableName = _strObjectName;
                string _strPatinetId = m_strPatientId;
                string _strVisitId = m_strVisitId;
                ExportOnePatInfoForOneObj(_strObjectName, _strPatinetId, _strVisitId);
            }
        }



        #region IExport 成员

        public void Export()
        {
            ExportPatsInfoForAllObj();
        }

        #endregion

        #region IExport 成员


        public void LogFalse(List<string> p_list)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IExport 成员


        public string SynSQL(string p_strObjName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
