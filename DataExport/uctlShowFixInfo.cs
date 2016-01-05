using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using DataExport;
using ToolFunction;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace ConfirmFileName
{
    public partial class ShowFixInfo : UserControl
    {
        public static DataTable m_dtSource = new DataTable();
        public IValidate m_ivalidate = null;
        public static string m_strWarning = string.Empty;

        public ShowFixInfo(string p_strTableName,string p_strMS)
        {
            if (!m_dtSource.Columns.Contains("TARGET"))
            {
                m_dtSource.Columns.Add("TARGET");
            }
            if (!m_dtSource.Columns.Contains("FIT"))
            {
                m_dtSource.Columns.Add("FIT");
            }
            InitializeComponent();
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            switch (_strExportType) {
                case "DB":
                    m_ivalidate = new ExportDB();
                    break;
                case "XML":
                    m_ivalidate = new ExportXml();
                    break;
                case "DBF":
                    break;
                case "EXCEL":
                    break;
            }
            m_ivalidate.ValidateData(p_strTableName);
            dataGridView2.DataSource = m_dtSource.DefaultView;
            textBox2.Text = m_strWarning;
        }


        public ShowFixInfo(DataTable p_dtSource)
        {
            if (!m_dtSource.Columns.Contains("TARGET"))
            {
                m_dtSource.Columns.Add("TARGET");
            }
            if (!m_dtSource.Columns.Contains("FIT"))
            {
                m_dtSource.Columns.Add("FIT");
            }
            InitializeComponent();
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            switch (_strExportType)
            {
                case "DB":
                    m_ivalidate = new ExportDB();
                    break;
                case "XML":
                    m_ivalidate = new ExportXml();
                    break;
                case "DBF":
                    break;
                case "EXCEL":
                    break;
            }
            m_ivalidate.ValidateAll(p_dtSource);
            string _strFile = Application.StartupPath + "\\error\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\validate.error";
            Process.Start("notepad.exe",_strFile);
        }


        /// <summary>
        /// 通过sql名获取文件
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public string GetSQL(string p_strFileName)
        {
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/SQL/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName+".sql";
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
        /// 将sql保存到文件中
        /// </summary>
        /// <param name="p_strSQL">sql</param>
        /// <param name="p_strFileName">sqlName</param>
        public void SaveSQL(string p_strSQL, string p_strFileName)
        {
            string _strPath = Application.StartupPath + "/SQL/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName+".sql";
                using (StreamWriter sw = new StreamWriter(_strPath, false))
                {
                    sw.WriteLine(p_strSQL);
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                SaveSQL(p_strSQL, p_strFileName);
            }
        }


       
    }
}
