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
using System.Data.OracleClient;

namespace ToolFunction
{
    public class CommonFunction
    {
        #region 属性

        //public static 
        /// <summary>
        /// Excel 版本
        /// </summary>
        public enum ExcelType
        {
            Excel2003, Excel2007
        }
        /// <summary>
        /// IMEX 三种模式。
        /// IMEX是用来告诉驱动程序使用Excel文件的模式，其值有0、1、2三种，分别代表导出、导入、混合模式。
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
        /// 消息堆栈，当文件占用时写入堆栈等待文件接触占用，将堆栈内容写入文件
        /// </summary>
        public static Stack<string> m_stackMessage = new Stack<string>();
        public static string m_strAlterFilePath = string.Empty;
        /// <summary>
        /// 共享内存
        /// </summary>
        public ShareMem m_MemDB = new ShareMem();
        /// <summary>
        /// 利用结构体包裹堆栈，以便序列化
        /// 2015-09-01
        /// </summary>
        [Serializable]
        public struct MessageStructure
        {
            public Stack<string> m_struMessage; //变量m_struMessage

            /// <summary>
            /// 构造函数
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

        #region 构造函数
        public CommonFunction()
        {

        }
        #endregion

        #region 程序设置主键，string类型
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

        #region 错误日志

        /// <summary>
        /// 输出日志到Log4Net
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
        /// 输出日志到Log4Net
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
        /// 统一化错误日志输出，以后就不用每个方法都写try...catch了，目前对需要获取返回值的方法支持还不够。
        /// </summary>
        /// <param name="obj">对象类</param>
        /// <param name="mymethod">方法名</param>
        /// <param name="param">方法参数列表</param>
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
        /// 写日志，覆盖方式
        /// 当发生文件占用时开启，堆栈操作线程
        /// 监控文件是否占用假如不占用将堆栈内容写入文件
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
                CommonFunction.WriteError("日志写入错误：\n" + exp.Message);
                CacheMessage(p_strMess);
                PopMessage();
            }
        }

        int _iArraySize = 1024 * 16;
        int _iWriteSize = 32;
        /// <summary>
        /// 初始化共享内存
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
        /// 向共享内存写数据
        /// 先读出来在写进去
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
        /// 将信息写入堆栈
        /// 2015-08-31
        /// </summary>
        /// <param name="p_strMess"></param>
        public static void CacheMessage(string p_strMess)
        {
            m_stackMessage.Push(p_strMess);
        }

        /// <summary>
        /// pop信息
        /// </summary>
        public static void PopMessage()
        {
            Thread t = new Thread(GetStackMessage);
            t.Start();
        }

        /// <summary>
        /// 获取堆栈中的信息
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
                    WriteError("堆栈装入信息：" + _strTemp);
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
        /// 用FileStream写文件
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
                ////获得字符数组
                //charData = str.ToCharArray();
                ////初始化字节数组
                //byData = new byte[charData.Length];
                //将字符数组转换为正确的字节格式
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
        /// 向 Application.StartupPath + @"\error\"+DateTime.Today.ToString("yyyy-MM-dd") + ".error"
        /// 文件书写错误日志
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
        /// 向指定文件下书写错误日志
        /// 目录为Application.StartupPath/error/
        /// 2015-10-28 
        /// </summary>
        /// <param name="p_strFileName">文件名</param>
        /// <param name="p_strMess">消息</param>
        /// <param name="p_bAppend">是否追加</param>
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
        /// 向指定文件下书写错误日志
        /// 目录为Application.StartupPath/error/
        /// 2015-10-28
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <param name="p_strMess"></param>
        public static void WriteError(string p_strFileName, string p_strMess)
        {
            WriteError(p_strFileName, p_strMess, true);
        }

        /// <summary>
        /// 输出记录信息
        /// 非log4net 
        /// </summary>
        /// <param name="p_strMess">信息</param>
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

        #region 窗体域usercontrol的绑定
        /// <summary>
        /// 简单的代码重用。因为每次显示窗体的代码都是一样的。
        /// </summary>
        /// <param name="f">容器窗体</param>
        /// <param name="uc">内容容器</param>
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
        /// 项panel容器中添加控件
        /// </summary>
        /// <param name="p">容器panel</param>
        /// <param name="uc">显示的usercontrol</param>
        public static void AddForm3(Panel p, UserControl uc)
        {
            p.Controls.Clear();
            p.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

        }
        #endregion

        #region 等待窗体
        /// <summary>
        /// 进度条前进&信息赋值
        /// </summary>
        /// <param name="_p">进度</param>
        /// <param name="_m">进度条上的文本</param>
        public static void SetProcessBarMess(int _p)
        {

            //object o = _p;
            //frmProecessBar pw = new frmProecessBar();
            ////pw.SetProecssBarPosition(_p, _m);
            //Thread fThread = new Thread(new ParameterizedThreadStart(pw.SetProecssBarPosition));
            //fThread.Start(_p);

        }

        /// <summary>
        /// 设置滚动条的最大值
        /// </summary>
        /// <param name="_maxvlue">最大值</param>
        public static void SetProcessBarMaxnum(int _maxvlue)
        {
            //frmProecessBar.maxvalue = _maxvlue;
        }
        /// <summary>
        /// 进度条显示
        /// </summary>
        public static void ShowProcessBar()
        {
            //frmProcessBar pb = new frmProcessBar();
            //pb.ShowDialog();
        }
        /// <summary>
        /// 向tabcontrol假如卡片。
        /// </summary>
        /// <param name="tc">指定的tabcontrol</param>
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
        /// 设置窗体样式，并将控件加入到窗体中
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
        /// 创建一个新的线程，并开启。
        /// </summary>
        public void WaitingThreadStart()
        {
            t = new Thread(new ThreadStart(ShowWaiting));
            t.Start();
        }
        /// <summary>
        /// 终止进程
        /// </summary>
        public void WaitingThreadStop()
        {
            t.Abort();
        }
        #endregion

        #region 将数据转换为指定类型
        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
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
        /// 将数据转换为指定类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
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

        #region 获取Excel工作表名

        /// <summary>
        /// 返回Excel工作表名
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(string excelPath, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            return GetExcelTablesName(connectstring);
        }

        /// <summary>
        /// 返回Excel工作表名
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(string connectstring)
        {
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                return GetExcelTablesName(conn);
            }
        }

        /// <summary>
        /// 返回Excel工作表名
        /// </summary>
        /// <param name="connection">excel连接</param>
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
        /// 返回Excel第一个工作表表名
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns></returns>
        public static string GetExcelFirstTableName(string excelPath, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            return GetExcelFirstTableName(connectstring);
        }

        /// <summary>
        /// 返回Excel第一个工作表表名
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <returns></returns>
        public static string GetExcelFirstTableName(string connectstring)
        {
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                return GetExcelFirstTableName(conn);
            }
        }

        /// <summary>
        /// 返回Excel第一个工作表表名
        /// </summary>
        /// <param name="connection">excel连接</param>
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
        /// 获取Excel文件中指定工作表的列
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="table">名称 excel table  例如：Sheet1$</param>
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
                string datatype = ((OleDbType)dr["ProviderType"]).ToString();//对应数据库类型
                string netType = dr["DataType"].ToString();//对应的.NET类型，如System.String
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

        #region EXCEL导入DataSet

        /// <summary>
        /// EXCEL导入DataSet
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="table">名称 excel table  例如：Sheet1$ </param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回Excel相应工作表中的数据 DataSet   [table不存在时返回空的DataSet]</returns>
        public static DataSet ExcelToDataSet(string excelPath, string table, bool header, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, header, eType);
            return ExcelToDataSet(connectstring, table);
        }

        /// <summary>
        /// 判断工作表名是否存在
        /// </summary>
        /// <param name="connection">excel连接</param>
        /// <param name="table">名称 excel table  例如：Sheet1$</param>
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
        /// EXCEL导入DataSet
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <param name="table">名称 excel table  例如：Sheet1$ </param>
        /// <returns>返回Excel相应工作表中的数据 DataSet   [table不存在时返回空的DataSet]</returns>
        public static DataSet ExcelToDataSet(string connectstring, string table)
        {
            using (OleDbConnection conn = new OleDbConnection(connectstring))
            {
                DataSet ds = new DataSet();

                //判断该工作表在Excel中是否存在
                if (isExistExcelTableName(conn, table))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + table + "]", conn);
                    adapter.Fill(ds, table);
                }

                return ds;
            }
        }

        /// <summary>
        /// EXCEL所有工作表导入DataSet
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回Excel第一个工作表中的数据 DataSet </returns>
        public static DataSet ExcelToDataSet(string excelPath, bool header, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, header, eType);
            return ExcelToDataSet(connectstring);
        }

        /// <summary>
        /// EXCEL所有工作表导入DataSet
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <returns>返回Excel第一个工作表中的数据 DataSet </returns>
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

        #region 获取Excel连接字符串

        /// <summary>
        /// 返回Excel 连接字符串   [IMEX=1]
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns></returns>
        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType)
        {
            return GetExcelConnectstring(excelPath, header, eType, IMEXType.ImportMode);
        }

        /// <summary>
        /// 返回Excel 连接字符串
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <param name="imex">IMEX模式</param>
        /// <returns></returns>
        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType, IMEXType imex)
        {
            if (!File.Exists(excelPath))
                CommonFunction.WriteError("Excel路径不存在!");

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

        #region EXCEL与datatable相互转换
        /// <summary>
        /// 将数据集导出成为excel
        /// </summary>
        /// <param name="name">导出excel的名称</param>
        /// <param name="ds">所导出的数据集</param>
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
            GC.Collect();//垃圾回收 
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

            //新增记录  
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
            GC.Collect();//垃圾回收 
        }

        #endregion

        #region XML与dataset相互转换
        /// <summary>
        /// 将xml对象内容字符串转换为DataSet
        /// </summary>
        /// <param name="xmlData">字符串</param>
        /// <returns>返回dataset对象</returns>
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                //从stream装载到XmlTextReader
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
        ///  将xml文件转换为DataSet
        /// </summary>
        /// <param name="xmlFile">xml文件地址</param>
        /// <returns>dataset对象</returns>
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
                //从stream装载到XmlTextReader
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
        ///  将DataSet转换为xml对象字符串
        /// </summary>
        /// <param name="xmlDS">dataset对象</param>
        /// <returns>xml字符串</returns>
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
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
        /// 将数据集转换为ptName指定的格式存储于xmlFile路径中
        /// </summary>
        /// <param name="xmlDS">需要转换的数据</param>
        /// <param name="xmlFile">输出路径</param>
        /// <param name="sqlgetlayout">获取各式</param>
        /// <param name="doc">xml文件</param>
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
        /// 将xml数据集转转化为xml文件
        /// </summary>
        /// <param name="dsxml">数据集</param>
        /// <param name="xmlFile">路径</param>
        /// <returns>返回字符串</returns>
        public static string ConverDataSetToXMLFile(DataSet dsxml, string xmlFile)
        {
            try
            {
                StringBuilder strxml = new StringBuilder(ConvertDataSetToXML(dsxml));
                StreamWriter sw = new StreamWriter(xmlFile);
                strxml.Replace("NewDataSet", "第一层");
                strxml.Replace("ds", "第二层");
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
        /// 将DataSet转换为xml文件
        /// </summary>
        /// <param name="xmlDS">dtaset对象</param>
        /// <param name="xmlFile">文件输出路径</param>
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
                //返回Unicode编码的文本
                UnicodeEncoding utf = new UnicodeEncoding();
                StreamWriter sw = new StreamWriter(xmlFile);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine(utf.GetString(arr).Trim());
                sw.Close();
            }
        }
        #endregion

        #region 操作数据库

        ///// <summary>
        ///// 替换sql语句参数，并给cmd赋值,绑定变量
        ///// </summary>
        ///// <param name="p_strSql">sql语句</param>
        ///// <param name="p_dictParam">参数字典</param>
        ///// <param name="p_oleCmd">cmd</param>
        ///// <returns>返回是否替换参数成功</returns>
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
        /// 执行查询操作
        /// </summary>
        /// <param name="p_strSql">sql</param>
        /// <param name="p_strTablename">表名</param>
        /// <param name="p_strConnectionStringName">连接串名</param>
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
        /// OracleConnection执行查询操作
        /// </summary>
        /// <param name="p_strSql">sql</param>
        /// <param name="p_strTablename">表名</param>
        /// <param name="p_strConnectionStringName">连接串名</param>
        /// <returns></returns>
        public static DataTable ExecuteBySQL(string p_strSql, string p_strTablename, string p_strConnectionStringName)
        {
            DataTable _dtTable = new DataTable(p_strTablename);
            String _strConnectionString = ConfigurationManager.ConnectionStrings[p_strConnectionStringName].ConnectionString;
            using (OracleConnection _oleConn = new OracleConnection(_strConnectionString))
            {
                try
                {
                    OracleCommand _oleCmd = _oleConn.CreateCommand();
                    _oleConn.Open();
                    _oleCmd.CommandText = p_strSql;
                    OracleDataAdapter adapter = new OracleDataAdapter(_oleCmd);
                    adapter.Fill(_dtTable);
                }
                catch (Exception exp)
                {
                    WriteError("DB", exp.Message);
                    return null;
                }
            }
            return _dtTable;
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="p_strSql">sql</param>
        /// <param name="p_strConnectionStringName">连接串名</param>
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
        /// 执行增删改操作
        /// </summary>
        /// <param name="p_strSql">sql</param>
        /// <param name="p_strConnectionStringName">连接串名</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string p_strSql, string p_strConnectionStringName)
        {
            int _iExeCount = 0;
            String _strConnectionString = ConfigurationManager.ConnectionStrings[p_strConnectionStringName].ConnectionString;
            using (OracleConnection _oleConn = new OracleConnection(_strConnectionString))
            {
                OracleCommand _oleCmd = _oleConn.CreateCommand();
                try
                {
                    _oleConn.Open();
                    _oleCmd.CommandText = p_strSql;
                    _iExeCount = _oleCmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    WriteError("DB", exp.Message);
                }
            }
            return _iExeCount;
        }


        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="dictionary">字典参数</param>
        /// <param name="tablename">产生的datatable名称</param>
        /// <param name="cmd">cmd</param>
        /// <returns>返回表</returns>
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
        /// 执行增，删，改操作
        /// </summary>
        /// <param name="sql">操作的sql</param>
        /// <param name="dictionary">字典参数</param>
        /// <param name="cmd">cmd</param>
        /// <returns>返回结果</returns>
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
        /// 通过APP.config文件获取连接数据库字符串
        /// </summary>
        /// <returns></returns>
        private static string GetConnString()
        {
            string strconn = System.Configuration.ConfigurationSettings.AppSettings["StrConn"].ToString();
            return strconn;
        }

        /// <summary>
        /// 替换sql语句参数，并给cmd赋值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dictionary">参数字典</param>
        /// <param name="cmd">cmd</param>
        /// <returns>返回是否替换参数成功</returns>
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

        #region 检查是否为空
        public static DataTable CheckNULL(DataTable source)
        {
            if (source == null)
            {
                MessageBox.Show("数据源为空");
                return null;
            }
            else
            {
                return source;
            }
        }
        #endregion

        #region 动态生成菜单
        /// <summary>
        /// 动态创建菜单
        /// </summary>
        private void CreateMenu(MenuStrip MainMenuStrip, Form parient_form)
        {
            //定义一个主菜单
            MenuStrip mainMenu = new MenuStrip();
            DataSet ds = new DataSet();
            //从XML中读取数据。数据结构后面详细讲一下。
            ds.ReadXml(@"..\..\Menu.xml");
            DataView dv = ds.Tables[0].DefaultView;
            //通过DataView来过滤数据首先得到最顶层的菜单
            dv.RowFilter = "ParentItemID=0";
            for (int i = 0; i < dv.Count; i++)
            {
                //创建一个菜单项
                ToolStripMenuItem topMenu = new ToolStripMenuItem();
                //给菜单赋Text值。也就是在界面上看到的值。
                topMenu.Text = dv[i]["Text"].ToString();
                //如果是有下级菜单则通过CreateSubMenu方法来创建下级菜单
                if (Convert.ToInt16(dv[i]["IsModule"]) == 1)
                {
                    //以ref的方式将顶层菜单传递参数，因为他可以在赋值后再回传。－－也许还有更好的方法^_^.
                    CreateSubMenu(ref topMenu, Convert.ToInt32(dv[i]["ItemID"]), ds.Tables[0]);
                }
                //显示应用程序中已打开的 MDI 子窗体列表的菜单项
                mainMenu.MdiWindowListItem = topMenu;
                //将递归附加好的菜单加到菜单根项上。
                mainMenu.Items.Add(topMenu);
            }
            mainMenu.Dock = DockStyle.Top;
            //将窗体的MainMenuStrip梆定为mainMenu.
            MainMenuStrip = mainMenu;
            //这句很重要。如果不写这句菜单将不会出现在主窗体中。
            parient_form.Controls.Add(mainMenu);
        }

        /// <summary>
        /// 创建子菜单
        /// </summary>
        /// <param name="topMenu">父菜单项</param>
        /// <param name="ItemID">父菜单的ID</param>
        /// <param name="dt">所有菜单数据集</param>
        private void CreateSubMenu(ref ToolStripMenuItem topMenu, int ItemID, DataTable dt)
        {
            DataView dv = new DataView(dt);
            //过滤出当前父菜单下在所有子菜单数据(仅为下一层的)
            dv.RowFilter = "ParentItemID=" + ItemID.ToString();

            for (int i = 0; i < dv.Count; i++)
            {
                //创建子菜单项
                ToolStripMenuItem subMenu = new ToolStripMenuItem();
                subMenu.Text = dv[i]["Text"].ToString();
                //如果还有子菜单则继续递归加载。
                if (Convert.ToInt16(dv[i]["IsModule"]) == 1)
                {
                    //递归调用
                    CreateSubMenu(ref subMenu, Convert.ToInt32(dv[i]["ItemID"]), dt);
                }
                else
                {
                    //扩展属性可以加任何想要的值。这里用formName属性来加载窗体。
                    subMenu.Tag = dv[i]["FormName"].ToString();
                    //给没有子菜单的菜单项加事件。
                    subMenu.Click += new EventHandler(subMenu_Click);
                }
                if (dv[i]["ImageName"].ToString().Length > 0)
                {
                    //设置菜单项前面的图票为16X16的图片文件。
                    Image img = Image.FromFile(@"..\..\Image\" + dv[i]["ImageName"].ToString());
                    subMenu.Image = img;
                    subMenu.Image.Tag = dv[i]["ImageName"].ToString();
                }
                //将菜单加到顶层菜单下。
                topMenu.DropDownItems.Add(subMenu);
            }
        }

        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void subMenu_Click(object sender, EventArgs e)
        {
            //tag属性在这里有用到。
            //string formName = ((ToolStripMenuItem)sender).Tag.ToString();
            //CreateFormInstance(formName);
        }

        /// <summary>
        /// 创建form实例。
        /// </summary>
        /// <param name="formName">form的类名</param>
        private void CreateFormInstance(Form form, string formName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < form.MdiChildren.Length; i++)
            {
                //如果所点的窗口被打开则重新激活
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
                //如果不存在则用反射创建form窗体实例。
                Assembly asm = Assembly.Load("Fastyou.BookShop.Win");//程序集名
                object frmObj = asm.CreateInstance("Fastyou.BookShop.Win." + formName);//程序集+form的类名。
                Form frms = (Form)frmObj;
                //tag属性要重新写一次，否则在第二次的时候取不到。原因还不清楚。有知道的望告知。
                frms.Tag = formName.ToString();
                frms.MdiParent = form;
                frms.Show();
            }
        }
        #endregion

        #region 文件操作 2015-01-05

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="p_strSourcePath">源文件</param>
        /// <param name="p_strTargetPath">目标文件</param>
        /// <param name="p_bOverWrite">是否覆盖</param>
        /// <returns></returns>
        public static bool CopyFile(string p_strSourcePath, string p_strTargetPath, bool p_bOverWrite)
        {
            try
            {
                String sourcePath = p_strSourcePath;
                String targetPath = p_strTargetPath;
                bool isrewrite = p_bOverWrite; // true=覆盖已存在的同名文件,false则反之
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
        public static void SaveConfig(string p_strKey, string p_strValue)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //写入<add>元素的Value
            config.AppSettings.Settings[p_strKey].Value = p_strValue;
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }
}
