using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Configuration;
using System.Threading;
using System.Data.OleDb;
using System.IO;
using System.Xml;

namespace ToolFunction
{
    public class CommonFunction
    {
        #region ����

        //public static 
        /// <summary>
        /// Excel �汾
        /// </summary>
        public enum ExcelType
        {
            Excel2003, Excel2007
        }
        /// <summary>
        /// IMEX ����ģʽ��
        /// IMEX������������������ʹ��Excel�ļ���ģʽ����ֵ��0��1��2���֣��ֱ�����������롢���ģʽ��
        /// </summary>
        public enum IMEXType
        {
            ExportMode = 0, ImportMode = 1, LinkedMode = 2
        }
        private DataSet myDs = new DataSet();
        private static string excelstring = "Provider=Microsoft.Ace.OleDb.12.0;data source='{0}';Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
        private Thread t = null;
        public static string ErrorLogPath = null;
        public static Dictionary<string, TabPage> dicpage = new Dictionary<string, TabPage>();
        /// <summary>
        /// ��Ϣ��ջ�����ļ�ռ��ʱд���ջ�ȴ��ļ��Ӵ�ռ�ã�����ջ����д���ļ�
        /// </summary>
        public static Stack<string> m_stackMessage = new Stack<string>();
        public static string m_strAlterFilePath = string.Empty;
        /// <summary>
        /// �����ڴ�
        /// </summary>
        public ShareMem m_MemDB = new ShareMem();
        /// <summary>
        /// ���ýṹ�������ջ���Ա����л�
        /// 2015-09-01
        /// </summary>
        [Serializable]
        public struct MessageStructure
        {
            public Stack<string> m_struMessage; //����m_struMessage

            /// <summary>
            /// ���캯��
            /// </summary>
            /// <param name="paraA"></param>
            /// <param name="paraB"></param>
            /// <param name="paraC"></param>
            public MessageStructure(Stack<string> paraA)
            {
                this.m_struMessage = paraA;
            }

        }
        #endregion

        #region ���캯��
        public CommonFunction()
        {

        }
        #endregion

        #region ��������������string����
        //        public static strng SetKey()
        //        {
        //            DateTime currentTime = DateTime.Now;
        //            string strtime = currentTime.Minute.ToString() + ":"
        //                              + currentTime.Second.ToString() + ":"
        //                              + currentTime.Millisecond.ToString();
        //            return int.p
        //strtime;
        //        }
        #endregion

        #region ������־

        /// <summary>
        /// �����־��Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// �����־��Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        #endregion

        /// <summary>
        /// ͳһ��������־������Ժ�Ͳ���ÿ��������дtry...catch�ˣ�Ŀǰ����Ҫ��ȡ����ֵ�ķ���֧�ֻ�������
        /// </summary>
        /// <param name="obj">������</param>
        /// <param name="mymethod">������</param>
        /// <param name="param">���������б�</param>
        public static void WriteErrorLog(Object obj, string mymethod, object[] param)
        {
            try
            {
                Type t = obj.GetType();
                MethodInfo mi = t.GetMethod(mymethod);
                mi.Invoke(obj, param);
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(@"D:\ErrorLog.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + "-----------------------------\n");
                sw.WriteLine(ex.ToString());
                sw.Close();
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// д��־�����Ƿ�ʽ
        /// �������ļ�ռ��ʱ��������ջ�����߳�
        /// ����ļ��Ƿ�ռ�ü��粻ռ�ý���ջ����д���ļ�
        /// </summary>
        /// <param name="p_strMess"></param>
        /// <param name="p_strPath"></param>
        public static void WriteAlter(string p_strMess, string p_strPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(p_strPath, false))
                {
                    sw.WriteLine(p_strMess);
                }
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError("��־д�����\n" + exp.Message);
                CacheMessage(p_strMess);
                PopMessage();
            }
        }

        int _iArraySize = 1024 * 16;
        int _iWriteSize = 32;
        /// <summary>
        /// ��ʼ�������ڴ�
        /// </summary>
        public void InitShareMem()
        {
            int _iMonSize = 1024 * 1024 * 1024;

            MessageStructure ms = new MessageStructure(m_stackMessage);
            byte[] bytArray = ByteConvertHelper.Object2Bytes(ms);
            m_MemDB.Init("MESSAGESTACK", _iMonSize);
            m_MemDB.Write(bytArray, 0, _iWriteSize);
        }

        /// <summary>
        /// �����ڴ�д����
        /// �ȶ�������д��ȥ
        /// </summary>
        /// <param name="p_strMess"></param>
        public void PushMessage(string p_strMess)
        {
            byte[] bytData = new byte[_iArraySize];
            m_MemDB.Read(ref bytData, 0, _iArraySize);
            MessageStructure _msB;
            object _obj = ByteConvertHelper.Bytes2Object(bytData);
            if (null != _obj)
            {
                _msB = (MessageStructure)_obj;
            }
            else
            {
                return;
            }
            _msB.m_struMessage.Push(p_strMess);
            bytData = ByteConvertHelper.Object2Bytes(_msB);
            m_MemDB.Write(bytData, 0, _iWriteSize);
        }


        public string GetMessage()
        {
            byte[] bytData = new byte[_iArraySize];
            string _strTemp = string.Empty;
            m_MemDB.Read(ref bytData, 0, _iArraySize);
            object _obj = ByteConvertHelper.Bytes2Object(bytData);
            if (null == _obj)
            {
                return "";
            }
            else
            {
                MessageStructure _msB = (MessageStructure)_obj;
                _strTemp = _msB.m_struMessage.Pop();
            }
            return _strTemp;
        }

        /// <summary>
        /// ����Ϣд���ջ
        /// 2015-08-31
        /// </summary>
        /// <param name="p_strMess"></param>
        public static void CacheMessage(string p_strMess)
        {
            m_stackMessage.Push(p_strMess);
        }

        /// <summary>
        /// pop��Ϣ
        /// </summary>
        public static void PopMessage()
        {
            Thread t = new Thread(GetStackMessage);
            t.Start();
        }

        /// <summary>
        /// ��ȡ��ջ�е���Ϣ
        /// 2015-08-31
        /// </summary>
        public static void GetStackMessage()
        {
            while (true)
            {
                Thread.Sleep(200);
                if (0 == m_stackMessage.Count)
                {
                    break;
                }
                else
                {
                    string _strTemp = m_stackMessage.Pop();
                    WriteError("��ջװ����Ϣ��" + _strTemp);
                    WriteAlter(_strTemp, m_strAlterFilePath);
                }
            }

        }

        public static bool CanWrite(string p_strPath)
        {
            using (StreamReader sr = new StreamReader(p_strPath))
            {
                if ("YES" == sr.ReadLine().ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ��FileStreamд�ļ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static void FileStreamWriteFile(string str)
        {
            byte[] byData;
            //char[] charData;
            try
            {
                FileStream nFile = new FileStream(Application.StartupPath + @"\ErrorLog.txt", System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
                ////����ַ�����
                //charData = str.ToCharArray();
                ////��ʼ���ֽ�����
                //byData = new byte[charData.Length];
                //���ַ�����ת��Ϊ��ȷ���ֽڸ�ʽ
                //Encoder enc = Encoding.UTF8.GetEncoder();
                //enc.GetBytes(charData, 0, charData.Length, byData, 0, true);
                //nFile.Seek(0, SeekOrigin.Begin);
                byData = Encoding.UTF8.GetBytes(str);
                nFile.Write(byData, 0, 1000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InvokeMethod(string mess)
        {
            StreamWriter sw = null;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + @"\ErrorLog.txt", System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
                //fs.write
                using (sw = new StreamWriter(fs))
                {
                    sw = new StreamWriter(Application.StartupPath + @"\ErrorLog.txt", true);
                    sw.WriteLine(DateTime.Now.ToString() + "\n");
                    sw.WriteLine(mess);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }

                FileStreamWriteFile(mess);
            }
            catch (Exception exp)
            {
                if (sw != null)
                {
                    sw.Close();
                }

            }

        }

        /// <summary>
        /// �� Application.StartupPath + @"\error\"+DateTime.Today.ToString("yyyy-MM-dd") + ".error"
        /// �ļ���д������־
        /// </summary>
        /// <param name="p_strMess"></param>
        public static void WriteError(string p_strMess)
        {
            string _strFilePath = Application.StartupPath + @"\error\" + DateTime.Today.ToString("yyyy-MM-dd")+"\\";
            if (!Directory.Exists(_strFilePath))
            {
                Directory.CreateDirectory(_strFilePath);
            }
            using (StreamWriter sw = new StreamWriter(_strFilePath + "\\Excute.error", true))
            {
                sw.WriteLine("[TIME]:" + DateTime.Now.ToString());
                sw.WriteLine("[ERROR]:" + p_strMess);
                sw.WriteLine("");
            }
        }

        /// <summary>
        /// ��ָ���ļ�����д������־
        /// Ŀ¼ΪApplication.StartupPath/error/
        /// 2015-10-28 
        /// </summary>
        /// <param name="p_strFileName">�ļ���</param>
        /// <param name="p_strMess">��Ϣ</param>
        /// <param name="p_bAppend">�Ƿ�׷��</param>
        public static void WriteError(string p_strFileName, string p_strMess, bool p_bAppend)
        {
            string _strFilePath = string.Format(@"{0}\error\{1}\", Application.StartupPath, DateTime.Today.ToString("yyyy-MM-dd"));
            // Application.StartupPath + @"\error\" + DateTime.Today.ToString("yyyy-MM-dd") + @"\";
            if (!Directory.Exists(_strFilePath))
            {
                Directory.CreateDirectory(_strFilePath);
            }
            using (StreamWriter sw = new StreamWriter(_strFilePath + p_strFileName + ".error", p_bAppend))
            {
                sw.WriteLine("[TIME]:" + DateTime.Now.ToString() + "");
                sw.WriteLine("[ERROR]:" + p_strMess);
                sw.WriteLine("");
            }

        }

        /// <summary>
        /// ��ָ���ļ�����д������־
        /// Ŀ¼ΪApplication.StartupPath/error/
        /// 2015-10-28
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <param name="p_strMess"></param>
        public static void WriteError(string p_strFileName, string p_strMess)
        {
            WriteError(p_strFileName, p_strMess, true);
        }

        /// <summary>
        /// �����¼��Ϣ
        /// ��log4net 
        /// </summary>
        /// <param name="p_strMess">��Ϣ</param>
        public static void WriteLog(string p_strMess)
        {
            string _strFilePath = Application.StartupPath + @"\log\";
            if (!Directory.Exists(_strFilePath))
            {
                Directory.CreateDirectory(_strFilePath);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(_strFilePath + DateTime.Today.ToString("yyyy-MM-dd") + ".log", true))
                {
                    sw.WriteLine("[TIME]:" + DateTime.Now.ToString());
                    sw.WriteLine("[EVENT]:" + p_strMess);
                    sw.WriteLine("");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        #endregion

        #region ������usercontrol�İ�
        /// <summary>
        /// �򵥵Ĵ������á���Ϊÿ����ʾ����Ĵ��붼��һ���ġ�
        /// </summary>
        /// <param name="f">��������</param>
        /// <param name="uc">��������</param>
        public static DialogResult AddForm(Form f, UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            f.StartPosition = FormStartPosition.CenterParent;
            //f.TopMost = true;
            f.Controls.Add(uc);
            return f.ShowDialog();
        }

        public static void AddForm2(UserControl uc)
        {
            Form f = new Form();
            f.Size = new Size(uc.Size.Width + 2, uc.Size.Height + 2);
            f.StartPosition = FormStartPosition.CenterParent;
            f.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            f.ShowDialog();
        }
        /// <summary>
        /// ��panel��������ӿؼ�
        /// </summary>
        /// <param name="p">����panel</param>
        /// <param name="uc">��ʾ��usercontrol</param>
        public static void AddForm3(Panel p, UserControl uc)
        {
            p.Controls.Clear();
            p.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

        }
        #endregion

        #region �ȴ�����
        /// <summary>
        /// ������ǰ��&��Ϣ��ֵ
        /// </summary>
        /// <param name="_p">����</param>
        /// <param name="_m">�������ϵ��ı�</param>
        public static void SetProcessBarMess(int _p)
        {

            //object o = _p;
            //frmProecessBar pw = new frmProecessBar();
            ////pw.SetProecssBarPosition(_p, _m);
            //Thread fThread = new Thread(new ParameterizedThreadStart(pw.SetProecssBarPosition));
            //fThread.Start(_p);

        }

        /// <summary>
        /// ���ù����������ֵ
        /// </summary>
        /// <param name="_maxvlue">���ֵ</param>
        public static void SetProcessBarMaxnum(int _maxvlue)
        {
            //frmProecessBar.maxvalue = _maxvlue;
        }
        /// <summary>
        /// ��������ʾ
        /// </summary>
        public static void ShowProcessBar()
        {
            //frmProcessBar pb = new frmProcessBar();
            //pb.ShowDialog();
        }
        /// <summary>
        /// ��tabcontrol���翨Ƭ��
        /// </summary>
        /// <param name="tc">ָ����tabcontrol</param>
        public static void AddTabControl(TabControl tc, string title, Control c)
        {
            if (dicpage.ContainsKey(title))
            {
                tc.SelectTab(title);
                return;
            }
            TabPage tp = new TabPage(title);
            tp.Name = title;
            dicpage.Add(title, tp);
            tp.Controls.Add(c);
            c.Dock = DockStyle.Fill;
            tc.TabPages.Add(dicpage[title]);
            tc.SelectTab(title);
        }

        /// <summary>
        /// ���ô�����ʽ�������ؼ����뵽������
        /// </summary>
        private static void ShowWaiting()
        {
            Form f = new Form();
            f.TopMost = true;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.Size = new Size(339, 88);
            f.FormBorderStyle = FormBorderStyle.None;
            uctlPleaseWaiting pw = new uctlPleaseWaiting();
            AddForm(f, pw);
        }
        /// <summary>
        /// ����һ���µ��̣߳���������
        /// </summary>
        public void WaitingThreadStart()
        {
            t = new Thread(new ThreadStart(ShowWaiting));
            t.Start();
        }
        /// <summary>
        /// ��ֹ����
        /// </summary>
        public void WaitingThreadStop()
        {
            t.Abort();
        }
        #endregion

        #region ������ת��Ϊָ������
        /// <summary>
        /// ������ת��Ϊָ������
        /// </summary>
        /// <param name="data">ת��������</param>
        /// <param name="targetType">ת����Ŀ������</param>
        public static object ConvertTo(object data, Type targetType)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return null;
            }

            Type type2 = data.GetType();
            if (targetType == type2)
            {
                return data;
            }
            if (((targetType == typeof(Guid)) || (targetType == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(data.ToString()))
                {
                    return null;
                }
                return new Guid(data.ToString());
            }

            if (targetType.IsEnum)
            {
                try
                {
                    return Enum.Parse(targetType, data.ToString(), true);
                }
                catch
                {
                    return Enum.ToObject(targetType, data);
                }
            }

            if (targetType.IsGenericType)
            {
                targetType = targetType.GetGenericArguments()[0];
            }

            return Convert.ChangeType(data, targetType);
        }

        /// <summary>
        /// ������ת��Ϊָ������
        /// </summary>
        /// <typeparam name="T">ת����Ŀ������</typeparam>
        /// <param name="data">ת��������</param>
        public static T ConvertTo<T>(object data)
        {
            if (data == null || Convert.IsDBNull(data))
                return default(T);

            object obj = ConvertTo(data, typeof(T));
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }
        #endregion

        #region ��ȡExcel��������

        /// <summary>
        /// ����Excel��������
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="eType">Excel �汾 </param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(string excelPath, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            return GetExcelTablesName(connectstring);
        }

        /// <summary>
        /// ����Excel��������
        /// </summary>
        /// <param name="connectstring">excel�����ַ���</param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(string connectstring)
        {
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                return GetExcelTablesName(conn);
            }
        }

        /// <summary>
        /// ����Excel��������
        /// </summary>
        /// <param name="connection">excel����</param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(OleDbConnection connection)
        {
            List<string> list = new List<string>();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(ConvertTo<string>(dt.Rows[i][2]));
                }
            }

            return list;
        }

        /// <summary>
        /// ����Excel��һ�����������
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="eType">Excel �汾 </param>
        /// <returns></returns>
        public static string GetExcelFirstTableName(string excelPath, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            return GetExcelFirstTableName(connectstring);
        }

        /// <summary>
        /// ����Excel��һ�����������
        /// </summary>
        /// <param name="connectstring">excel�����ַ���</param>
        /// <returns></returns>
        public static string GetExcelFirstTableName(string connectstring)
        {
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                return GetExcelFirstTableName(conn);
            }
        }

        /// <summary>
        /// ����Excel��һ�����������
        /// </summary>
        /// <param name="connection">excel����</param>
        /// <returns></returns>
        public static string GetExcelFirstTableName(OleDbConnection connection)
        {
            string tableName = string.Empty;

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                tableName = ConvertTo<string>(dt.Rows[0][2]);
            }

            return tableName;
        }

        /// <summary>
        /// ��ȡExcel�ļ���ָ�����������
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="table">���� excel table  ���磺Sheet1$</param>
        /// <returns></returns>
        public static List<string> GetColumnsList(string excelPath, ExcelType eType, string table)
        {
            List<string> list = new List<string>();
            DataTable tableColumns = null;
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                conn.Open();
                tableColumns = GetReaderSchema(table, conn);
            }
            foreach (DataRow dr in tableColumns.Rows)
            {
                string columnName = dr["ColumnName"].ToString();
                string datatype = ((OleDbType)dr["ProviderType"]).ToString();//��Ӧ���ݿ�����
                string netType = dr["DataType"].ToString();//��Ӧ��.NET���ͣ���System.String
                list.Add(columnName);
            }

            return list;
        }

        private static DataTable GetReaderSchema(string tableName, OleDbConnection connection)
        {
            DataTable schemaTable = null;
            IDbCommand cmd = new OleDbCommand();
            cmd.CommandText = string.Format("select * from [{0}]", tableName);
            cmd.Connection = connection;

            using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
            {
                schemaTable = reader.GetSchemaTable();
            }
            return schemaTable;
        }

        #endregion

        #region EXCEL����DataSet

        /// <summary>
        /// EXCEL����DataSet
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="table">���� excel table  ���磺Sheet1$ </param>
        /// <param name="header">�Ƿ�ѵ�һ����Ϊ����</param>
        /// <param name="eType">Excel �汾 </param>
        /// <returns>����Excel��Ӧ�������е����� DataSet   [table������ʱ���ؿյ�DataSet]</returns>
        public static DataSet ExcelToDataSet(string excelPath, string table, bool header, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, header, eType);
            return ExcelToDataSet(connectstring, table);
        }

        /// <summary>
        /// �жϹ��������Ƿ����
        /// </summary>
        /// <param name="connection">excel����</param>
        /// <param name="table">���� excel table  ���磺Sheet1$</param>
        /// <returns></returns>
        private static bool isExistExcelTableName(OleDbConnection connection, string table)
        {
            List<string> list = GetExcelTablesName(connection);
            foreach (string tName in list)
            {
                if (tName.ToLower() == table.ToLower())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// EXCEL����DataSet
        /// </summary>
        /// <param name="connectstring">excel�����ַ���</param>
        /// <param name="table">���� excel table  ���磺Sheet1$ </param>
        /// <returns>����Excel��Ӧ�������е����� DataSet   [table������ʱ���ؿյ�DataSet]</returns>
        public static DataSet ExcelToDataSet(string connectstring, string table)
        {
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                DataSet ds = new DataSet();

                //�жϸù�������Excel���Ƿ����
                if (isExistExcelTableName(conn, table))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + table + "]", conn);
                    adapter.Fill(ds, table);
                }

                return ds;
            }
        }

        /// <summary>
        /// EXCEL���й�������DataSet
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="header">�Ƿ�ѵ�һ����Ϊ����</param>
        /// <param name="eType">Excel �汾 </param>
        /// <returns>����Excel��һ���������е����� DataSet </returns>
        public static DataSet ExcelToDataSet(string excelPath, bool header, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, header, eType);
            return ExcelToDataSet(connectstring);
        }

        /// <summary>
        /// EXCEL���й�������DataSet
        /// </summary>
        /// <param name="connectstring">excel�����ַ���</param>
        /// <returns>����Excel��һ���������е����� DataSet </returns>
        public static DataSet ExcelToDataSet(string connectstring)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectstring))
                {
                    List<string> tableNames = GetExcelTablesName(conn);
                    foreach (string tableName in tableNames)
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + tableName + "]", conn);
                        adapter.Fill(ds, tableName);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            return ds;
        }

        #endregion

        #region ��ȡExcel�����ַ���

        /// <summary>
        /// ����Excel �����ַ���   [IMEX=1]
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="header">�Ƿ�ѵ�һ����Ϊ����</param>
        /// <param name="eType">Excel �汾 </param>
        /// <returns></returns>
        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType)
        {
            return GetExcelConnectstring(excelPath, header, eType, IMEXType.ImportMode);
        }

        /// <summary>
        /// ����Excel �����ַ���
        /// </summary>
        /// <param name="excelPath">Excel�ļ� ����·��</param>
        /// <param name="header">�Ƿ�ѵ�һ����Ϊ����</param>
        /// <param name="eType">Excel �汾 </param>
        /// <param name="imex">IMEXģʽ</param>
        /// <returns></returns>
        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType, IMEXType imex)
        {
            if (!File.Exists(excelPath))
                throw new FileNotFoundException("Excel·��������!");

            string connectstring = string.Empty;

            string hdr = "NO";
            if (header)
                hdr = "YES";

            if (eType == ExcelType.Excel2003)
                connectstring = "Provider=Microsoft.Jet.OleDb.4.0; data source=" + excelPath + ";Extended Properties='Excel 8.0; HDR=" + hdr + "; IMEX=" + imex.GetHashCode() + "'";
            else
                connectstring = "Provider=Microsoft.ACE.OLEDB.12.0; data source=" + excelPath + ";Extended Properties='Excel 12.0 Xml; HDR=" + hdr + "; IMEX=" + imex.GetHashCode() + "'";

            return connectstring;
        }

        #endregion

        #region EXCEL��datatable�໥ת��
        /// <summary>
        /// �����ݼ�������Ϊexcel
        /// </summary>
        /// <param name="name">����excel������</param>
        /// <param name="ds">�����������ݼ�</param>
        public static void AddExcel(string name, DataTable dt)
        {
            string fileName = name + ".xls";
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            int rowIndex = 1;
            int colIndex = 0;
            excel.Application.Workbooks.Add(true);
            foreach (DataColumn col in dt.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            foreach (DataRow row in dt.Rows)
            {
                rowIndex++;
                colIndex = 0;
                for (colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    excel.Cells[rowIndex, colIndex + 1] = row[colIndex].ToString();
                }
            }

            excel.Visible = false;
            excel.ActiveWorkbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            excel.Quit();
            excel = null;
            GC.Collect();//�������� 
        }

        public static void SaveAsExcel(string name, DataTable dt)
        {
            OleDbConnection cnnxls = new OleDbConnection(string.Format(excelstring, name));
            string insertsql = "";
            string insertcolumnstring = "";
            string insertvaluestring = "";
            string fileName = name + ".xls";
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excel.Application.Workbooks.Add(true);
            int colIndex = 0;
            foreach (DataColumn col in dt.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
                insertcolumnstring += string.Format("{0},", col.ColumnName);
            }
            excel.ActiveWorkbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);

            //������¼  
            //conn.execute(sql);

            insertcolumnstring = insertcolumnstring.Trim(',');
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    row[dc].ToString();
                    insertvaluestring += string.Format("'{0}',", row[dc].ToString().Replace("'", "''"));
                }
                string sql = string.Format("insert into [Sheet1$]({0}) values({1})", insertcolumnstring, insertvaluestring);
                OleDbDataAdapter myDa = new OleDbDataAdapter(sql, cnnxls);
                myDa.InsertCommand.ExecuteNonQuery();
            }
            excel.Visible = false;
            //excel.ActiveWorkbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            excel.Quit();
            excel = null;
            GC.Collect();//�������� 
        }

        #endregion

        #region XML��dataset�໥ת��
        /// <summary>
        /// ��xml���������ַ���ת��ΪDataSet
        /// </summary>
        /// <param name="xmlData">�ַ���</param>
        /// <returns>����dataset����</returns>
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                //��streamװ�ص�XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }


        /// <summary>
        ///  ��xml�ļ�ת��ΪDataSet
        /// </summary>
        /// <param name="xmlFile">xml�ļ���ַ</param>
        /// <returns>dataset����</returns>
        public static DataSet ConvertXMLFileToDataSet(string xmlFile)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlDocument xmld = new XmlDocument();
                xmld.Load(xmlFile);

                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmld.InnerXml);
                //��streamװ�ص�XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                //xmlDS.ReadXml(xmlFile);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return null;
        }


        /// <summary>
        ///  ��DataSetת��Ϊxml�����ַ���
        /// </summary>
        /// <param name="xmlDS">dataset����</param>
        /// <returns>xml�ַ���</returns>
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //��streamװ�ص�XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //��WriteXml����д���ļ�.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static void SetXMLValue(string xmlFile)
        {
            //XmlDocument xmld = new XmlDocument();
            //xmld.Load(xmlFile);
            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmld.NameTable);
            //XmlNodeList xmln = xmld.SelectNodes(xmlFile, nsmgr);
            //XmlNode newnode = new XmlNode("www");
            //xmln.AppendChild(newnode);
        }

        /// <summary>
        /// �����ݼ�ת��ΪptNameָ���ĸ�ʽ�洢��xmlFile·����
        /// </summary>
        /// <param name="xmlDS">��Ҫת��������</param>
        /// <param name="xmlFile">���·��</param>
        /// <param name="sqlgetlayout">��ȡ��ʽ</param>
        /// <param name="doc">xml�ļ�</param>
        public static System.Collections.Generic.Dictionary<string, XmlElement> ConverDataToXMLFile(XmlDocument doc, DataSet xmlDS, DataTable dtgetlayout)
        {
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(dec);
            Dictionary<string, XmlElement> xmldic = new Dictionary<string, XmlElement>();
            try
            {
                foreach (DataRow drlayout in dtgetlayout.Rows)
                {
                    foreach (DataRow drxml in xmlDS.Tables[0].Rows)
                    {
                        foreach (DataColumn dcxml in xmlDS.Tables[0].Columns)
                        {
                            if (drlayout["field_name"].ToString().Equals(dcxml.ToString()))
                            {
                                XmlElement xe1 = doc.CreateElement((drlayout["field_name"].ToString()));
                                xe1.InnerText = drxml[dcxml].ToString();
                                xmldic.Add(dcxml.ToString(), xe1);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return xmldic;
        }

        public static void ConverDataSetToXMLFile(string strxml, string xmlFile)
        {
            try
            {
                StreamWriter sw = new StreamWriter(xmlFile);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine(strxml.ToString());
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// ��xml���ݼ�תת��Ϊxml�ļ�
        /// </summary>
        /// <param name="dsxml">���ݼ�</param>
        /// <param name="xmlFile">·��</param>
        /// <returns>�����ַ���</returns>
        public static string ConverDataSetToXMLFile(DataSet dsxml, string xmlFile)
        {
            try
            {
                StringBuilder strxml = new StringBuilder(ConvertDataSetToXML(dsxml));
                StreamWriter sw = new StreamWriter(xmlFile);
                strxml.Replace("NewDataSet", "��һ��");
                strxml.Replace("ds", "�ڶ���");
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine(strxml.ToString());
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return "";
        }

        /// <summary>
        /// ��DataSetת��Ϊxml�ļ�
        /// </summary>
        /// <param name="xmlDS">dtaset����</param>
        /// <param name="xmlFile">�ļ����·��</param>
        public static void ConvertDataSetToXMLFile(DataSet xmlDS, string xmlFile)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Unicode);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                //����Unicode������ı�
                UnicodeEncoding utf = new UnicodeEncoding();
                StreamWriter sw = new StreamWriter(xmlFile);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine(utf.GetString(arr).Trim());
                sw.Close();
            }
        }
        #endregion

        #region �������ݿ�

        ///// <summary>
        ///// �滻sql������������cmd��ֵ,�󶨱���
        ///// </summary>
        ///// <param name="p_strSql">sql���</param>
        ///// <param name="p_dictParam">�����ֵ�</param>
        ///// <param name="p_oleCmd">cmd</param>
        ///// <returns>�����Ƿ��滻�����ɹ�</returns>
        public static void OleChangeSelectCommand(string p_strSql, SortedDictionary<string, string> p_dictParam, ref OleDbCommand p_oleCmd)
        {
            p_oleCmd.Parameters.Clear();
            if (p_dictParam != null)
            {
                foreach (object obj in p_dictParam.Keys)
                {
                    string strParm = obj.ToString();
                    string values;
                    p_dictParam.TryGetValue(obj.ToString(), out values);
                    p_oleCmd.Parameters.Add(strParm, OleDbType.VarChar).Value = values;
                }
            }
            p_oleCmd.CommandText = p_strSql;
        }

        /// <summary>
        /// ִ�в�ѯ����
        /// </summary>
        /// <param name="p_strSql">sql</param>
        /// <param name="p_strTablename">����</param>
        /// <param name="p_strConnectionStringName">���Ӵ���</param>
        /// <returns></returns>
        public static DataTable OleExecuteBySQL(string p_strSql, string p_strTablename, string p_strConnectionStringName)
        {
            DataTable _dtTable = new DataTable(p_strTablename);
            String _strConnectionString = ConfigurationManager.ConnectionStrings[p_strConnectionStringName].ConnectionString;
            using (OleDbConnection _oleConn = new OleDbConnection(_strConnectionString))
            {
                try
                {
                    OleDbCommand _oleCmd = _oleConn.CreateCommand();
                    _oleConn.Open();
                    _oleCmd.CommandText = p_strSql;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(_oleCmd);
                    adapter.Fill(_dtTable);
                }
                catch (Exception exp)
                {
                    WriteError("DB",exp.Message);
                    return null;
                    //throw;
                }
            }
            return _dtTable;
        }

        /// <summary>
        /// ִ����ɾ�Ĳ���
        /// </summary>
        /// <param name="p_strSql">sql</param>
        /// <param name="p_strConnectionStringName">���Ӵ���</param>
        /// <returns></returns>
        public static int OleExecuteNonQuery(string p_strSql, string p_strConnectionStringName)
        {
            int _iExeCount = 0;
            String _strConnectionString = ConfigurationManager.ConnectionStrings[p_strConnectionStringName].ConnectionString;
            using (OleDbConnection _oleConn = new OleDbConnection(_strConnectionString))
            {
                OleDbCommand _oleCmd = _oleConn.CreateCommand();
                try
                {
                    _oleConn.Open();
                    _oleCmd.CommandText = p_strSql;
                    _iExeCount = _oleCmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    WriteError("DB",exp.Message);
                    //throw;
                }
            }
            return _iExeCount;
        }


        /// <summary>
        /// ִ�в�ѯ����
        /// </summary>
        /// <param name="sql">��ѯsql���</param>
        /// <param name="dictionary">�ֵ����</param>
        /// <param name="tablename">������datatable����</param>
        /// <param name="cmd">cmd</param>
        /// <returns>���ر�</returns>
        private DataTable ExecuteBySQL(string sql, Dictionary<string, string> dictionary, string tablename, string ConnectionString)
        {
            DataTable table = new DataTable(tablename);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                OleDbCommand cmd = conn.CreateCommand();
                conn.Open();
                ChangeSelectCommand(sql, dictionary, ref cmd);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(table);
            }
            return table;
        }

        /// <summary>
        /// ִ������ɾ���Ĳ���
        /// </summary>
        /// <param name="sql">������sql</param>
        /// <param name="dictionary">�ֵ����</param>
        /// <param name="cmd">cmd</param>
        /// <returns>���ؽ��</returns>
        public static int ExecutenonQuery(string sql, Dictionary<string, string> dictionary, string ConnectionString)
        {
            int n = 0;
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                OleDbCommand cmd = conn.CreateCommand();
                conn.Open();
                ChangeSelectCommand(sql, dictionary, ref cmd);
                n = cmd.ExecuteNonQuery();
            }
            return n;
        }
        /// <summary>
        /// ͨ��APP.config�ļ���ȡ�������ݿ��ַ���
        /// </summary>
        /// <returns></returns>
        private static string GetConnString()
        {
            string strconn = System.Configuration.ConfigurationSettings.AppSettings["StrConn"].ToString();
            return strconn;
        }

        /// <summary>
        /// �滻sql������������cmd��ֵ
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="dictionary">�����ֵ�</param>
        /// <param name="cmd">cmd</param>
        /// <returns>�����Ƿ��滻�����ɹ�</returns>
        static public bool ChangeSelectCommand(string sql, Dictionary<string, string> dictionary, ref OleDbCommand cmd)
        {

            cmd.Parameters.Clear();
            string sqltxt = sql;
            int nIndex = sqltxt.IndexOf(':');
            while (-1 != nIndex)
            {
                if (nIndex > -1)
                {
                    foreach (object obj in dictionary.Keys)
                    {
                        string strParm = ":" + obj.ToString();
                        if (nIndex == sqltxt.IndexOf(strParm, nIndex))
                        {
                            string values;
                            dictionary.TryGetValue(obj.ToString(), out values);
                            cmd.Parameters.Add(nIndex.ToString(), OleDbType.VarChar).Value = values;
                        }
                    }
                }
                if (sqltxt.Length > nIndex)
                {
                    nIndex = sqltxt.IndexOf(':', nIndex + 1);
                }
                else
                    nIndex = -1;
            }
            if (dictionary != null)
            {
                foreach (object obj in dictionary.Keys)
                {
                    string strParm = ":" + obj.ToString();
                    sqltxt = sqltxt.Replace(strParm, "?");
                }
            }
            cmd.CommandText = sqltxt;
            return true;
        }

        #endregion

        #region ����Ƿ�Ϊ��
        public static DataTable CheckNULL(DataTable source)
        {
            if (source == null)
            {
                MessageBox.Show("����ԴΪ��");
                return null;
            }
            else
            {
                return source;
            }
        }
        #endregion

        #region ��̬���ɲ˵�
        /// <summary>
        /// ��̬�����˵�
        /// </summary>
        private void CreateMenu(MenuStrip MainMenuStrip, Form parient_form)
        {
            //����һ�����˵�
            MenuStrip mainMenu = new MenuStrip();
            DataSet ds = new DataSet();
            //��XML�ж�ȡ���ݡ����ݽṹ������ϸ��һ�¡�
            ds.ReadXml(@"..\..\Menu.xml");
            DataView dv = ds.Tables[0].DefaultView;
            //ͨ��DataView�������������ȵõ����Ĳ˵�
            dv.RowFilter = "ParentItemID=0";
            for (int i = 0; i < dv.Count; i++)
            {
                //����һ���˵���
                ToolStripMenuItem topMenu = new ToolStripMenuItem();
                //���˵���Textֵ��Ҳ�����ڽ����Ͽ�����ֵ��
                topMenu.Text = dv[i]["Text"].ToString();
                //��������¼��˵���ͨ��CreateSubMenu�����������¼��˵�
                if (Convert.ToInt16(dv[i]["IsModule"]) == 1)
                {
                    //��ref�ķ�ʽ������˵����ݲ�������Ϊ�������ڸ�ֵ���ٻش�������Ҳ���и��õķ���^_^.
                    CreateSubMenu(ref topMenu, Convert.ToInt32(dv[i]["ItemID"]), ds.Tables[0]);
                }
                //��ʾӦ�ó������Ѵ򿪵� MDI �Ӵ����б�Ĳ˵���
                mainMenu.MdiWindowListItem = topMenu;
                //���ݹ鸽�ӺõĲ˵��ӵ��˵������ϡ�
                mainMenu.Items.Add(topMenu);
            }
            mainMenu.Dock = DockStyle.Top;
            //�������MainMenuStrip��ΪmainMenu.
            MainMenuStrip = mainMenu;
            //������Ҫ�������д���˵�������������������С�
            parient_form.Controls.Add(mainMenu);
        }

        /// <summary>
        /// �����Ӳ˵�
        /// </summary>
        /// <param name="topMenu">���˵���</param>
        /// <param name="ItemID">���˵���ID</param>
        /// <param name="dt">���в˵����ݼ�</param>
        private void CreateSubMenu(ref ToolStripMenuItem topMenu, int ItemID, DataTable dt)
        {
            DataView dv = new DataView(dt);
            //���˳���ǰ���˵����������Ӳ˵�����(��Ϊ��һ���)
            dv.RowFilter = "ParentItemID=" + ItemID.ToString();

            for (int i = 0; i < dv.Count; i++)
            {
                //�����Ӳ˵���
                ToolStripMenuItem subMenu = new ToolStripMenuItem();
                subMenu.Text = dv[i]["Text"].ToString();
                //��������Ӳ˵�������ݹ���ء�
                if (Convert.ToInt16(dv[i]["IsModule"]) == 1)
                {
                    //�ݹ����
                    CreateSubMenu(ref subMenu, Convert.ToInt32(dv[i]["ItemID"]), dt);
                }
                else
                {
                    //��չ���Կ��Լ��κ���Ҫ��ֵ��������formName���������ش��塣
                    subMenu.Tag = dv[i]["FormName"].ToString();
                    //��û���Ӳ˵��Ĳ˵�����¼���
                    subMenu.Click += new EventHandler(subMenu_Click);
                }
                if (dv[i]["ImageName"].ToString().Length > 0)
                {
                    //���ò˵���ǰ���ͼƱΪ16X16��ͼƬ�ļ���
                    Image img = Image.FromFile(@"..\..\Image\" + dv[i]["ImageName"].ToString());
                    subMenu.Image = img;
                    subMenu.Image.Tag = dv[i]["ImageName"].ToString();
                }
                //���˵��ӵ�����˵��¡�
                topMenu.DropDownItems.Add(subMenu);
            }
        }

        /// <summary>
        /// �˵������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void subMenu_Click(object sender, EventArgs e)
        {
            //tag�������������õ���
            //string formName = ((ToolStripMenuItem)sender).Tag.ToString();
            //CreateFormInstance(formName);
        }

        /// <summary>
        /// ����formʵ����
        /// </summary>
        /// <param name="formName">form������</param>
        private void CreateFormInstance(Form form, string formName)
        {
            bool flag = false;
            //�����������ϵ������Ӳ˵�
            for (int i = 0; i < form.MdiChildren.Length; i++)
            {
                //�������Ĵ��ڱ��������¼���
                if (form.MdiChildren[i].Tag.ToString().ToLower() == formName.ToLower())
                {
                    form.MdiChildren[i].Activate();
                    form.MdiChildren[i].Show();
                    form.MdiChildren[i].WindowState = FormWindowState.Normal;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                //������������÷��䴴��form����ʵ����
                Assembly asm = Assembly.Load("Fastyou.BookShop.Win");//������
                object frmObj = asm.CreateInstance("Fastyou.BookShop.Win." + formName);//����+form��������
                Form frms = (Form)frmObj;
                //tag����Ҫ����дһ�Σ������ڵڶ��ε�ʱ��ȡ������ԭ�򻹲��������֪��������֪��
                frms.Tag = formName.ToString();
                frms.MdiParent = form;
                frms.Show();
            }
        }
        #endregion

        #region �ļ����� 2015-01-05

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="p_strSourcePath">Դ�ļ�</param>
        /// <param name="p_strTargetPath">Ŀ���ļ�</param>
        /// <param name="p_bOverWrite">�Ƿ񸲸�</param>
        /// <returns></returns>
        public static bool CopyFile(string p_strSourcePath, string p_strTargetPath, bool p_bOverWrite)
        {
            try
            {
                String sourcePath = p_strSourcePath;
                String targetPath = p_strTargetPath;
                bool isrewrite = p_bOverWrite; // true=�����Ѵ��ڵ�ͬ���ļ�,false��֮
                System.IO.File.Copy(sourcePath, targetPath, isrewrite);
                return true;
            }
            catch (Exception exp)
            {
                WriteError(exp.ToString());
                return false;
            }
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
        /// 2015-01-05
        /// �⺣��
        /// ����������Ϣ
        /// </summary>
        /// <param name="p_strKey"></param>
        /// <param name="p_strValue"></param>
        public static void SaveConfig(string p_strKey, string p_strValue)
        {
            //��ȡConfiguration����
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //д��<add>Ԫ�ص�Value
            config.AppSettings.Settings[p_strKey].Value = p_strValue;
            config.Save(ConfigurationSaveMode.Modified);
            //ˢ�£���������ȡ�Ļ���֮ǰ��ֵ��������װ���ڴ棩
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }
}
