using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ToolFunction;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using DataExport.�ļ��ӿ�;
using System.Text.RegularExpressions;

namespace DataExport
{
    /// <summary>
    /// ���½ڵķ�ʽ��������եȡ
    /// 2015-11-06 
    /// �⺣��
    /// </summary>
    public class GrabInfo:IExport
    {
        public string m_strClass = string.Empty;
        public string m_strTableName = string.Empty;
        public string m_strChapterName = string.Empty;
        public string m_strDataDatail = string.Empty;

        #region IExport ��Ա

        public void Export()
        {
            //PublicProperty.ExportParam[0];
        }

        #endregion

        /// <summary>
        /// ��ȡ�½�����
        /// 2015-11-06
        /// �⺣��
        /// </summary>
        public static DataSet GetChapterDetail(string p_strObjectName)
        {
            //DataSet _dsChapterDetail = new DataSet();
            DataSet _dsMultyChapterDetail = GetMultyChapterDetail(p_strObjectName);
            DataTable _dtSimpleChapterDetail = GetSimpleChapterDetail(p_strObjectName);
            _dsMultyChapterDetail.Tables.Add(_dtSimpleChapterDetail.Copy());
            return _dsMultyChapterDetail;
        }

        /// <summary>
        /// ��ȡ�½�����
        /// ���ڻ�ȡ�ֶλ�ȡ���ݷ�ʽ
        /// </summary>
        /// <returns></returns>
        public static DataTable GetChapterDescription(string p_strObjectName)
        {
            string _strSQL = string.Format(@"select * from pt_chapter_dict where table_name = '{0}' ", p_strObjectName);
            return CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
        }

        /// <summary>
        /// �������½ڵı�
        /// </summary>
        /// <param name="p_strTable"></param>
        /// <returns></returns>
        public static DataTable GetSubChapterDict(string p_strTable)
        {
            DataTable _dtResult = new DataTable();
            _dtResult.Columns.Add("PATIENT_ID");
            _dtResult.Columns.Add("VISIT_ID");
            Regex reg = new Regex(@"\[[^\[^\]]*\]");
            string _strTable = p_strTable;
            int _nCount = reg.Matches(_strTable).Count;
            for (int i = 0; i < _nCount; i++)
            {
                _dtResult.Columns.Add(reg.Matches(_strTable)[i].Captures[0].Value);
            }
            return _dtResult;
        }

        /// <summary>
        /// ��ȡ����½�����
        /// 2015-11-06
        /// �⺣��
        /// </summary>
        public static DataSet GetMultyChapterDetail(string p_strObjectName)
        {
            DataSet _dsChapter = new DataSet();
            string _strSQL = string.Format(@"select * from pt_chapter_dict where table_name = '{0}' and class = 'TABLE'", p_strObjectName);
            DataTable _dtChapter = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            int _nCount = 0;
            foreach (DataRow var in _dtChapter.Rows)
            {
                DataTable _dtTemp = GetSubChapterDict(var["CHAPTER_NAME"].ToString());
                _dtTemp.TableName = "TABLE_" + var["CHAPTER_NAME"].ToString();
                _dsChapter.Tables.Add(_dtTemp.Copy());
                _nCount++;
            }
            return _dsChapter;
        }

        /// <summary>
        /// ��ȡ�����½�
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <returns></returns>
        public static DataTable GetSimpleChapterDetail(string p_strObjectName)
        {
            string _strSQL = string.Format(@"select * from pt_chapter_dict where table_name = '{0}' and class != 'TABLE'", p_strObjectName);
            DataTable _dtSimpleChapter = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            DataTable _dtTemp = new DataTable();
            _dtTemp.Columns.Add("PATIENT_ID");
            _dtTemp.Columns.Add("VISIT_ID");
            foreach (DataRow var in _dtSimpleChapter.Rows)
            {
                _dtTemp.Columns.Add(var["CHAPTER_NAME"].ToString());
            }
            _dtTemp.TableName = p_strObjectName;
            return _dtTemp;
        }

        /// <summary>
        /// ��ȡ�����ֶ�
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEnabledObject()
        {
            string _strSQL = string.Format(@"select * from pt_tables_dict where exportflag = 'TRUE'");
            return CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
        }

        /// <summary>
        /// ���ɶ����Ӧ��DataTable
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <returns></returns>
        public static DataSet GetObjectLayOut(string p_strObjectName)
        {
            DataSet _dsObject = GetChapterDetail(p_strObjectName);
            return _dsObject;
        }

        /// <summary>
        /// �����ݿ�ץȡ���ݵ�sql
        /// </summary>
        /// <returns></returns>
        public string GetGrabSQL(DataTable p_dtObject,string p_strChapterName) {

            DataRow[] _drDataDetail = p_dtObject.Select("CHAPTER_NAME = '{0}'", p_strChapterName);
            if (_drDataDetail.Length==1)
            {
               return _drDataDetail[0]["DATA_DETAIL"].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// �����ݿ��ȡ����
        /// ��ѯ������Ϊһ��һЩ������
        /// ��������ѯ������Ϊ�����򷵻ؿ�
        /// </summary>
        /// <returns></returns>
        public static string GrabPatientInfoFromDB(string p_strChapterDetail, string p_strPatientId, string p_strVisitId)
        {
            string _strSQL = p_strChapterDetail.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId);
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtTemp.Rows.Count == 1)
            {
                return _dtTemp.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// ��ģ��ץȡ����
        /// 2015-11-09
        /// �⺣��
        /// </summary>
        /// <param name="p_strPatientId">����id</param>
        /// <param name="p_strVisitId">סԺ��</param>
        /// <param name="p_strDataDatail">���������ַ��������ļ�����[AB,AC]|�ļ���|Ԫ����</param>
        /// <returns></returns>
        public static string GrabPatientInfoFromFile(string p_strDataDatail, string p_strPatientId, string p_strVisitId)
        {
            string[] _arrParam = p_strDataDatail.Split('|');
            string _strFileType = string.Empty;
            string _strFileName = string.Empty;
            string _strElementName = string.Empty;
            string _strResult = string.Empty;
            if (_arrParam.Length == 3)
            {
                _strFileType = _arrParam[0];
                _strFileName = _arrParam[1];
                _strElementName = _arrParam[2];
                if ("��κ�" == _strFileType)
                {
                    _strResult = EmrInfoManagement.GetABFileInfo(p_strPatientId, p_strVisitId, _strFileName, _strElementName);
                }
                else if ("Ԫ��" == _strFileType)
                {
                    _strResult = EmrInfoManagement.GetACFileInfo(p_strPatientId, p_strVisitId, _strFileName, _strElementName);
                }
                else
                {
                    CommonFunction.WriteError("δ֪�ļ�����:" + _strFileType);
                }
            }
            else
            {
                CommonFunction.WriteError("�����������:" + p_strDataDatail);
            }
            return _strResult;
        }

        /// <summary>
        /// ��ȡ�����ݴ����ݿ�
        /// </summary>
        /// <param name="p_strDataDatail"></param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <returns></returns>
        public static DataTable GrabPatientTableInfoFromDB(string p_strDataDatail, string p_strPatientId, string p_strVisitId)
        {
            string _strSQL = p_strDataDatail.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId);
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            return _dtTemp;
        }

        /// <summary>
        /// ��ȡ����ʽ������Ϣ
        /// </summary>
        /// <param name="p_dtObjectReflect"></param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <returns></returns>
        public static DataTable GrabMultiPatientInfo(DataTable p_dtObjectReflect, string p_strPatientId, string p_strVisitId, string p_strObjectName)
        {
            DataTable _dtObjecDetail = GetChapterDescription(p_strObjectName);
            DataTable _dtObjectReflect = p_dtObjectReflect;
            //DataRow _drObjectReflect = _dtObjectReflect.NewRow();
            string _strPatientId = p_strPatientId;
            string _strVisitId = p_strVisitId;
            string _strClass = string.Empty;
            string _strTableName = string.Empty;
            string _strChapterName = p_dtObjectReflect.TableName.Replace("TABLE_","");
            string _strDataDatail = string.Empty;
            DataRow[] _arrObjectReflect = _dtObjecDetail.Select(string.Format("CHAPTER_NAME = '{0}'", _strChapterName));
            if (_arrObjectReflect.Length == 1)
            {
                _strClass = _arrObjectReflect[0]["CLASS"].ToString();
                _strTableName = _arrObjectReflect[0]["TABLE_NAME"].ToString();
                _strChapterName = _arrObjectReflect[0]["CHAPTER_NAME"].ToString();
                _strDataDatail = _arrObjectReflect[0]["DATA_DETAIL"].ToString();
            }

            //foreach (DataColumn _dcObjectReflect in _dtObjectReflect.Columns)
            //{
            //DataRow[] _arrObjectReflect = _dtObjecDetail.Select(string.Format("CHAPTER_NAME = '{0}'", _dcObjectReflect.Caption));
            //if (_arrObjectReflect.Length == 1)
            //{
            if ("TABLE" == _strClass)
            {
                DataTable _dtTemp = GrabPatientTableInfoFromDB(_strDataDatail, _strPatientId, _strVisitId);
                foreach (DataRow _drSubPatInfo in _dtTemp.Rows)
                {
                    _dtObjectReflect.Rows.Add(_drSubPatInfo.ItemArray);
                }
            }
            //else
            //{
            //    continue;
            //}
            string _strField = _strPatientId + "|" + _strVisitId + "|" + p_strObjectName;
            RemoteMessage.SendMessage(_strField.PadRight(30, '.') + "TABLE_DATE");
            //}
            //}
            return _dtObjectReflect;
        }

        /// <summary>
        /// ��ȡ���в�����Ϣ
        /// </summary>
        /// <param name="p_dtObjectReflect"></param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <returns></returns>
        public static DataTable GrabSimplePatientInfo(DataTable p_dtObjectReflect,string p_strPatientId,string p_strVisitId,string p_strObjectName)
        {
            DataTable _dtObjecDetail = GetChapterDescription(p_strObjectName);
            DataTable _dtObjectReflect = p_dtObjectReflect;
            DataRow _drObjectReflect = _dtObjectReflect.NewRow();
            string _strPatientId = p_strPatientId;
            string _strVisitId = p_strVisitId;
            foreach (DataColumn _dcObjectReflect in _dtObjectReflect.Columns)
            {
                DataRow[] _arrObjectReflect = _dtObjecDetail.Select(string.Format("CHAPTER_NAME = '{0}'", _dcObjectReflect));
                if (_arrObjectReflect.Length == 1)
                {
                    string _strClass = _arrObjectReflect[0]["CLASS"].ToString();
                    string _strTableName = _arrObjectReflect[0]["TABLE_NAME"].ToString();
                    string _strChapterName = _arrObjectReflect[0]["CHAPTER_NAME"].ToString();
                    string _strDataDatail = _arrObjectReflect[0]["DATA_DETAIL"].ToString();
                    string _strTempValue = string.Empty;
                    if ("DB" == _strClass)
                    {
                        _strTempValue = GrabPatientInfoFromDB(_strDataDatail, _strPatientId, _strVisitId);
                    }
                    else if ("FILE" == _strClass)
                    {
                        _strTempValue = GrabPatientInfoFromFile(_strDataDatail, _strPatientId, _strVisitId);
                    }
                    else
                    {
                        continue;
                    }
                    _drObjectReflect[_strChapterName] = _strTempValue;
                    string _strField = _strPatientId + "|" + _strVisitId + "|" + _strChapterName;
                    RemoteMessage.SendMessage(_strField.PadRight(30, '.') + _strTempValue);
                }
            }
            _drObjectReflect["PATIENT_ID"] = _strPatientId;
            _drObjectReflect["VISIT_ID"] = _strVisitId;
            _dtObjectReflect.Rows.Add(_drObjectReflect);
            return _dtObjectReflect;
        }

        public static void GrabPatientInfo()
        {
            PublicProperty.ExportData.Tables.Clear();
            DataTable _dtObjecDict = GetEnabledObject();
            foreach (DataRow _drObject in _dtObjecDict.Rows)
            {
                string _strObject = _drObject["TABLE_NAME"].ToString();
                //DataTable _dtObjecDetail = GetChapterDetail(_strObject);
                DataSet _dsObjectReflect = GetObjectLayOut(_strObject);
                foreach (DataRow _drPatient in PublicProperty.m_dsPatients.Rows)
                {
                    string _strPatientId = _drPatient["PATIENT_ID"].ToString();
                    string _strVisitId = _drPatient["VISIT_ID"].ToString();
                    foreach (DataTable _dtObjectReflect in _dsObjectReflect.Tables)
                    {
                        if (_dtObjectReflect.TableName.Contains("TABLE"))
                        {
                            DataTable _dtMultiPatientInfo = GrabMultiPatientInfo(_dtObjectReflect, _strPatientId, _strVisitId, _strObject);
                            PublicProperty.ExportData.Tables.Add(_dtMultiPatientInfo.Copy());
                        }
                        else
                        {
                            DataTable _dtSimplePatientInfo = GrabSimplePatientInfo(_dtObjectReflect, _strPatientId, _strVisitId, _strObject);
                            PublicProperty.ExportData.Tables.Add(_dtSimplePatientInfo.Copy());
                        }
                    }
                }
                //DataTable _dtTemp = _dsObjectReflect.Copy();
                //_dtTemp.TableName = _strObject;
                //if (!PublicProperty.ExportData.Tables.Contains(_dtTemp.TableName))
                //{
                //    PublicProperty.ExportData.Tables.Add(_dtTemp);
                //}
                //else
                //{
                //    PublicProperty.ExportData.Tables.Remove(_dtTemp.TableName);
                //    PublicProperty.ExportData.Tables.Add(_dtTemp);
                //}
                //����ת��2015-09-09 �⺣��
                ConversionData.ExchangeData();
                ExeExport();
            }
        }


        //public static DataSet GrabPatientInfo(string p_strObject,string p_strPatientId,string p_strVisitId)
        //{
        //    DataSet _dsOnePatInfo = new DataSet();
        //    DataTable _dtObjecDict = GetChapterDict();
        //    string _strObject = p_strObject;
        //    //DataTable _dtObjecDetail = GetChapterDetail(_strObject);
        //    DataSet _dsObjectReflect = GetObjectLayOut(_strObject);
        //    //DataTable _dtObjectReflect = GetObjectLayOut(_strObject);
        //    DataRow _drObjectReflect = _dtObjectReflect.NewRow();
        //    string _strPatientId = p_strPatientId;
        //    string _strVisitId = p_strVisitId;
        //    foreach (DataColumn _dcObjectReflect in _dtObjectReflect.Columns)
        //    {
        //        DataRow[] _arrObjectReflect = _dtObjecDetail.Select(string.Format("CHAPTER_NAME = '{0}'", _dcObjectReflect));
        //        if (_arrObjectReflect.Length == 1)
        //        {
        //            string _strClass = _arrObjectReflect[0]["CLASS"].ToString();
        //            string _strTableName = _arrObjectReflect[0]["TABLE_NAME"].ToString();
        //            string _strChapterName = _arrObjectReflect[0]["CHAPTER_NAME"].ToString();
        //            string _strDataDatail = _arrObjectReflect[0]["DATA_DETAIL"].ToString();
        //            string _strTempValue = string.Empty;
        //            if ("DB" == _strClass)
        //            {
        //                _strTempValue = GrabPatientInfoFromDB(_strDataDatail, _strPatientId, _strVisitId);
        //            }
        //            else if("FILE"==_strClass)
        //            {
        //                _strTempValue = GrabPatientInfoFromFile(_strDataDatail, _strPatientId, _strVisitId);
        //            }
        //            else if("TABLE"==_strClass)
        //            {
        //                DataTable _dtTemp= GrabPatientTableInfoFromDB(_strDataDatail, _strPatientId, _strVisitId);
        //                _dtTemp.TableName = "TABLE";
        //                _dsOnePatInfo.Tables.Add(_dtTemp.Copy());
        //            }
        //            _drObjectReflect[_strChapterName] = _strTempValue;
        //            string _strField = _strPatientId + "|" + _strVisitId + "|" + _strChapterName;
        //            RemoteMessage.SendMessage(_strField.PadRight(0, ' ') + _strTempValue);
        //        }
        //    }
        //    _drObjectReflect["PATIENT_ID"] = _strPatientId;
        //    _drObjectReflect["VISIT_ID"] = _strVisitId;
        //    _dtObjectReflect.Rows.Add(_drObjectReflect);
        //    _dtObjectReflect.TableName = _strObject;
        //    _dsOnePatInfo.Tables.Add(_dtObjectReflect);
        //    return _dsOnePatInfo;
        //}

        /// <summary>
        /// ��ȡ��������ָ����������
        /// </summary>
        /// <param name="p_strObject">������</param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <returns></returns>
        public static DataSet GrabPatientInfo(string p_strObject, string p_strPatientId, string p_strVisitId)
        {
            DataSet _dsOnePatInfo = new DataSet();
            DataSet _dsObjectReflect = GetObjectLayOut(p_strObject);
            string _strObject = p_strObject;
            string _strPatientId = p_strPatientId;
            string _strVisitId = p_strVisitId;
            foreach (DataTable _dtObjectReflect in _dsObjectReflect.Tables)
            {
                if (_dtObjectReflect.TableName.Contains("TABLE"))
                {
                    DataTable _dtMultiPatientInfo = GrabMultiPatientInfo(_dtObjectReflect, _strPatientId, _strVisitId, _strObject);
                    _dsOnePatInfo.Tables.Add(_dtMultiPatientInfo.Copy());
                }
                else
                {
                    DataTable _dtSimplePatientInfo = GrabSimplePatientInfo(_dtObjectReflect, _strPatientId, _strVisitId, _strObject);
                    _dsOnePatInfo.Tables.Add(_dtSimplePatientInfo.Copy());
                }
            }
            return _dsOnePatInfo;
        }

        /// <summary>
        /// ��ȡ��sql��ѯ���Ĳ�����Ϣ
        /// </summary>
        /// <param name="dql">sql��</param>
        /// <returns>������Ϣ</returns>
        public static void GetPatientData()
        {
            PublicProperty.ExportData.Tables.Clear();
            DataTable _dtSql = PublicProperty.m_dtSQL;
            foreach (DataRow dr in _dtSql.Rows)
            {
                DataTable _dtTemp = new DataTable();
                foreach (DataRow drpat in PublicProperty.m_dsPatients.Rows)
                {
                    string _strSQL = string.Format(dr["sql"].ToString().Replace("@PATIENT_ID", drpat["PATIENT_ID"].ToString()).Replace("@VISIT_ID", drpat["VISIT_ID"].ToString()));
                    //_dtTemp.Merge(DALUse.Query(_strSQL).Tables[0]);
                    _dtTemp.Merge(CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR"));
                    RemoteMessage.SendMessage("��ѯ������Ϣ\n" + _strSQL);
                }
                DataTable _dt = _dtTemp.Copy();
                _dt.TableName = dr["TABLE_NAME"].ToString();
                PublicProperty.ExportData.Tables.Add(_dt);
            }
            //����ת��2015-09-09 �⺣��
            ConversionData.ExchangeData();
            ExeExport();
        }

        /// <summary>
        /// ��ȡƽ̨���õ�sql
        /// </summary>
        /// <returns>����sql�б�</returns>
        public static DataTable GetConfigSQL()
        {
            string _strSQL = string.Format("select table_name,MS,'' sql from pt_tables_dict b where b.exportflag ='TRUE'");
            RemoteMessage.SendMessage("���ڻ�ȡ����SQL:" + _strSQL);
            DataTable _dtSQL = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            foreach (DataRow var in _dtSQL.Rows)
            {
                var["sql"] = GetSQL(var["table_name"].ToString());
            }
            return _dtSQL;
        }

        /// <summary>
        /// ͨ��sql����ȡ�ļ�
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string GetSQL(string p_strFileName)
        {
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/SQL/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".sql";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath))
                    {
                        _strSQL = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
            }
            return _strSQL;
        }

        /// <summary>
        /// ִ�е���
        /// </summary>
        public static void ExeExport()
        {
            IExport ie = null;
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            switch (_strExportType)
            {
                case "DB":
                    ie = new ExportDB();
                    PublicProperty.ExportParam[0] = PublicProperty.ExportData;
                    break;
                case "DBF":
                    ie = new ExportDBF();
                    string _strDbfPath =  uctlBaseConfig.GetConfig("DbfPath");
                    PublicProperty.ExportParam[0] = _strDbfPath;
                    PublicProperty.ExportParam[1] = PublicProperty.ExportData.Tables[0];
                    break;
                case "EXCLE":
                    ie = new ExportExcel();
                    string _strExceltPath =  uctlBaseConfig.GetConfig("ExceltPath");
                    PublicProperty.ExcelPath = _strExceltPath;
                    PublicProperty.ExcelSource = PublicProperty.ExportData.Tables[0];
                    break;
                case "XML":
                    ie = new ExportXml();
                    PublicProperty.ExportParam[0] = PublicProperty.ExportData;
                    break;
                default:
                    CommonFunction.WriteError("δ֪��������:" + _strExportType);
                    break;
            }
            Thread t = new Thread(new ThreadStart(ie.Export));
            t.Start();
        }


        /// <summary>
        /// ִ�е���
        /// </summary>
        public static void ExeExport(DataSet p_dsOnePatInfo,string p_strObjectName ,string p_strPatientId,string p_strVisitId)
        {
            IExport ie = null;
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            switch (_strExportType)
            {
                case "DB":
                    ie = new ExportDB();
                    PublicProperty.ExportParam[0] = p_dsOnePatInfo;
                    break;
                case "DBF":
                    ie = new ExportDBF();
                    string _strDbfPath = uctlBaseConfig.GetConfig("DbfPath");
                    PublicProperty.ExportParam[0] = _strDbfPath;
                    PublicProperty.ExportParam[1] = p_dsOnePatInfo;
                    break;
                case "EXCLE":
                    ie = new ExportExcel();
                    string _strExceltPath = uctlBaseConfig.GetConfig("ExceltPath");
                    PublicProperty.ExcelPath = _strExceltPath;
                    //PublicProperty.ExcelSource = p_dtOnePatInfo;
                    break;
                case "XML":
                    ie = new ExportXml(p_dsOnePatInfo, p_strObjectName, p_strPatientId, p_strVisitId);
                    PublicProperty.ExportParam[0] = p_dsOnePatInfo;
                    break;
                default:
                    CommonFunction.WriteError("δ֪��������:" + _strExportType);
                    break;
            }
            Thread t = new Thread(new ThreadStart(ie.Export));
            t.Start();
        }

    }
}