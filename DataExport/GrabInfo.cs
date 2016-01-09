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
        public static DataSet m_dsPatDBInfo = new DataSet();

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
        /// ��ʼ���ϴ����ݿ���Ϣ
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        public static void InitPatDBInfo(string p_strPatientId, string p_strVisitId)
        {
            m_dsPatDBInfo.Tables.Clear();
            DataSet _dsObj = DBTemplet.GetSQL();
            DataRow[] _arrSQL = _dsObj.Tables[0].Select("name like '#%'");
            foreach (DataRow var in _arrSQL)
            {
                string _strName = var["NAME"].ToString().ToUpper().Replace("#","");
                string _strSQL = var["SQL"].ToString().ToUpper();
                _strSQL = string.Format(_strSQL.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId));
                DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, _strName, PublicVar.m_strEmrConnection);
                m_dsPatDBInfo.Tables.Add(_dtTemp.Copy());
            }
        }

        /// <summary>
        /// ��ȡdataset�й��˵ı�
        /// û�з���null
        /// 2015-11-21����������
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        public static DataTable GetPatTable(string p_strTableName)
        {
            if (m_dsPatDBInfo.Tables.Contains(p_strTableName))
            {
                return m_dsPatDBInfo.Tables[p_strTableName];
            }
            RemoteMessage.SendMessage("���ݼ�������������" + p_strTableName);
            return null;
        }

        /// <summary>
        /// ��ȡ�����ֶ�ֵ
        /// </summary>
        /// <param name="p_dtTable"></param>
        /// <param name="p_strName"></param>
        /// <returns></returns>
        public static string GetPatItemInfo(DataTable p_dtTable, string p_strName)
        {
            if (p_dtTable != null && p_dtTable.Rows.Count == 1 && p_dtTable.Columns.Contains(p_strName))
            {
                return p_dtTable.Rows[0][p_strName].ToString();
            }
            RemoteMessage.SendMessage("��Ϊ��|�в�Ϊ1|�����޴���" + p_strName);
            return "";
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

        public static string GetTableName(string p_strDataDetail)
        {
            string[] _arrParam = p_strDataDetail.Split('|');
            if (_arrParam.Length==2)
            {
                return _arrParam[0];
            }
            return "";
        }

        public static string GetFieldName(string p_strDataDetail)
        {
            string[] _arrParam = p_strDataDetail.Split('|');
            if (_arrParam.Length == 2)
            {
                return _arrParam[1];
            }
            return "";
        }

        /// <summary>
        /// ͨ��sql�����ݿ�ץȡ����
        /// sql��@���ſ�ʼ�����޷�ʶ��
        /// </summary>
        /// <param name="p_strChapterDetail"></param>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        /// <returns></returns>
        public static string GrabPatientInfoFromDBBySQL(string p_strChapterDetail, string p_strPatientId, string p_strVisitId)
        {
            string _strSQL = p_strChapterDetail.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId);
            _strSQL = _strSQL.Replace("@", "");
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtTemp == null)
            {
                return "";
            }
            else if (_dtTemp.Rows.Count == 1)
            {
                return _dtTemp.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// ͨ���̶����������ݿ��ȡ����
        /// ��ѯ������Ϊһ��һЩ������
        /// ��������ѯ������Ϊ�����򷵻ؿ�
        /// </summary>
        /// <returns></returns>
        public static string GrabPatientInfoFromDBByParam(string p_strChapterDetail, string p_strPatientId, string p_strVisitId)
        {
            string _strTableName = GetTableName(p_strChapterDetail);
            string _strFieldName = GetFieldName(p_strChapterDetail);
            DataTable _dtDB = GetPatTable(_strTableName);
            string _strValue = GetPatItemInfo(_dtDB, _strFieldName);
            return _strValue;
           // string _strSQL = string.Empty;
           // string[] _arrDataDetail = p_strChapterDetail.Split('|');
           // _strSQL="SELECT "
        
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
                    _strResult = EmrInfoManagement.GetCCFileInfo(p_strPatientId, p_strVisitId, _strFileName, _strElementName);
                }
                else if ("Ԫ��" == _strFileType)
                {
                    _strResult = EmrInfoManagement.GetYSFileInfo(p_strPatientId, p_strVisitId, _strFileName, _strElementName);
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
                        if (_strDataDatail.StartsWith("@"))
                        {
                            _strTempValue = GrabPatientInfoFromDBBySQL(_strDataDatail, _strPatientId, _strVisitId);

                        }
                        else
                        {
                            _strTempValue = GrabPatientInfoFromDBByParam(_strDataDatail, _strPatientId, _strVisitId);

                        }
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
            PublicVar.ExportData.Tables.Clear();
            DataTable _dtObjecDict = GetEnabledObject();
            foreach (DataRow _drObject in _dtObjecDict.Rows)
            {
                string _strObject = _drObject["TABLE_NAME"].ToString();
                //DataTable _dtObjecDetail = GetChapterDetail(_strObject);
                DataSet _dsObjectReflect = GetObjectLayOut(_strObject);
                foreach (DataRow _drPatient in PublicVar.m_dsPatients.Rows)
                {
                    string _strPatientId = _drPatient["PATIENT_ID"].ToString();
                    string _strVisitId = _drPatient["VISIT_ID"].ToString();
                    foreach (DataTable _dtObjectReflect in _dsObjectReflect.Tables)
                    {
                        if (_dtObjectReflect.TableName.Contains("TABLE"))
                        {
                            DataTable _dtMultiPatientInfo = GrabMultiPatientInfo(_dtObjectReflect, _strPatientId, _strVisitId, _strObject);
                            PublicVar.ExportData.Tables.Add(_dtMultiPatientInfo.Copy());
                        }
                        else
                        {
                            DataTable _dtSimplePatientInfo = GrabSimplePatientInfo(_dtObjectReflect, _strPatientId, _strVisitId, _strObject);
                            PublicVar.ExportData.Tables.Add(_dtSimplePatientInfo.Copy());
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
                //ExeExport();
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
            PublicVar.successcount = 0;
            PublicVar.falsecount = 0;
            DataTable _dtSql = GrabInfo.GetConfigSQL();
            foreach (DataRow dr in _dtSql.Rows)
            {
                foreach (DataRow drpat in PublicVar.m_dsPatients.Rows)
                {
                    string _strSQL = string.Format(dr["sql"].ToString().Replace("@PATIENT_ID", drpat["PATIENT_ID"].ToString()).Replace("@VISIT_ID", drpat["VISIT_ID"].ToString()));
                    RemoteMessage.SendMessage("��ѯ������Ϣ" + dr["TABLE_NAME"].ToString() + "---" + drpat["PATIENT_ID"].ToString() + "---" + drpat["VISIT_ID"].ToString());
                    DataTable _dtOnePatOneObj = CommonFunction.OleExecuteBySQL(_strSQL, dr["TABLE_NAME"].ToString(), "EMR");
                    _dtOnePatOneObj = ConversionData.ExchangeData(_dtOnePatOneObj);
                    ExeExport(_dtOnePatOneObj);                   
                }
            }
            RemoteMessage.SendMessage("����ɹ�:" + PublicVar.successcount + "\n����ʧ��:" + PublicVar.falsecount);
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
        public static void ExeExport(DataTable p_dtOnePatInfo)
        {
            IExport ie = null;
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            switch (_strExportType)
            {
                case "DB":
                    ie = new SingleObjectDBExport(p_dtOnePatInfo);
                    break;
                case "DBF":
                    ie = new ExportDBF(p_dtOnePatInfo);
                    break;
                case "EXCLE":
                    ie = new ExportExcel();
                    string _strExceltPath =  uctlBaseConfig.GetConfig("ExceltPath");
                    PublicVar.ExcelPath = _strExceltPath;
                    PublicVar.ExcelSource = PublicVar.ExportData.Tables[0];
                    break;
                case "XML":
                    ie = new FluenctExport(PublicVar.m_dsPatients);
                    break;
                default:
                    CommonFunction.WriteError("δ֪��������:" + _strExportType);
                    break;
            }
            ie.Export();
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
                    PublicVar.ExportParam[0] = p_dsOnePatInfo;
                    break;
                case "DBF":
                    string _strDbfPath = uctlBaseConfig.GetConfig("DbfPath");
                    //PublicVar.ExportParam[0] = _strDbfPath;
                    //PublicVar.ExportParam[1] = p_dsOnePatInfo;
                    ie = new ExportDBF(p_dsOnePatInfo.Tables[0]);
                    break;
                case "EXCLE":
                    ie = new ExportExcel();
                    string _strExceltPath = uctlBaseConfig.GetConfig("ExceltPath");
                    PublicVar.ExcelPath = _strExceltPath;
                    //PublicProperty.ExcelSource = p_dtOnePatInfo;
                    break;
                case "XML":
                    ie = new ExportXml(p_dsOnePatInfo, p_strObjectName, p_strPatientId, p_strVisitId);
                    //PublicVar.ExportParam[0] = p_dsOnePatInfo;
                    break;
                default:
                    CommonFunction.WriteError("δ֪��������:" + _strExportType);
                    break;
            }
            ie.Export();
            //Thread t = new Thread(new ThreadStart(ie.Export));
            //t.Start();
        }

    }
}
