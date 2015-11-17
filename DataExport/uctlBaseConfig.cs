using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using System.Configuration;
using System.Diagnostics;
using ToolFunction;
using System.IO;

namespace DataExport
{
    public partial class uctlBaseConfig : UserControl
    {
        private const string m_strDB = "DB";
        private const string m_strDBF = "DBF";
        private const string m_strXML = "XML";
        private const string m_strEXCEL = "EXCEL";
        public uctlBaseConfig()
        {
            InitializeComponent();
            InitData();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings.GetValues("123");

        }

        /// <summary>
        /// ��ȡ�ڵ�����
        /// </summary>
        /// <param name="m_strElementName"></param>
        /// <returns></returns>
        public static string GetConfig(string m_strElementName)
        {
            //��ȡConfiguration����
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strValue = string.Empty;
            ////����Key��ȡ<add>Ԫ�ص�Value
            try
            {
                 _strValue = config.AppSettings.Settings[m_strElementName].Value;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return _strValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strKey"></param>
        /// <param name="p_strValue"></param>
        public void InitValue(string p_strKey, string p_strValue)
        {
            bool _boExist = false;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string[] _arrKey = config.AppSettings.Settings.AllKeys;
            foreach (string var in _arrKey)
            {
                if (var == p_strKey)
                {
                    //config.AppSettings.Settings[p_strKey].Value = p_strValue;
                    _boExist = true;
                }
            }
            if (!_boExist)
            {
                config.AppSettings.Settings.Add(p_strKey, p_strValue);
            }
            config.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        public void InitData()
        {
            string _strExportType = string.Empty;
            string _strDbfPath = string.Empty;
            string _strExcelPath = string.Empty;
            string _strXmlPath = string.Empty;
            string _strTargetConnectionString = string.Empty;
            string _strUploadFlag = string.Empty;
            string _strDBType = string.Empty;
            string _strTargetDBType = string.Empty;
            string _strXmlOutputPath = string.Empty;
            InitValue("ExportType", "DB");
            InitValue("XmlOutPutPath", "E:\\");
            InitValue("DbfPath", "123");
            InitValue("ExcelPath", "123");
            InitValue("XmlPath", "123");
            InitValue("UploadFlag", "False");
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _strExportType = config.AppSettings.Settings["ExportType"].Value;
            _strDBType = config.AppSettings.Settings["DBType"].Value;
            _strTargetDBType = config.AppSettings.Settings["TARGETDBTYPE"].Value;
            _strDbfPath = config.AppSettings.Settings["DbfPath"].Value;
            _strExcelPath = config.AppSettings.Settings["ExcelPath"].Value;
            _strXmlPath = config.AppSettings.Settings["XmlPath"].Value;
            _strUploadFlag = config.AppSettings.Settings["UploadFlag"].Value;
            _strXmlOutputPath = config.AppSettings.Settings["XmlOutputPath"].Value;
            switch (_strExportType)
            {
                case m_strDB:
                    rb_db.Checked = true;
                    tabControl1.SelectedIndex = 0;
                    //txt_value.Text = _strTargetConnectionString;
                    break;
                case m_strEXCEL:
                    rb_excel.Checked = true;
                    tabControl1.SelectedIndex = 1;
                    txt_excel.Text = _strExcelPath;
                    break;
                case m_strDBF:
                    rb_dbf.Checked = true;
                    tabControl1.SelectedIndex = 2;
                    txt_dbf.Text = _strDbfPath;
                    break;
                case m_strXML:
                    rb_xml.Checked = true;
                    tabControl1.SelectedIndex = 3;
                    txt_xmlsavepath.Text = _strXmlOutputPath;
                    break;
                default:
                    throw new Exception("δ֪�ؼ���[DB,DBF,XML,EXCEL]");
                    break;
            }
            if (_strUploadFlag.ToUpper() =="TRUE")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            comboBox1.Text = _strDBType;
            comboBox2.Text = _strTargetDBType;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //��ȡConfiguration����
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////д��<add>Ԫ�ص�Value
            //config.AppSettings.Settings["TargetConnectionString"].Value = txt_value.Text;
            ////����Key��ȡ<add>Ԫ�ص�Value
            //string name = config.AppSettings.Settings["name"].Value;
           
            ////����<add>Ԫ��
            //config.AppSettings.Settings.Add("url", "http://www.xieyc.com");
            //ɾ��<add>Ԫ��
            //config.AppSettings.Settings.Remove("name");
            //һ��Ҫ�ǵñ��棬д����������config.Save()Ҳ����
            config.Save(ConfigurationSaveMode.Modified);
            //ˢ�£���������ȡ�Ļ���֮ǰ��ֵ��������װ���ڴ棩
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string _strExportType = string.Empty;
            string _strDBType = string.Empty;
            _strDBType = comboBox1.Text;
            if (rb_db.Checked)
            {
                _strExportType = rb_db.Tag.ToString();
            }
            if (rb_dbf.Checked)
            {
                _strExportType = rb_dbf.Tag.ToString();
            }
            if (rb_excel.Checked)
            {
                _strExportType = rb_excel.Tag.ToString();
            }
            if (rb_xml.Checked)
            {
                _strExportType = rb_xml.Tag.ToString();
            }
            //��ȡConfiguration����
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////д��<add>Ԫ�ص�Value
            config.AppSettings.Settings["ExportType"].Value = _strExportType;
            config.AppSettings.Settings["DBType"].Value = _strDBType;
            if (checkBox1.Checked)
            {
                config.AppSettings.Settings["UploadFlag"].Value = "True";
            }
            else
            {
                config.AppSettings.Settings["UploadFlag"].Value = "False";
            }
            config.Save(ConfigurationSaveMode.Modified);
            //ˢ�£���������ȡ�Ļ���֮ǰ��ֵ��������װ���ڴ棩
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process Pnotepad = new Process();
            Pnotepad.StartInfo.FileName = Application.StartupPath + "\\DataExport.exe.config";
            Pnotepad.Start();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            EmrInfo eif = new EmrInfo();
            RTRESULT.Text = eif.GetMrInfo(TXTPID.Text, int.Parse(TXTVID.Text), TXTNAME.Text);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string _strTARGETDBType = string.Empty;
            _strTARGETDBType = comboBox2.Text;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["TARGETDBTYPE"].Value = _strTARGETDBType;
            config.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //LogHelper.WriteLog(typeof(uctlBaseConfig), "123");
            //LogHelper.WriteLog(typeof(uctlBaseConfig), new Exception("123123"));
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            CommonFunction.WriteError("123","123123");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            fbd_file.ShowDialog();
            txt_xmlsavepath.Text = fbd_file.SelectedPath.ToString();
            //��ȡConfiguration����
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strXmlSavePath = txt_xmlsavepath.Text;
            config.AppSettings.Settings["XmlOutputPath"].Value = _strXmlSavePath;
            config.Save(ConfigurationSaveMode.Modified);
            //ˢ�£���������ȡ�Ļ���֮ǰ��ֵ��������װ���ڴ棩
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string _strExePath = Application.StartupPath + @"\MessagePlatform.exe";
            Process.Start(_strExePath);
            RemoteMessage.InitClient();
            richTextBox1.Text = GrabInfo.GrabPatientInfoFromFile(textBox3.Text, textBox1.Text, textBox2.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Directory.CreateDirectory(Application.StartupPath + "file");
            //Directory.CreateDirectory(Application.StartupPath + "xml");
            //Directory.CreateDirectory(Application.StartupPath + "log");
            //Directory.CreateDirectory(Application.StartupPath + "error");
        }

        private void uctlBaseConfig_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Application.StartupPath + "//file");
            Directory.CreateDirectory(Application.StartupPath + "//xml");
            Directory.CreateDirectory(Application.StartupPath + "//log");
            Directory.CreateDirectory(Application.StartupPath + "//error");
        }

        //public static Dictionary<string, string> GetConfig() {
        //    Dictionary<string, string> _dict = new Dictionary<string, string>();
        //    _dict.Add("exporttype",GetConfig("ExportType"));
        //}
    }
}