using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ConfirmFileName;
using ToolFunction;
using DataExport.外部接口;
using DataExport.文件接口;

namespace DataExport
{
    class ExportXml : IExport,IValidate
    {

   

        public string m_strObjectName = string.Empty;
        public string m_strPatientId = string.Empty;
        public string m_strVisitId = string.Empty;
        public string m_strFileNo = string.Empty;
        public DataSet m_dsPatInfo = new DataSet();

        public ExportXml() { }

        public ExportXml(DataSet p_dsPatInfo, string p_strObjectName, string p_strPatientId, string p_strVisitId)
        {
            m_strObjectName = p_strObjectName;
            m_strPatientId = p_strPatientId;
            m_strVisitId = p_strVisitId;
            m_dsPatInfo = p_dsPatInfo;
        }

        

        #region IExport 成员

        public void Export()
        {
            //ReplaceValue(PublicProperty.ExportParam[0]);
            DoReplace(m_dsPatInfo);
        }

        public static DataTable GetObject(string p_strObjectName)
        {
            DataTable _dtTemp = new DataTable();
            _dtTemp.Columns.Add("CLASS");
            _dtTemp.Columns.Add("CHAPTER_NAME");
            List<string> _listField = GetFieldFromXmlObject(p_strObjectName);
            foreach (string var in _listField)
            {
                _dtTemp.Rows.Add("FILE", var);
            }
            return _dtTemp;
        }

        #endregion

        public void ReplaceValue(object o)
        {
            DataSet ds = (DataSet)o;
            DoReplace(ds);
        }

        public string GetFixXml(string p_strXml) 
        {
            return p_strXml.Replace("TABLE_", "");
        }

        /// <summary>
        /// 表格元素替换
        /// </summary>
        /// <param name="p_strOldValue"></param>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public string DoTableReplace( string p_strXml, DataTable p_dtSource)
        {

            DataTable _dtSoucr = p_dtSource;
            string _strNewXml = string.Empty;
            string _strSimpleXml = GetFixXml(_dtSoucr.TableName);
            if (p_dtSource.Rows.Count > 0)
            {
                foreach (DataRow _drSource in _dtSoucr.Rows)
                {
                    string _strMultiXml = GetFixXml(_dtSoucr.TableName);
                    foreach (DataColumn _dcSource in _dtSoucr.Columns)
                    {
                        string _strOldValue = _dcSource.Caption ;
                        string _strNewValue = _drSource[_dcSource].ToString();
                        if (_strMultiXml.IndexOf(_strOldValue) > 0)
                        {
                            _strMultiXml = _strMultiXml.Replace(_strOldValue, _strNewValue);
                        }
                    }
                    _strNewXml += _strMultiXml;
                }
                _strNewXml = ClearXml(_strNewXml);
            }
            else//没有信息内容为空
            {
                _strNewXml = "";
            }
            if (p_strXml.IndexOf(_strSimpleXml) > 0)
            {
                p_strXml = p_strXml.Replace(_strSimpleXml, _strNewXml);
            }
            //p_strXml = ClearXml(p_strXml);
            return p_strXml;
        }

        /// <summary>
        /// 清理xml删除{}
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <returns></returns>
        public string ClearXml(string p_strXml)
        {
            return p_strXml.Replace("{", "").Replace("}", "");
        }

        /// <summary>
        /// 单个元素替换
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public string DoSimpleReplace(string p_strXml, DataTable p_dtSource)
        {
            DataTable _dtSoucr = p_dtSource;
            string _strXml = p_strXml;
            foreach (DataRow _drSource in _dtSoucr.Rows)
            {
                foreach (DataColumn _dcSource in _dtSoucr.Columns)
                {
                    string _strOldValue = _dcSource.Caption;
                    string _strNewValue = _drSource[_dcSource].ToString();
                    if (_strXml.IndexOf(_strOldValue) > 0)
                    {
                        _strXml = _strXml.Replace(_strOldValue, _strNewValue);
                    }

                }
               
            }
            return _strXml;
        }

        /// <summary>
        /// 通过正则表达式判断此段xml字符串是否要绑定table
        /// </summary>
        /// <param name="p_strValue"></param>
        /// <returns></returns>
        public bool IsTableSource(string p_strValue)
        {
            Regex reg = new Regex(@"\{[^\{^\}]*\}");
            //string _strXml = GetXML(p_strObject);
            int _nCount = reg.Matches(p_strValue).Count;
            if (_nCount > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 通过表判断此数据源是否绑定表
        /// </summary>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public bool IsTableSource(DataTable p_dtSource)
        {
            if (p_dtSource.TableName.Contains("TABLE"))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 将xml字符串中的[FILED_NAME]项，替换为数据源中的对应项
        /// 数据源必须包含列PATIENT_ID,VISIT_ID
        /// 2015-11-04
        /// 吴海龙
        /// </summary>
        /// <param name="p_dsSoucre"></param>
        public void DoReplace(DataSet p_dsSoucre)
        {
            if (p_dsSoucre == null)
            {
                return;
            }
            else
            {
                if (p_dsSoucre.Tables.Count == 0)
                {
                    return;
                }
            }
            string _strXml = GetXML(p_dsSoucre);
            StringBuilder _sbXml = new StringBuilder(_strXml);
            foreach (DataTable _dtSoucr in p_dsSoucre.Tables)
            {
                if (IsTableSource(_dtSoucr))
                {
                    _strXml = DoTableReplace(_strXml, _dtSoucr);
                }
                else
                {
                    _strXml = DoSimpleReplace(_strXml, _dtSoucr);
                }
            }
            string _strFiledName = m_strObjectName + "_" + m_strPatientId + "_" + m_strVisitId;
            if (EmrInfoManagement.m_bHasFile)
            {
                if (IsUseInterface())
                {
                    UploadInterface ui = new UploadInterface();
                    ui.CallInterface(_strXml, _strFiledName);
                }
                else
                {
                    if (SaveXML(_strXml, _strFiledName, "XmlOutPutPath"))
                    {
                        RemoteMessage.SendMessage("FILE_EXPORT_RESULT:".PadRight(50, '.') + "OK");
                        uctlRestoreManage.RemoveRecord(m_strObjectName, m_strPatientId, m_strVisitId);
                    }
                    else
                    {
                        RemoteMessage.SendMessage("FILE_EXPORT_RESULT:".PadRight(50, '.') + "FALSE");
                        uctlRestoreManage.LogFalsePatient(m_strObjectName, m_strPatientId, m_strVisitId);
                    }
                }
            }
        }

        public bool IsUseInterface()
        {
            string _strUseInterface = uctlBaseConfig.GetConfig("UseInterface");
            if ("TRUE" == _strUseInterface.ToUpper())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 通过目标表的名称获取xml脚本
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string GetXML(DataSet p_dsSource)
        {
            string _strFileName = string.Empty;
            foreach (DataTable var in p_dsSource.Tables)
            {
                if (var.TableName!="TABLE")
                {
                    _strFileName = var.TableName;
                }
            }
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/xml/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + _strFileName + ".xml";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath, Encoding.UTF8))
                    {
                        _strSQL = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
                //return string.Empty;
            }
            return _strSQL;
        }

        /// <summary>
        /// 通过目标表的名称获取xml脚本
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string GetXML(string p_strFileName)
        {
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/xml/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath, Encoding.UTF8))
                    {
                        _strSQL = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
                //return string.Empty;
            }
            return _strSQL;
        }

        /// <summary>
        /// 将xml保存在根目录 的xml文件加下
        /// </summary>
        /// <param name="p_strXML">xml脚本</param>
        /// <param name="p_strFileName">目标表的名称</param>
        public static bool SaveXML(string p_strXML, string p_strFileName)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////写入<add>元素的Value
            string _strPath = Application.StartupPath+"\\xml\\";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                using (StreamWriter sw = new StreamWriter(_strPath, false, Encoding.UTF8))
                {
                    sw.WriteLine(p_strXML);
                    return true;
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                SaveXML(p_strXML, p_strFileName);
            }
            return false;
        }

        /// <summary>
        /// 将xml保存配置的目录下在根目录 的xml文件加下
        /// </summary>
        /// <param name="p_strXML">xml文本</param>
        /// <param name="p_strFileName">文件名</param>
        /// <param name="p_strDirectory">配置的路径名</param>
        public bool SaveXML(string p_strXML, string p_strFileName, string p_strDirectory)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////写入<add>元素的Value
            string _strPath = config.AppSettings.Settings[p_strDirectory].Value + "\\";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                using (StreamWriter sw = new StreamWriter(_strPath,false,Encoding.UTF8))
                {
                    sw.WriteLine(p_strXML);
                    return true;
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                SaveXML(p_strXML, p_strFileName);
            }
            return false;
        }

        /// <summary>
        /// 从xmlstring中获取字段
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <returns></returns>
        public static List<string> GetFiledFromXml(string p_strXml)
        {
            List<string> _listField = new List<string>();
            Regex reg = new Regex(@"\[[^\[^\]]*\]");
            string _strXml = p_strXml;
            int _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                string _strField = reg.Matches(_strXml)[i].Captures[0].Value;
                _listField.Add(_strField);
            }
            return _listField;
        }

        /// <summary>
        /// 通过正则表达式获取xml中标注的字段
        /// 标注示例 [field_name]
        /// </summary>
        /// <param name="p_strObject">对象名称</param>
        /// <returns>字段集合</returns>
        public static List<string> GetFieldFromXmlObject(string p_strObject)
        {
            List<string> _listField = new List<string>();
            Regex reg = new Regex(@"\[[^\[^\]]*\]");
            string _strXml = GetXML(p_strObject);
            int _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                string _strField = reg.Matches(_strXml)[i].Captures[0].Value;
                if (_strField.ToUpper().StartsWith("[COL"))
                {
                    continue;
                }
                _listField.Add(_strField);
            }
            reg = new Regex(@"\{[^\{^\}]*\}");
            //string _strXml = GetXML(p_strObject);
            _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                string _strValue = reg.Matches(_strXml)[i].Captures[0].Value;
                //if (_strValue.g)
                //{
                    
                //}
                _listField.Add(_strValue);
            }
            return _listField;
        }

        #region IValidate 成员

        public string ValidateData(string p_strTableName)
        {
            ShowFixInfo.m_dtSource.Rows.Clear();
            string _strSQLValue = ExportDB.GetSQL(p_strTableName);
            List<string> _listField = GetFieldFromXmlObject(p_strTableName);
            string _strSQL = _strSQLValue.ToUpper().Trim().Replace("@PATIENT_ID", "1").Replace("@VISIT_ID", "1");
            DataTable _dtLocal = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtLocal == null)
            {
                throw new Exception("SQL语句有误" + _strSQL);
            }
            if (!_dtLocal.Columns.Contains("PATIENT_ID") || !_dtLocal.Columns.Contains("VISIT_ID"))
            {
                ShowFixInfo.m_strWarning = (" [PATIENT_ID][VISIT_ID] ");
            }
            string _strUnCmpareItems = string.Empty;
            foreach (string var in _listField)
            {
                string _strField = var.Replace("[", "").Replace("]","");
                if (_dtLocal.Columns.Contains(_strField))
                {
                    ShowFixInfo.m_dtSource.Rows.Add(_strField, "TRUE");
                }
                else
                {
                    ShowFixInfo.m_dtSource.Rows.Add(_strField, "FALSE");
                    _strUnCmpareItems += "[" + _strField + "]";
                }
            }
            return " 对象[" + p_strTableName.PadRight(30,'*') + "]未对照条目:" + _strUnCmpareItems;
           
        }

        public void ValidateAll(DataTable p_dt)
        {
            CommonFunction.WriteError("validate", "====================================================华丽分割线====================================================", false);
            string _strError = string.Empty;
            foreach (DataRow var in p_dt.Rows)
            {
                CommonFunction.WriteError("validate", "【警告:" + ValidateData(var["TABLE_NAME"].ToString()) + ShowFixInfo.m_strWarning + "】");
            }
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
