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
        private MainForm mf = null;

        public uctlBaseConfig()
        {
            InitializeComponent();
            InitData();
        }

        public uctlBaseConfig(MainForm p_form)
        {
            InitializeComponent();
            InitData();
            mf = p_form;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings.GetValues("123");

        }

        /// <summary>
        /// 获取节点设置
        /// </summary>
        /// <param name="m_strElementName"></param>
        /// <returns></returns>
        public static string GetConfig(string m_strElementName)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strValue = string.Empty;
            ////根据Key读取<add>元素的Value
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
            string _strTimeKind = string.Empty;
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
            _strTimeKind = config.AppSettings.Settings["TimeKind"].Value;
            switch (_strExportType)
            {
                case m_strDB:
                    //rb_db.Checked = true;
                    tabControl1.SelectedIndex = 0;
                    //txt_value.Text = _strTargetConnectionString;
                    break;
                case m_strEXCEL:
                    //rb_excel.Checked = true;
                    tabControl1.SelectedIndex = 1;
                    txt_excel.Text = _strExcelPath;
                    break;
                case m_strDBF:
                    //rb_dbf.Checked = true;
                    tabControl1.SelectedIndex = 2;
                    txt_dbf.Text = _strDbfPath;
                    break;
                case m_strXML:
                    //rb_xml.Checked = true;
                    tabControl1.SelectedIndex = 3;
                    txt_xmlsavepath.Text = _strXmlOutputPath;
                    break;
                default:
                    throw new Exception("未知关键字[DB,DBF,XML,EXCEL]");
                    break;
            }
            //if (_strUploadFlag.ToUpper() =="TRUE")
            //{
            //    checkBox1.Checked = true;
            //}
            //else
            //{
            //    checkBox1.Checked = false;
            //}
            comboBox1.Text = _strDBType;
            comboBox2.Text = _strTargetDBType;
            comboBox3.Text = _strTimeKind;
            comboBox4.Text = _strUploadFlag;
            comboBox5.Text = _strExportType;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////写入<add>元素的Value
            //config.AppSettings.Settings["TargetConnectionString"].Value = txt_value.Text;
            ////根据Key读取<add>元素的Value
            //string name = config.AppSettings.Settings["name"].Value;
           
            ////增加<add>元素
            //config.AppSettings.Settings.Add("url", "http://www.xieyc.com");
            //删除<add>元素
            //config.AppSettings.Settings.Remove("name");
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strExportType = comboBox5.Text;
            string _strDBType = comboBox1.Text;
            string _strUpLoad = comboBox4.Text;
            string _strTimeKind = comboBox3.Text;
            config.AppSettings.Settings["TimeKind"].Value = _strTimeKind;
            config.AppSettings.Settings["ExportType"].Value = _strExportType;
            config.AppSettings.Settings["DBType"].Value = _strDBType;
            config.AppSettings.Settings["UploadFlag"].Value = _strUpLoad;
            config.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            mf.SetBaseInfo();
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
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string _strXmlSavePath = txt_xmlsavepath.Text;
            config.AppSettings.Settings["XmlOutputPath"].Value = _strXmlSavePath;
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
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

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "DB")
            {
                tabControl1.SelectedIndex = 0;
            }
            else if (comboBox5.Text == "DBF")
            {
                tabControl1.SelectedIndex = 2;
            }
            else if (comboBox5.Text == "XML")
            {
                tabControl1.SelectedIndex = 3;
            }
            else if (comboBox5.Text == "EXCEL")
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        //public static Dictionary<string, string> GetConfig() {
        //    Dictionary<string, string> _dict = new Dictionary<string, string>();
        //    _dict.Add("exporttype",GetConfig("ExportType"));
        //}
    }
}
