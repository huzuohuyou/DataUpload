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

namespace DataExport
{
    class ExportXml : IExport,IValidate
    {

        #region IExport ��Ա

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

        public string m_strObjectName = string.Empty;
        public string m_strPatientId = string.Empty;
        public string m_strVisitId = string.Empty;
        public DataSet m_dsPatInfo = new DataSet();

        public ExportXml(DataSet p_dsPatInfo, string p_strObjectName, string p_strPatientId, string p_strVisitId)
        {
            m_strObjectName = p_strObjectName;
            m_strPatientId = p_strPatientId;
            m_strVisitId = p_strVisitId;
            m_dsPatInfo = p_dsPatInfo;
        }

        public ExportXml() { }

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
        /// ���Ԫ���滻
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
            else//û����Ϣ����Ϊ��
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
        /// ����xmlɾ��{}
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <returns></returns>
        public string ClearXml(string p_strXml)
        {
            return p_strXml.Replace("{", "").Replace("}", "");
        }

        /// <summary>
        /// ����Ԫ���滻
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
        /// ͨ��������ʽ�жϴ˶�xml�ַ����Ƿ�Ҫ��table
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
        /// ͨ�����жϴ�����Դ�Ƿ�󶨱�
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
        /// ��xml�ַ����е�[FILED_NAME]��滻Ϊ����Դ�еĶ�Ӧ��
        /// ����Դ���������PATIENT_ID,VISIT_ID
        /// 2015-11-04
        /// �⺣��
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
            if (SaveXML(_strXml, _strFiledName, "XmlOutPutPath"))
            {
                RemoteMessage.SendMessage("FILE_EXPORT_RESULT:".PadRight(50, '.') + "OK");
            }
            else
            {
                RemoteMessage.SendMessage("FILE_EXPORT_RESULT:".PadRight(50, '.') + "FALSE");
            }
        }

        /// <summary>
        /// ͨ��Ŀ�������ƻ�ȡxml�ű�
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
        /// ͨ��Ŀ�������ƻ�ȡxml�ű�
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
        /// ��xml�����ڸ�Ŀ¼ ��xml�ļ�����
        /// </summary>
        /// <param name="p_strXML">xml�ű�</param>
        /// <param name="p_strFileName">Ŀ��������</param>
        public static bool SaveXML(string p_strXML, string p_strFileName)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////д��<add>Ԫ�ص�Value
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
        /// ��xml�������õ�Ŀ¼���ڸ�Ŀ¼ ��xml�ļ�����
        /// </summary>
        /// <param name="p_strXML">xml�ı�</param>
        /// <param name="p_strFileName">�ļ���</param>
        /// <param name="p_strDirectory">���õ�·����</param>
        public bool SaveXML(string p_strXML, string p_strFileName, string p_strDirectory)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////д��<add>Ԫ�ص�Value
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
        /// ��xmlstring�л�ȡ�ֶ�
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
        /// ͨ��������ʽ��ȡxml�б�ע���ֶ�
        /// ��עʾ�� [field_name]
        /// </summary>
        /// <param name="p_strObject">��������</param>
        /// <returns>�ֶμ���</returns>
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

        #region IValidate ��Ա

        public string ValidateData(string p_strTableName)
        {
            ShowFixInfo.m_dtSource.Rows.Clear();
            string _strSQLValue = ExportDB.GetSQL(p_strTableName);
            List<string> _listField = GetFieldFromXmlObject(p_strTableName);
            string _strSQL = _strSQLValue.ToUpper().Trim().Replace("@PATIENT_ID", "1").Replace("@VISIT_ID", "1");
            DataTable _dtLocal = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtLocal == null)
            {
                throw new Exception("SQL�������" + _strSQL);
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
            return " ����[" + p_strTableName.PadRight(30,'*') + "]δ������Ŀ:" + _strUnCmpareItems;
           
        }

        public void ValidateAll(DataTable p_dt)
        {
            CommonFunction.WriteError("validate", "====================================================�����ָ���====================================================", false);
            string _strError = string.Empty;
            foreach (DataRow var in p_dt.Rows)
            {
                CommonFunction.WriteError("validate", "������:" + ValidateData(var["TABLE_NAME"].ToString()) + ShowFixInfo.m_strWarning + "��");
            }
        }

        #endregion

    }
}
