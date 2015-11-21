using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ToolFunction;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using DataExport.文件接口;
using System.Text.RegularExpressions;

namespace DataExport
{
    /// <summary>
    /// 以章节的方式进行数据榨取
    /// 2015-11-06 
    /// 吴海龙
    /// </summary>
    public class GrabInfo:IExport
    {
        public string m_strClass = string.Empty;
        public string m_strTableName = string.Empty;
        public string m_strChapterName = string.Empty;
        public string m_strDataDatail = string.Empty;
        public static DataSet m_dsPatDBInfo = new DataSet();

        #region IExport 成员

        public void Export()
        {
            //PublicProperty.ExportParam[0];
        }

        #endregion

        /// <summary>
        /// 获取章节详情
        /// 2015-11-06
        /// 吴海龙
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
        /// 初始化上传数据库信息
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strVisitId"></param>
        public static void InitPatDBInfo(string p_strPatientId, string p_strVisitId)
        {
            m_dsPatDBInfo.Tables.Clear();
            DataSet _dsObj = DBTemplet.GetSQL();
            foreach (DataRow var in _dsObj.Tables[0].Rows)
            {
                string _strName = var["NAME"].ToString().ToUpper();
                string _strSQL = var["SQL"].ToString().ToUpper();
                _strSQL = string.Format(_strSQL.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId));
                DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, _strName, PublicProperty.m_strEmrConnection);
                m_dsPatDBInfo.Tables.Add(_dtTemp.Copy());
            }
        }

        /// <summary>
        /// 获取dataset中过滤的表
        /// 没有返回null
        /// 2015-11-21今天是周日
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        public static DataTable GetPatTable(string p_strTableName)
        {
            if (m_dsPatDBInfo.Tables.Contains(p_strTableName))
            {
                return m_dsPatDBInfo.Tables[p_strTableName];
            }
            RemoteMessage.SendMessage("数据集中无所描述表" + p_strTableName);
            return null;
        }

        /// <summary>
        /// 获取表中字段值
        /// </summary>
        /// <param name="p_dtTable"></param>
        /// <param name="p_strName"></param>
        /// <returns></returns>
        public static string GetPatItemInfo(DataTable p_dtTable, string p_strName)
        {
            if (p_dtTable != null && p_dtTable.Rows.Count == 1)
            {
                return p_dtTable.Rows[0][p_strName].ToString();
            }
            RemoteMessage.SendMessage("表为空或行不为1");
            return "";
        }

        /// <summary>
        /// 获取章节描述
        /// 用于获取字段获取数据方式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetChapterDescription(string p_strObjectName)
        {
            string _strSQL = string.Format(@"select * from pt_chapter_dict where table_name = '{0}' ", p_strObjectName);
            return CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
        }

        /// <summary>
        /// 返回字章节的表
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
        /// 获取组合章节详情
        /// 2015-11-06
        /// 吴海龙
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
        /// 获取单行章节
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
        /// 获取对象字段
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEnabledObject()
        {
            string _strSQL = string.Format(@"select * from pt_tables_dict where exportflag = 'TRUE'");
            return CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
        }

        /// <summary>
        /// 生成对象对应的DataTable
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <returns></returns>
        public static DataSet GetObjectLayOut(string p_strObjectName)
        {
            DataSet _dsObject = GetChapterDetail(p_strObjectName);
            return _dsObject;
        }

        /// <summary>
        /// 从数据库抓取数据的sql
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
        /// 从数据库获取数据
        /// 查询的数据为一行一些的数据
        /// 假如语句查询出来的为多行则返回空
        /// </summary>
        /// <returns></returns>
        public static string GrabPatientInfoFromDB(string p_strChapterDetail, string p_strPatientId, string p_strVisitId)
        {
            string _strTableName = GetTableName(p_strChapterDetail);
            string _strFieldName = GetFieldName(p_strChapterDetail);
            DataTable _dtDB = GetPatTable(_strTableName);
            string _strValue = GetPatItemInfo(_dtDB, _strFieldName);
            return _strValue;
           // string _strSQL = string.Empty;
           // string[] _arrDataDetail = p_strChapterDetail.Split('|');
           // _strSQL="SELECT "
           //_strChapterDetail.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId);
           // DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
           // if (_dtTemp == null)
           // {
           //     return "";
           // }
           // else if (_dtTemp.Rows.Count == 1)
           // {
           //     return _dtTemp.Rows[0][0].ToString();
           // }
           // return string.Empty;
        }

        /// <summary>
        /// 从模板抓取数据
        /// 2015-11-09
        /// 吴海龙
        /// </summary>
        /// <param name="p_strPatientId">病人id</param>
        /// <param name="p_strVisitId">住院次</param>
        /// <param name="p_strDataDatail">参数描述字符串包含文件类型[AB,AC]|文件名|元素名</param>
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
                if ("层次号" == _strFileType)
                {
                    _strResult = EmrInfoManagement.GetCCFileInfo(p_strPatientId, p_strVisitId, _strFileName, _strElementName);
                }
                else if ("元素" == _strFileType)
                {
                    _strResult = EmrInfoManagement.GetYSFileInfo(p_strPatientId, p_strVisitId, _strFileName, _strElementName);
                }
                else
                {
                    CommonFunction.WriteError("未知文件类型:" + _strFileType);
                }
            }
            else
            {
                CommonFunction.WriteError("参数输入错误:" + p_strDataDatail);
            }
            return _strResult;
        }

        /// <summary>
        /// 获取表数据从数据库
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
        /// 获取表样式病人信息
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
        /// 获取单行病人信息
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
                //数据转换2015-09-09 吴海龙
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
        /// 获取单个病人指定对象数据
        /// </summary>
        /// <param name="p_strObject">对象名</param>
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
        /// 获取由sql查询到的病人信息
        /// </summary>
        /// <param name="dql">sql集</param>
        /// <returns>病人信息</returns>
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
                    RemoteMessage.SendMessage("查询病人信息\n" + _strSQL);
                }
                DataTable _dt = _dtTemp.Copy();
                _dt.TableName = dr["TABLE_NAME"].ToString();
                PublicProperty.ExportData.Tables.Add(_dt);
            }
            //数据转换2015-09-09 吴海龙
            ConversionData.ExchangeData();
            ExeExport();
        }

        /// <summary>
        /// 获取平台配置的sql
        /// </summary>
        /// <returns>返回sql列表</returns>
        public static DataTable GetConfigSQL()
        {
            string _strSQL = string.Format("select table_name,MS,'' sql from pt_tables_dict b where b.exportflag ='TRUE'");
            RemoteMessage.SendMessage("正在获取配置SQL:" + _strSQL);
            DataTable _dtSQL = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            foreach (DataRow var in _dtSQL.Rows)
            {
                var["sql"] = GetSQL(var["table_name"].ToString());
            }
            return _dtSQL;
        }

        /// <summary>
        /// 通过sql名获取文件
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
        /// 执行导出
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
                    CommonFunction.WriteError("未知导出类型:" + _strExportType);
                    break;
            }
            Thread t = new Thread(new ThreadStart(ie.Export));
            t.Start();
        }


        /// <summary>
        /// 执行导出
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
                    CommonFunction.WriteError("未知导出类型:" + _strExportType);
                    break;
            }
            Thread t = new Thread(new ThreadStart(ie.Export));
            t.Start();
        }

    }
}
