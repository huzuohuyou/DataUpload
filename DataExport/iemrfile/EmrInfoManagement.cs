using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using ToolFunction;
using System.Xml;
using JHEMR.EmrSysCom;
using JHEMR.EmrSysAdaper;

namespace DataExport.文件接口
{
    public class EmrInfoManagement
    {

        [DllImport("EMRAssist.dll")]
        private static extern bool SaveFileToFieldElem(string strOpenFileName, string strSaveFileName, int nDocumentMode);

        [DllImport("EMRAssist.dll")]
        private static extern bool GetXmlNodeContent(string strOpenFileName, int nDocumentMode, int nType, string strFindText, short nLayerNo, short nFindType, [Out, MarshalAs(UnmanagedType.LPArray)]char[] strBuff, int nBufSize);//

        [DllImport("FSRVJH.DLL")]
        public static extern int get_file(string host_addr, string remote_file, string local_file, int option);



        /// <summary>
        /// 下载服务器文件状态初始化
        /// </summary>
        public static bool InitStatus()
        {
            try
            {
                EmrSysPubVar.fillHospitalName();
                if (EmrSysPubVar.fillFileSystemInfo())
                {
                    //RemoteMessage.SendMessage("连接文件服务器状态初始化.......");
                    EmrSysPubVar.setTempFileFullName(Application.StartupPath + "\\file\\mrtemp");
                    return true;
                }
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError(exp.Message);
                throw;
            }
            //RemoteMessage.SendMessage("请退出应用程序，配置当前病历路径描述表mr_work_path!");
            Application.ExitThread();
            Application.Exit();
            return false;
        }

        /// <summary>
        /// 重新设置保存的文件路径
        /// 路径为根目录file下 patientid|filename
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <param name="p_strFileName"></param>
        public static void RedirectSavePath(string p_strPatientId, string p_strVisitId, string p_strFileName)
        {
            EmrSysPubVar.setTempFileFullName(Application.StartupPath + "\\file\\" + p_strPatientId +"_"+p_strFileName);
        }



        /// <summary>
        /// 在执行 环境初始化后 下载文件
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <param name="p_strPatientId"></param>
        /// <returns></returns>
        public static bool DownLoadFile(string p_strFileName, string p_strPatientId)
        {
            string _strMess = "文件" + p_strPatientId + "|" + p_strFileName;
            object[] strArgs;
            strArgs = new object[3];
            strArgs[0] = 0;
            strArgs[1] = p_strFileName;
            strArgs[2] = p_strPatientId;
            try
            {
                if (!EMRArchiveAdaperUse.retrieveEmrFile(strArgs))
                {
                    return false;
                }
            }
            catch (Exception exp)
            {

                return false;
            }
            return true;
        }


        #region 取模板元素

        /// <summary>
        /// 取模板元素
        /// </summary>
        /// <param name="p_strFilePath"></param>
        /// <returns></returns>
        public static string GetElement(string p_strFilePath, string p_strElementName)
        {
            try
            {
                string _strXml = GetXmlContent(p_strFilePath);
                DataSet _dsReturn = new DataSet();
                StringReader _srXml = new StringReader(_strXml);
                XmlTextReader Xmlrdr = new XmlTextReader(_srXml);
                _dsReturn.ReadXml(Xmlrdr);
                if (_dsReturn.Tables[0].Rows.Count > 0)
                {
                    int count = _dsReturn.Tables[0].Rows.Count;
                    DataRow[] drCur = _dsReturn.Tables[0].Select(string.Format(@"FIELD_NAME='{0}'", p_strElementName));
                    if (drCur.Length == 1)
                    {
                        return drCur[0]["FIELD_TEXT"].ToString().Replace("{", "").Replace("}", "").Trim();
                    }
                }
            }
            catch (Exception exp)
            {
                return "";
            }
            return "";
        }

        /// <summary>
        /// 获取元素文件数据
        /// </summary>
        /// <param name="p_strPatientId">病人id</param>
        /// <param name="p_strVisitId">住院次</param>
        /// <param name="p_strFileName">文件名</param>
        /// <param name="p_strElementName">元素名</param>
        /// <returns></returns>
        public static string GetYSFileInfo(string p_strPatientId, string p_strVisitId, string p_strMrClass, string p_strElementName)
        {
            string _strElementValue = string.Empty;
            //string _strGenMrPath = GetRootMrPath(p_strPatientId, p_strVisitId, p_strMrClass);
            string _strFileName = GetFileName(p_strPatientId, p_strVisitId, p_strMrClass);
            if ("" == _strFileName)
            {
                m_bHasFile = false;
                return "";
            }
            string _strFilePath = GetLocalFilePath(p_strPatientId, _strFileName);
            //File.Delete(_strFilePath);
            if (!EmrFile.ExistMrFile(p_strPatientId, _strFileName))
            {
                DownLoadFile(_strFileName, p_strPatientId);
            }
            return GetElement(_strFilePath, p_strElementName);
        }

        /// <summary>
        /// 文件服务器IP
        /// </summary>
        /// <returns></returns>
        public static string GetServerIp()
        {
            DataTable _dtTemp = new DataTable();
            string _strIpAddress = string.Empty;
            string _strSQL = "select ip_addr,mr_path from mr_work_path ";
            _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtTemp.Rows.Count > 0)
            {
                _strIpAddress = _dtTemp.Rows[0]["ip_addr"].ToString();
                //mr_path = _dtTemp.Tables[0].Rows[0]["mr_path"].ToString();
            }
            return _strIpAddress;
        }

        /// <summary>
        /// 返回模板保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetFileRoot()
        {
            DataTable _dtTemp = new DataTable();
            string _strMrPath = string.Empty;
            string _strSQL = "select ip_addr,mr_path from mr_work_path ";
            _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtTemp.Rows.Count > 0)
            {
                //_strMrPath = ds.Tables[0].Rows[0]["ip_addr"].ToString();
                _strMrPath = _dtTemp.Rows[0]["mr_path"].ToString();
            }
            return _strMrPath;
        }

        /// <summary>
        /// 返回拷贝文件的路径文件名为 模板名+病人id+住院次
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <param name="nVisitID"></param>
        /// <param name="MR_CLASS"></param>
        /// <returns></returns>
        public static string GetCopyPath(string p_strPatientId, string p_strVisitId, string p_strFileName)
        {
            return Application.StartupPath + "\\file\\" + p_strFileName + p_strPatientId + p_strVisitId;
            //return Application.StartupPath + "\\file\\mrtemp";
        }

        /// <summary>
        /// 返回拷贝文件的路径文件名为\\file\\mrtemp
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <param name="nVisitID"></param>
        /// <param name="MR_CLASS"></param>
        /// <returns></returns>
        public static string GetLocalFilePath(string p_strPatienId,string p_strFileName)
        {
            return Application.StartupPath + "\\file\\" + p_strPatienId + "_" + p_strFileName;
        }

        /// <summary>
        /// 获取病人的根文件
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <param name="nVisitID"></param>
        /// <param name="MR_CLASS"></param>
        /// <returns></returns>
        public static string GetRootMrPath(string strPatientID, string nVisitID, string MR_CLASS)
        {
            string strPath, strTemp;
            if (strPatientID.Length < 2)
            {
                strPath = strPatientID;
                strTemp = "";
            }
            else
            {
                if (strPatientID.Length <= 8)
                {
                    strPath = strPatientID.Substring(strPatientID.Length - 2, 2); //取末尾两位
                    strTemp = strPatientID.Substring(0, strPatientID.Length - 2);  //取剩余部分
                }
                else
                {
                    strPath = strPatientID.Substring(strPatientID.Length - 4, 4); //取末尾四位
                    strTemp = strPatientID.Substring(0, strPatientID.Length - 4);  //取剩余部分
                }

            }
            if (strPath.Length == 1)
                strPath = "0" + strPath;
            strPath = strPath + "\\";
            string strPrefix = "00000000000000";
            if (strTemp.Length <= 8)
                strTemp = strPrefix.Substring(0, 8 - strTemp.Length) + strTemp;
            else
                strTemp = strPrefix.Substring(0, 12 - strTemp.Length) + strTemp;
            string _strSQL = ""; string ftpMrFileName = "";
            _strSQL = "select file_name from mr_file_index where patient_id='" + strPatientID + "' and visit_id=" + nVisitID.ToString() + " and (file_name= '" + MR_CLASS + "' OR MR_CLASS= '" + MR_CLASS + "' or topic LIKE '%" + MR_CLASS + "%')";
            DataTable _dtFile = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR"); 
            if (_dtFile.Rows.Count > 0)
            {
                ftpMrFileName = _dtFile.Rows[0]["file_name"].ToString();
            }
            strPath = getMrPath() + strPath + strTemp + "\\" + ftpMrFileName;
            return strPath;
        }

        /// <summary>
        /// 获取mr_file_index的文件名
        /// </summary>
        /// <param name="p_strPatiendId"></param>
        /// <param name="p_strVisitId"></param>
        /// <param name="p_strMrClass"></param>
        /// <returns></returns>
        public static string GetFileName(string p_strPatiendId, string p_strVisitId, string p_strMrClass)
        {
            string _strSQL = "select file_name from mr_file_index where patient_id='" + p_strPatiendId + "' and visit_id=" + p_strVisitId + " and (file_name= '" + p_strMrClass + "' OR MR_CLASS= '" + p_strMrClass + "' or topic LIKE '%" + p_strMrClass + "%')";
            DataTable _dtFile = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtFile.Rows.Count > 0)
            {
                return _dtFile.Rows[0]["file_name"].ToString();
            }
            return "";
        }

        public static string getMrPath()
        {
            string _strMrPath = string.Empty;
            string _strSQL = "select mr_path,file_user,file_pwd,ip_addr from mr_work_path";
            DataTable _dtInfo = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtInfo.Rows.Count > 0)
            {
                _strMrPath = _dtInfo.Rows[0]["mr_path"].ToString();
            }
            return _strMrPath;
        }

        /// <summary>
        /// 获取病历文件打散的xml
        /// </summary>
        /// <param name="strMrFile"></param>
        /// <returns></returns>
        public static string GetXmlContent(string strMrFile)
        {
            try
            {
                string strRootPath = strMrFile + ".xml";
                string strContent = string.Empty;
                if (SaveFileToFieldElem(strMrFile, strRootPath, 1))
                {
                    strContent = System.IO.File.ReadAllText(strRootPath, Encoding.Default);
                }
                return strContent;
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError(exp.ToString());
                return "";
            }
            return "";
        }


        ///// <summary>
        ///// 获取病历文件打散的xml
        ///// </summary>
        ///// <param name="strMrFile"></param>
        ///// <returns></returns>
        //public static string GetXmlContent(string strMrFile)
        //{
        //    string strRootPath = strCopyPath + ".xml";
        //    string strContent = "";
        //    if (SaveFileToFieldElem(strMrFile, strRootPath, 1))
        //    {
        //        strContent = System.IO.File.ReadAllText(strRootPath, Encoding.Default);
        //    }
        //    return strContent;
        //}

        #endregion

        #region 取模板层次号元素

        /// <summary>
        /// 取层次号值
        /// </summary>
        /// <param name="p_strLocalPath"></param>
        /// <param name="p_strElementName"></param>
        /// <returns></returns>
        public static string GetCC(string p_strLocalPath, string p_strElementName)
        {
            string strPath = Application.StartupPath + "\\file\\" + p_strLocalPath;
            GetXmlContent(strPath);
            string _strFile = GetXmlNodeStr(strPath, p_strElementName);
            if (_strFile.Length > 512)
            {
                return _strFile.Substring(0, 512).Replace("{", "").Replace("}", "");
            }
            else
            {
                return _strFile;
            }
            return "";
        }
        /// <summary>
        /// 没有文件不导出
        /// </summary>
        public static bool m_bHasFile = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <param name="p_strFileName"></param>
        /// <param name="p_strElementName"></param>
        /// <returns></returns>
        public static string GetCCFileInfo(string p_strPatientId, string p_strVisitId, string p_strMrClass, string p_strElementName)
        {
            string _strFileName = GetFileName(p_strPatientId, p_strVisitId, p_strMrClass);
            if ("" == _strFileName)
            {
                m_bHasFile = false;
                return "";
            }
            string _strLocalPath = GetLocalFilePath(p_strPatientId, _strFileName);
            RedirectSavePath(p_strPatientId, p_strVisitId, _strFileName);
            if (!EmrFile.ExistMrFile(p_strPatientId, _strFileName))
            {
                DownLoadFile(_strFileName, p_strPatientId);
            }
            return GetCC(p_strPatientId + "_" + _strFileName, p_strElementName);
        }

        #endregion

        /// <summary>
        /// 层次号取主诉
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetXmlNodeStr(string p_strPath, string ElemName)
        {
            string strPath = p_strPath;
            //string strPath = Application.StartupPath + "\\file\\" + p_strPath;
            try
            {
                bool bReturn;
                char[] sReCheckCodeList = new char[2560];
                
                bReturn = GetXmlNodeContent(strPath, 0, 0, ElemName, 1, 1, sReCheckCodeList, 2560);
                if (!bReturn)
                {
                    bReturn = GetXmlNodeContent(strPath, 0, 0, ElemName, 2, 1, sReCheckCodeList, 2560);
                }
                ArrayList al = new ArrayList();
                for (int i = 0; i < sReCheckCodeList.Length; i++)
                {
                    if (sReCheckCodeList[i].ToString().Trim() != "\0")
                        al.Add(sReCheckCodeList[i]);
                    else
                        break;
                }
                char[] str2 = new char[al.Count];
                for (int i = 0; i < str2.Length; i++)
                {
                    str2[i] = (char)al[i];
                }
                string s = new string(str2);
                return s;
            }
            catch (Exception exp)
            {
                return "";
                CommonFunction.WriteError("取层次号错误" + strPath + "|" + ElemName);
            }

        }

        ///// <summary>
        ///// 获取病历文件打散的xml
        ///// </summary>
        ///// <param name="strMrFile"></param>
        ///// <returns></returns>
        //public static string GetXmlContent(string strMrFile)
        //{
        //    string strRootPath = strCopyPath + ".xml";
        //    string strContent = "";
        //    if (SaveFileToFieldElem(strMrFile, strRootPath, 1))
        //    {
        //        strContent = System.IO.File.ReadAllText(strRootPath, Encoding.Default);
        //    }
        //    return strContent;
        //}


        /// <summary>
        /// 层次号取主诉
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        //public static string GetXmlNodeStr(string strPath, string ElemName)
        //{
        //    //string strRootPath = Application.StartupPath + "\\content";
        //    bool bReturn;
        //    char[] sReCheckCodeList = new char[2560];

        //    bReturn = GetXmlNodeContent(strPath, 0, 0, ElemName, 1, 1, sReCheckCodeList, 2560);
        //    if (!bReturn)
        //    {
        //        bReturn = GetXmlNodeContent(strPath, 0, 0, ElemName, 2, 1, sReCheckCodeList, 2560);
        //    }
        //    ArrayList al = new ArrayList();
        //    for (int i = 0; i < sReCheckCodeList.Length; i++)
        //    {
        //        if (sReCheckCodeList[i].ToString().Trim() != "\0")
        //            al.Add(sReCheckCodeList[i]);
        //        else
        //            break;
        //    }

        //    char[] str2 = new char[al.Count];
        //    for (int i = 0; i < str2.Length; i++)
        //    {
        //        str2[i] = (char)al[i];
        //    }
        //    string s = new string(str2);
        //    return s;
        //}

    }
}
