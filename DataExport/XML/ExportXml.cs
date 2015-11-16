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
            ReplaceValue(PublicProperty.ExportParam[0]);
        }

        public static DataTable GetObject(string p_strObjectName)
        {
            DataTable _dtTemp = new DataTable();
            _dtTemp.Columns.Add("CLASS");
            _dtTemp.Columns.Add("CHAPTER_NAME");
            List<string> _listField = GetFieldFromXml(p_strObjectName);
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

        /// <summary>
        /// ��xml�ַ����е�[FILED_NAME]��滻Ϊ����Դ�еĶ�Ӧ��
        /// ����Դ���������PATIENT_ID,VISIT_ID
        /// 2015-11-04
        /// �⺣��
        /// </summary>
        /// <param name="p_dsSoucre"></param>
        public void DoReplace(DataSet p_dsSoucre)
        {
            foreach (DataTable _dtSoucr in p_dsSoucre.Tables)
            {
                string _strXml = GetXML(_dtSoucr.TableName);
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
                    string _strFiledName = _drSource["PATIENT_ID"].ToString() + "_" + _drSource["VISIT_ID"].ToString() + "_" + _dtSoucr.TableName;
                    if (SaveXML(_strXml, _strFiledName, "XmlOutPutPath"))
                    {
                        RemoteMessage.SendMessage("FILE_EXPORT_RESULT:" + _strFiledName.PadRight(50, '.') + "OK");
                    }
                    else
                    {
                        RemoteMessage.SendMessage("FILE_EXPORT_RESULT:" + _strFiledName.PadRight(50, '.') + "FALSE");
                    }
                   
                }
            }
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
        public static bool SaveXML(string p_strXML, string p_strFileName, string p_strDirectory)
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
        /// ͨ��������ʽ��ȡxml�б�ע���ֶ�
        /// ��עʾ�� [field_name]
        /// </summary>
        /// <param name="p_strObject">��������</param>
        /// <returns>�ֶμ���</returns>
        public static List<string> GetFieldFromXml(string p_strObject)
        {
            List<string> _listField = new List<string>();
            Regex reg = new Regex(@"\[[^\[^\]]*\]");
            string _strXml = GetXML(p_strObject);
            int _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                _listField.Add(reg.Matches(_strXml)[i].Captures[0].Value);
            }
            return _listField;
        }

        #region IValidate ��Ա

        public string ValidateData(string p_strTableName)
        {
            ShowFixInfo.m_dtSource.Rows.Clear();
            string _strSQLValue = ExportDB.GetSQL(p_strTableName);
            List<string> _listField = GetFieldFromXml(p_strTableName);
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
