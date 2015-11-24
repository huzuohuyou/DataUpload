using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using ToolFunction;
using System.Threading;
using JHEMR.EmrSysAdaper;
using System.Windows.Forms;

namespace DataExport.文件接口
{
    public class EmrFile
    {

        public static string m_strPatientId = string.Empty;
        public static string m_strFileName = string.Empty;
        //public static DataTable m_dtPats = new DataTable();
        public static string m_strStart = string.Empty;
        public static string m_strEnd = string.Empty;
        public static int m_nCount = 0;

        //public EmrFile(DataTable p_dtPats)
        //{
        //    foreach (DataRow var in p_dtPats.Rows)
        //    {
        //        string _strSQL = string.Format(@"select patient_id,visit_id,file_name from mr_file_index where patient_id = '{0}' and visit_id = '{1}'", var["PATIENT_ID"].ToString(), var["VISIT_ID"].ToString());
        //        m_dtPats.Merge(CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR").Copy());
        //    }
        //}

        public EmrFile(string p_strStart, string p_strEnd)
        {
            m_strStart = p_strStart;
            m_strEnd = p_strEnd;
        
        }

        int _nTCount = 0;
        int _nFCount = 0;
        /// <summary>
        /// 下载数据集中病人的所有文件
        /// </summary>
        /// <param name="p_dsPats"></param>
        public void PreDownLoadFile()
        {
            string _strTimeKind = uctlBaseConfig.GetConfig("TimeKind");
            string _strSQL = string.Format(@"select n.patient_id,n.visit_id,n.file_name from pat_visit m,mr_file_index n where m.patient_id = n.patient_id and m.visit_id = n.visit_id and  m.{2} >TO_DATE('{0}','yyyy-MM-dd') and m.{2}< TO_DATE('{1}','yyyy-MM-dd')", m_strStart, m_strEnd, _strTimeKind);
            DataTable m_dtPats = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            foreach (DataRow var in m_dtPats.Rows)
            {
                m_strPatientId = var["PATIENT_ID"].ToString();
                string _strVisitId = var["VISIT_ID"].ToString();
                m_strFileName = var["FILE_NAME"].ToString();
                string _strLocalPath = EmrInfoManagement.GetLocalFilePath(m_strPatientId, m_strFileName);
                File.Delete(_strLocalPath);
                EmrInfoManagement.RedirectSavePath(m_strPatientId, _strVisitId, m_strFileName);
                DoDownLoad(m_strPatientId, m_strFileName);
                //DoDownLoad();
                //Thread t = new Thread(DoDownLoad);
                //t.Start();
            }
            RemoteMessage.SendMessage("文件导出完毕");
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static bool ExistMrFile(string p_strPatientId, string p_strFileName)
        {
            string _strFilePath = Application.StartupPath + "\\file\\" + p_strPatientId + "_" + p_strFileName;
            if (File.Exists(_strFilePath))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 在执行 环境初始化后 下载文件
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <param name="p_strPatientId"></param>
        /// <returns></returns>
        public void DoDownLoad(string p_strPatientId,string p_strFileName)
        {
            object[] strArgs;
            strArgs = new object[3];
            strArgs[0] = 0;
            strArgs[1] = p_strFileName;
            strArgs[2] = p_strPatientId;
            string _strTemp = p_strPatientId + "_" + p_strFileName;
            if (!EMRArchiveAdaperUse.retrieveEmrFile(strArgs))
            {
                _nFCount++;
                RemoteMessage.SendMessage(_strTemp.PadRight(30, '　') + "FALSE");
            }
            else
            {
                _nTCount++;
                RemoteMessage.SendMessage(_strTemp.PadRight(30, '　') + "SUCCESS");
            }
            _strTemp = "导出完毕:[成功]" + _nTCount + "[失败]:" + _nFCount;
            RemoteMessage.SendMessage(_strTemp);
        }

        /// <summary>
        /// 在执行 环境初始化后 下载文件
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <param name="p_strPatientId"></param>
        /// <returns></returns>
        public void DoDownLoad()
        {
            m_nCount++;
            object[] strArgs;
            strArgs = new object[3];
            strArgs[0] = 0;
            strArgs[1] = m_strFileName;
            strArgs[2] = m_strPatientId;
            string _strTemp = m_strPatientId + "_" + m_strFileName;
            if (!EMRArchiveAdaperUse.retrieveEmrFile(strArgs))
            {
                RemoteMessage.SendMessage(_strTemp.PadRight(30, '　') + "FALSE");
            }
            else
            {
                RemoteMessage.SendMessage(_strTemp.PadRight(30, '　') + "SUCCESS");
            }
            //if (m_nCount>=m_dtPats.Rows.Count)
            //{
            //    RemoteMessage.SendMessage("==========================[数据导出完毕]==========================");
            //}
        }

    }
}
