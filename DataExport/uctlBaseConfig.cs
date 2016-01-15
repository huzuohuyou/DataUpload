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
        /// 2015-01-05
        /// 吴海龙
        /// 保存配置信息
        /// </summary>
        /// <param name="p_strKey"></param>
        /// <param name="p_strValue"></param>
        public static void SaveConfig(string p_strKey,string p_strValue) {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //写入<add>元素的Value
            config.AppSettings.Settings[p_strKey].Value = p_strValue;
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
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
            string _strExportType = GetConfig("ExportType");
            string _strDbfPath = GetConfig("DbfPath");
            string _strDbfOutPutDir = GetConfig("DbfOutPutDir");
            string _strExcelOutPutDir = GetConfig("ExcelOutPutDir");
            string _strXmlPath = GetConfig("XmlOutPutPath");
            string _strUploadFlag = GetConfig("UploadFlag");
            string _strDBType = GetConfig("DBType");
            string _strTargetDBType = GetConfig("TargetDBType");
            string _strXmlOutputPath = GetConfig("XmlOutputPath");
            string _strTimeKind = GetConfig("TimeKind");
            string _strUseInterface = GetConfig("UseInterface");
            string _strUrl = GetConfig("WebServiceUrl");
            string _strUseAdapterSQL=GetConfig("UseAdapterSQL");
            string _strAdapterSQL = GetConfig("AdapterSQL");
            switch (_strExportType)
            {
                case m_strDB:
                    tabControl1.SelectedIndex = 0;
                    break;
                case m_strEXCEL:
                    tabControl1.SelectedIndex = 1;
                    break;
                case m_strDBF:
                    tabControl1.SelectedIndex = 2;
                    break;
                case m_strXML:
                    tabControl1.SelectedIndex = 3;
                    break;
                default:
                    CommonFunction.WriteError("未知关键字[DB,DBF,XML,EXCEL]");
                    break;
            }
            txt_dbf.Text = _strDbfPath;
            textBox5.Text = _strDbfOutPutDir;
            txt_xmlsavepath.Text = _strXmlOutputPath;
            txt_excel.Text = _strExcelOutPutDir;
            comboBox1.Text = _strDBType;
            comboBox2.Text = _strTargetDBType;
            comboBox3.Text = _strTimeKind;
            comboBox4.Text = _strUploadFlag;
            comboBox5.Text = _strExportType;
            comboBox6.Text = _strUseInterface;
            textBox4.Text = _strUrl;
            comboBox7.Text = _strUseAdapterSQL;
            richTextBox2.Text = _strAdapterSQL;
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
            if (comboBox7.Text=="TRUE"&&richTextBox2.Text == "")
            {
                MessageBox.Show("开启自动上传时请填写个性化SQL");
            }
            string _strExportType = comboBox5.Text;
            string _strDBType = comboBox1.Text;
            string _strUpLoad = comboBox4.Text;
            string _strTimeKind = comboBox3.Text;
            string _strUseInterface = comboBox6.Text;
            string _strUrl = textBox4.Text;
            string _strUseAdapterSQL = comboBox7.Text;
            string _strAdapterSQL = richTextBox2.Text;
            SaveConfig("TimeKind", _strTimeKind);
            SaveConfig("ExportType", _strExportType);
            SaveConfig("DBType", _strDBType);
            SaveConfig("UploadFlag", _strUpLoad);
            SaveConfig("UseInterface", _strUseInterface);
            SaveConfig("WebServiceUrl", _strUrl);
            SaveConfig("UseAdapterSQL", _strUseAdapterSQL);
            SaveConfig("AdapterSQL", _strAdapterSQL);
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
            WebServiceHelper.Test();
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
                comboBox2.Text = GetConfig("TARGETDBTYPE");
            }
            else if (comboBox5.Text == "DBF")
            {
                tabControl1.SelectedIndex = 2;
                txt_dbf.Text = GetConfig("DbfPath");
                textBox5.Text = GetConfig("DbfOutPutDir");
            }
            else if (comboBox5.Text == "XML")
            {
                 tabControl1.SelectedIndex = 3;
                 txt_xmlsavepath.Text = GetConfig("XmlOutPutPath"); 
            }
            else if (comboBox5.Text == "EXCEL")
            {
                 tabControl1.SelectedIndex = 1;
                 txt_excel.Text = txt_xmlsavepath.Text = GetConfig("ExcelOutPutDir"); 
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            InitDBEnviroment();
        }


        public void InitDBEnviroment()
        {
            string _strEnviroment = Application.StartupPath + "\\Enviroment.xml";
            DataSet _dsDBEnviroment = CommonFunction.ConvertXMLFileToDataSet(_strEnviroment);
            foreach (DataRow var in _dsDBEnviroment.Tables[0].Rows)
            {
                int _n = CommonFunction.OleExecuteNonQuery(var["sql"].ToString(), PublicVar.m_strEmrConnection);
                //if (1==_n)
                //{
                //    uctlMessageBox.frmDisappearShow("成功执行语句" + var["name"].ToString());
                //}
                //else
                //{
                //    uctlMessageBox.frmDisappearShow("语句执行失败请矫正Enviroment.xml");
                //}
            }
            uctlMessageBox.frmDisappearShow("语句执行完成");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd_path.ShowDialog();
            SaveConfig("DbfPath", ofd_path.FileName);
            txt_dbf.Text = GetConfig("DbfPath");
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            fbd_file.ShowDialog();
            SaveConfig("DbfOutPutDir", fbd_file.SelectedPath);
            textBox5.Text = GetConfig("DbfOutPutDir");
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请通过");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = GetConfig("TARGETDBTYPE");
            txt_dbf.Text = GetConfig("DbfPath");
            textBox5.Text = GetConfig("DbfOutPutDir");
            txt_xmlsavepath.Text = GetConfig("XmlOutPutPath");
            txt_excel.Text = txt_xmlsavepath.Text = GetConfig("ExcelPath");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text=="TRUE")
            {
                comboBox7.Text = "TRUE";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fbd_file.ShowDialog();
            SaveConfig("ExcelOutPutDir", fbd_file.SelectedPath);
            txt_excel.Text = GetConfig("ExcelOutPutDir");
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }


        //public static Dictionary<string, string> GetConfig() {
        //    Dictionary<string, string> _dict = new Dictionary<string, string>();
        //    _dict.Add("exporttype",GetConfig("ExportType"));
        //}
    }
}
