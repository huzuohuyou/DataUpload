using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using JHEMR.EmrSysDAL;
using ToolFunction;
using System.IO;
using System.Windows.Forms;
namespace DataExport
{
    public class AsynXmlExport
    {
        public string m_strXml = string.Empty;
        /// <summary>
        /// 通过目标表的名称获取xml脚本
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public string GetXML(string p_strFileName)
        {
            string _strXML = string.Empty;
            string _strPath = Application.StartupPath + "/xml/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath, Encoding.UTF8))
                    {
                        _strXML = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
                //return string.Empty;
            }
            return _strXML;
        }


       

    }
}
