/*----------------------------------------------------------------
            // Copyright (C) 2010 嘉和美康信息技术有限公司
            // 版权所有。 
            //
            // 文件名：ExportDBF
            // 文件功能描述：实现将标准数据转化成DBF后对数据的操作
            //
            // 
            // 创建标识：吴海龙 2015-01-15
            //
            // 修改标识：
            // 修改描述：
            //
            // 修改标识：
            // 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Net;
using System.Data.OleDb;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ToolFunction;


namespace DataExport
{
    public class ExportDBF : IExport
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private string m_strDbfTemplet = string.Empty;
        private DataTable m_dtSource = null;

        public ExportDBF( DataTable p_dtSource)
        {
            m_dtSource = p_dtSource;
        }
        string strErrorPatInf = "";

        #region 处理DBF文件
        /// <summary>
        /// 操作DBF数据 孙奎松 2012-12-12
        /// </summary>
        /// <param name="strFileName">DBF文件全路径</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns>true：执行成功 false 执行失败</returns>
        public bool WriteDBF(string strFileName, string strSql)
        {
            try
            {
                string strPath = strFileName.Substring(0, strFileName.LastIndexOf('\\'));
                string strName = strFileName.Substring(strFileName.LastIndexOf('\\') + 1);
                string strConstr = @"Provider=VFPOLEDB.1;Data Source=" + strPath + @";Mode=Share Deny None;User ID="";Mask Password=False;Cache Authentication=False;Encrypt Password=False;Collating Sequence=MACHINE;DSN="";DELETED=True;CODEPAGE=936;MVCOUNT=16384;ENGINEBEHAVIOR=90;TABLEVALIDATE=3;REFRESH=5;VARCHARMAPPING=False;ANSI=True;REPROCESS=5;";
                using (OleDbConnection con = new OleDbConnection(strConstr))
                {
                    con.Open();
                    OleDbCommand cmdInsert = new OleDbCommand();
                    cmdInsert.Connection = con;
                    strSql = strSql.Replace("TableName", strName.Split('.')[0].ToString());
                    cmdInsert.CommandText = strSql;
                    cmdInsert.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError(ex.ToString() + "\nSQL:" + strSql);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 读取dbf文件的列
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        /// <returns>表结构</returns>
        public DataTable ReadDBF(string strFileName)
        {
            DataTable dt = new DataTable();
            try
            {
                string strPath = strFileName.Substring(0, strFileName.LastIndexOf('\\'));
                string strName = strFileName.Substring(strFileName.LastIndexOf('\\') + 1);
                string strConstr = @"Provider=VFPOLEDB.1;Data Source=" + strPath + @";Mode=Share Deny None;User ID="";Mask Password=False;Cache Authentication=False;Encrypt Password=False;Collating Sequence=MACHINE;DSN="";DELETED=True;CODEPAGE=936;MVCOUNT=16384;ENGINEBEHAVIOR=90;TABLEVALIDATE=3;REFRESH=5;VARCHARMAPPING=False;ANSI=True;REPROCESS=5;";
                using (OleDbConnection con = new OleDbConnection(strConstr))
                {
                    con.Open();
                    string sql = @"select * from  " + strName.Split('.')[0].ToString() + "  where 1=0";
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError(exp.Message);
                return null;
            }
            return null;
        }
        /// <summary>
        /// 读取dbf文件的列
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        /// <returns>返回所有列的字符拼接，以逗号相隔</returns>
        public string ReColumnStr(string strFileName)
        {
            string strcolumns = "";
            try
            {
                DataTable dt = ReadDBF(strFileName);
                foreach (DataColumn s in dt.Columns)
                {
                    strcolumns += s + ",";
                }
                strcolumns = strcolumns.Trim(',');
                return strcolumns;
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }


        /// <summary>
        /// 创建DBF文件 孙奎松 2012-12-15
        /// </summary>
        /// <param name="strFilePath">要保存的文件路径</param>
        /// <param name="strFileName">文件名</param>
        /// <param name="strSql">SQL（保留参数）</param>
        /// <returns>true：创建成功 false 创建失败</returns>
        public bool CreateDBF(string strFilePath, string strFileName, string strDefName, string strSql)
        {
            try
            {
                //string path = fileName.Substring(0, fileName.LastIndexOf('\\'));
                //string name = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                //string constr = @"Provider=VFPOLEDB.1;Data Source=" + path + @";Mode=Share Deny None;User ID="";Mask Password=False;Cache Authentication=False;Encrypt Password=False;Collating Sequence=MACHINE;DSN="";DELETED=True;CODEPAGE=936;MVCOUNT=16384;ENGINEBEHAVIOR=90;TABLEVALIDATE=3;REFRESH=5;VARCHARMAPPING=False;ANSI=True;REPROCESS=5;";
                ////string constr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=dBASE IV;";
                //using (OleDbConnection con = new OleDbConnection(constr))
                //{
                //    con.Open();
                //    OleDbCommand cmd = null;
                //    cmd = new OleDbCommand(@"create table " + name + " (id int)", con);
                //    cmd.ExecuteNonQuery();
                //    con.Close();
                //}
                if (File.Exists(@"DBFTempletTwo\\" + strFileName))
                {
                    File.Copy(@"DBFTempletTwo\\" + strFileName, strFilePath + "\\" + strDefName);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 处理ZIP文件
        /// <summary>
        /// 压缩目录 孙奎松 2012-12-16
        /// </summary>
        /// <param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后的文件名，全路径格式</param>
        /// <returns>true：压缩成功 false 压缩失败</returns>
        //public bool ZipFileDictory(string strFolderToZip, string strZipedFile)
        //{
        //    bool res;
        //    if (!Directory.Exists(strFolderToZip))
        //    {
        //        return false;
        //    }

        //    ZipOutputStream s = new ZipOutputStream(File.Create(strZipedFile));
        //    s.SetLevel(6);

        //    res = ZipFileDictory(strFolderToZip, s, "");

        //    s.Finish();
        //    s.Close();
        //    return res;
        //}
        /// <summary>
        /// 递归压缩文件夹方法 孙奎松 2012-12-16
        /// </summary>
        /// <param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="s">ZipOutputStream对象</param>
        /// <param name="ParentFolderName">父级目录</param>
        /// <returns>true：成功 false 失败</returns>
        //private bool ZipFileDictory(string strFolderToZip, ZipOutputStream s, string strParentFolderName)
        //{
        //    bool res = true;
        //    string[] folders, filenames;
        //    ZipEntry entry = null;
        //    FileStream fs = null;
        //    Crc32 crc = new Crc32();

        //    try
        //    {

        //        //创建当前文件夹
        //        //entry = new ZipEntry(Path.Combine(ParentFolderName, "/"));  //加上 “/” 才会当成是文件夹创建
        //        //s.PutNextEntry(entry);
        //        s.Flush();


        //        //先压缩文件，再递归压缩文件夹 
        //        filenames = Directory.GetFiles(strFolderToZip);
        //        foreach (string file in filenames)
        //        {
        //            //打开压缩文件
        //            fs = File.OpenRead(file);

        //            byte[] buffer = new byte[fs.Length];
        //            fs.Read(buffer, 0, buffer.Length);
        //            entry = new ZipEntry(Path.Combine(strParentFolderName, Path.GetFileName(file)));
        //            entry.DateTime = DateTime.Now;
        //            entry.Size = fs.Length;
        //            fs.Close();

        //            crc.Reset();
        //            crc.Update(buffer);

        //            entry.Crc = crc.Value;

        //            s.PutNextEntry(entry);

        //            s.Write(buffer, 0, buffer.Length);
        //        }
        //    }
        //    catch
        //    {
        //        res = false;
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //            fs = null;
        //        }
        //        if (entry != null)
        //        {
        //            entry = null;
        //        }
        //        GC.Collect();
        //        GC.Collect(1);
        //    }


        //    folders = Directory.GetDirectories(strFolderToZip);
        //    foreach (string folder in folders)
        //    {
        //        if (!ZipFileDictory(folder, s, Path.Combine(strParentFolderName, Path.GetFileName(strFolderToZip))))
        //        {
        //            return false;
        //        }
        //    }

        //    return res;
        //}
        #endregion

        #region 上传文件到医管司平台
        /// <summary>
        /// 发送Token请求 孙奎松 2012-12-17
        /// </summary>
        /// <param name="URL">请求地址</param>
        /// <returns>返回Token值</returns>
        public string GetToken(string strUrl)
        {
            string sReturn = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);
                req.Timeout = 10000; //超时时间    
                req.CookieContainer = new CookieContainer();
                req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                req.ContentType = "multipart/form-data";
                WebResponse resp = req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                sReturn = sr.ReadToEnd().Trim();
                resp.Close();
                sr.Close();
            }
            catch
            {

            }
            return sReturn;
        }
        /// <summary>
        /// 发送数据请求 孙奎松 2012-12-17
        /// </summary>
        /// <param name="file">ZIP文件全路径</param>
        /// <param name="url">数据传送请求地址</param>
        /// <returns>返回上传日志</returns>
        public string PostFile(string strFile, string strUrl)
        {
            //请求 
            WebRequest req = WebRequest.Create(strUrl);
            req.Method = "POST";
            req.ContentType = "multipart/form-data";
            FileStream fileStream = new FileStream(strFile, FileMode.Open, FileAccess.Read);
            //post总长度 
            long length = fileStream.Length;
            req.ContentLength = length;
            Stream requestStream = req.GetRequestStream();
            //文件内容 
            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);
            //响应 
            WebResponse pos = req.GetResponse();
            StreamReader sr = new StreamReader(pos.GetResponseStream(), Encoding.UTF8);
            string strResponse = sr.ReadToEnd().Trim();
            sr.Close();
            if (pos != null)
            {
                pos.Close();
                pos = null;
            }
            if (req != null)
            {
                req = null;
            }
            return strResponse;
        }
        #endregion

        #region 向DBF中插入数据

        
        #endregion

        #region 操作配置文件
        /// <summary>
        /// 写入INI文件 孙奎松 2012-12-17
        /// </summary>
        /// <param name="strSection">项目名称(如 [TypeName] )</param>
        /// <param name="strKey">键</param>
        /// <param name="strValue">值</param>
        /// <param name="strIniPath">INI文件路径</param>
        /// <returns>true写入成功 false写入失败</returns>
        public bool IniWriteValue(string strSection, string strKey, string strValue,string strIniPath)
        {
            try
            {
                WritePrivateProfileString(strSection, strKey, strValue, strIniPath);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 读出INI文件  孙奎松 2012-12-17
        /// </summary>
        /// <param name="strSection">项目名称(如 [TypeName] )</param>
        /// <param name="strKey">键</param>
        /// <returns>读取出来的字符</returns>
        public string IniReadValue(string strSection, string strKey, string strIniPath)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(strSection, strKey, "", temp, 500, strIniPath);
            return temp.ToString();
        }
        /// <summary>
        /// 验证文件是否存在 孙奎松 2012-12-17
        /// </summary>
        /// <returns>true存在 false 不存在</returns>
        public bool ExistINIFile(string strIniPath)
        {
            return File.Exists(strIniPath);
        }

        /// <summary>
        /// 由datarow组装值字符串
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string ReturnStrValues(DataRow drsource)
        {
            string strSqlValues = "";
            foreach (DataColumn s in drsource.Table.Columns)
            {
                if (s.DataType.Name == "String") //拼接字符串类型
                {
                    strSqlValues += "'" + drsource[s].ToString() + "',";
                }
                //else if (s.DataType.Name.Contains("Int"))//拼接数字类型
                //{
                //    if (""!=drsource[s].ToString())
                //    {
                //        strSqlValues += drsource[s].ToString() + ",";
                //    }
                //    else
                //    {
                //        strSqlValues += "null,";
                //    }
                //}
                //else if (s.DataType.Name == "DateTime")//拼接日期类型
                //{
                //    strSqlValues += GetDate(drsource[s].ToString()) + ",";
                //}
                else
                {
                    strSqlValues += "null,";
                }
            }
            return strSqlValues.Trim(','); ;

        }

       /// <summary>
       /// 2015-01-15 吴海龙 根据【本地】列的数据类型，返回拼接values部分的字符串
       /// </summary>
       /// <param name="drsource">目标行</param>
       /// <param name="strColumn">列名</param>
       /// <returns>拼接完成的字符串</returns>
        public string ReturnStrValues(DataRow drsource, string strColumn)
        {
            DataTable dt = drsource.Table;
            string strSqlValues = "";
            foreach (string s in strColumn.Split(','))
            {
                if (dt.Columns.Contains(s))
                {
                    if (dt.Columns[s].DataType.Name == "String") //拼接字符串类型
                    {
                        string _strTemp = drsource[s].ToString();
                        if (_strTemp.Contains("'"))
                        {
                            _strTemp = _strTemp.Replace("'", "''");
                        }
                        strSqlValues += "'" + _strTemp + "',";
                    }
                    else if (dt.Columns[s].DataType.Name.Contains("Int"))//拼接数字类型
                    {
                        strSqlValues += drsource[s].ToString() + ",";
                    }
                    else if (dt.Columns[s].DataType.Name == "DateTime")//拼接日期类型
                    {
                        strSqlValues += GetDate(drsource[s].ToString()) + ",";
                    }
                    else
                    {
                        strSqlValues += "null,";
                    }
                }
                else
                {
                    strSqlValues += "null,";
                }
            }
            return strSqlValues.Trim(','); ;
        }


        /// <summary>
        /// 2015-01-15 吴海龙 根据【本地】列的数据类型，返回拼接values部分的字符串
        /// </summary>
        /// <param name="drsource">目标行</param>
        /// <param name="strColumn">列名</param>
        /// <returns>拼接完成的字符串</returns>
        public string ReturnStrValues(DataRow drsource, DataTable p_dtDbf)
        {
            DataTable dt = drsource.Table;
            string strSqlValues = "";
            foreach (DataColumn _dc in p_dtDbf.Columns)
            {
                if (dt.Columns.Contains(_dc.ColumnName))
                {
                    string _strTemp = drsource[_dc.ColumnName.ToUpper()].ToString();
                    if (_dc.DataType.Name == "String") //拼接字符串类型
                    {
                        if (_strTemp != null && _strTemp != "")
                        {
                            strSqlValues += "'" + _strTemp + "',";
                        }
                        else
                        {
                            strSqlValues += "null,";
                        }
                    }
                    else if (_dc.DataType.Name.Contains("Int") || _dc.DataType.Name.Contains("Decimal") || _dc.DataType.Name.Contains("Double"))//拼接数字类型
                    {
                        if (_strTemp != null && _strTemp != "")
                        {
                            strSqlValues += drsource[_dc].ToString() + ",";
                        }
                        else
                        {
                            strSqlValues += "null,";
                        }

                    }
                    else if (_dc.DataType.Name == "DateTime")//拼接日期类型
                    {
                        if (_strTemp != null && _strTemp != "")
                        {
                            strSqlValues += GetDate(drsource[_dc].ToString()) + ",";
                        }
                        else
                        {
                            strSqlValues += GetDate("1949-01-01") + ",";
                            //strSqlValues += "1949-01-01,";
                        }
                    }
                    else
                    {
                        string _s = _dc.DataType.Name;
                        strSqlValues += "null,";
                    }
                }
                else
                {
                    strSqlValues += "null,";
                }
            }
            int _p = strSqlValues.Trim(',').Split(',').Length;
            return strSqlValues.Trim(','); ;
        }
        

        /// <summary>
        /// 2015-01-14 吴海龙 向dbf文件插入数据 
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="strFileName">文件路径</param>
        /// <returns>插入是否成功</returns>
        public bool InsertDBF(DataTable p_dtSource, string p_strTempletFileFullName)
        {
            try
            {
                string _strFileName = p_strTempletFileFullName.Substring(p_strTempletFileFullName.LastIndexOf('\\') + 1);
                string _strTarget = CommonFunction.GetConfig("DbfOutPutDir") + "\\" + _strFileName;
                CommonFunction.CopyFile(p_strTempletFileFullName, _strTarget,false);
                string strColumn = GetColumnStr(_strTarget);//存放要插入的字段名
                DataTable dtColumn = ReadDBF(_strTarget);//存放要插入的字段名
                CommonFunction cf = new CommonFunction();
                foreach (DataRow dr in p_dtSource.Rows)
                {
                    string strsql = "insert into TableName( ";//sql的开头
                    strsql += strColumn;
                    strsql += ") values (";
                    strsql += ReturnStrValues(dr, dtColumn);
                    //strsql += ReturnStrValues(dr, strColumn);
                    strsql += ")";
                    if (WriteDBF(_strTarget, strsql))
                    {
                        PublicVar.m_nSuccessCount++;
                        RemoteMessage.SendMessage("插入成功...");
                    }
                    else
                    {
                        PublicVar.m_nFalseCount++;
                        RemoteMessage.SendMessage("插入失败...");
                    }
                }
                
                //string mess = "" + DateTime.Now.ToString() + "数据导出完成\n导出成功" + successcount.ToString() + "条； \n 导出失败" + falsecount.ToString() + "条； \n 详情信息查看Errorlog.txt!";
                //CommonFunction.WriteError(mess + "\n");
                //ToolFunction.uctlMessageBox.frmDisappearShow(mess);
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError(ex.Message);
            }
            return true;
        }

        private string GetColumnStr(string _strTarget)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 将日期转换成DBF识别的格式 孙奎松 2012-12-16
        /// </summary>
        /// <param name="strDate">日期</param>
        /// <returns>DBF识别的日期格式</returns>
        public string GetDate(string strDate)
        {
            if (strDate.Trim().Length == 0)
            {
                return "";
            }
            DateTime dt = Convert.ToDateTime(strDate);
            return "DateTime(" + dt.Year.ToString() + "," + dt.Month.ToString().PadLeft(2, '0') + "," + dt.Day.ToString().PadLeft(2, '0') + ")";
        }
        #endregion

        /// <summary>
        /// 获取日志 孙奎松 2012-12-24
        /// 最后修改：冯梦龙 2013-3-6
        /// 修改内容：设置获取结果等待时间为1分钟
        /// </summary>
        /// <param name="URL">请求地址</param>
        /// <returns>日志信息</returns>
        public string GetLog(string strUrl)
        {
            string sReturn = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);
            req.Timeout = 10000; //超时时间    
            req.CookieContainer = new CookieContainer();
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            req.ContentType = "multipart/form-data";
            WebResponse resp = req.GetResponse();
            Thread.Sleep(60000);
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
            sReturn = sr.ReadToEnd().Trim();
            resp.Close();
            sr.Close();
            return sReturn;
        }

        /// <summary>
        /// 删除指定目录下的指定后缀名的文件 孙奎松 2012-12-24
        /// </summary>
        /// <param name="directory">要删除的文件所在的目录，是绝对目录，如d:\temp</param>
        /// <param name="masks">要删除的文件的后缀名的一个数组，比如masks中包含了.cs,.vb,.c这三个元素</param>
        /// <param name="searchSubdirectories">表示是否需要递归删除，即是否也要删除子目录中相应的文件</param>
        /// <param name="ignoreHidden">表示是否忽略隐藏文件</param>
        /// <param name="deletedFileCount">表示总共删除的文件数</param>
        public void DeleteFiles(string directory, string[] masks, bool searchSubdirectories, bool ignoreHidden, ref int deletedFileCount)
        {
            //先删除当前目录下指定后缀名的所有文件
            foreach (string file in Directory.GetFiles(directory, "*.*"))
            {
                if (!(ignoreHidden && (File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    foreach (string mask in masks)
                    {
                        if (Path.GetExtension(file) == mask)
                        {
                            File.Delete(file);
                            deletedFileCount++;
                        }
                    }
                }
            }

            //如果需要对子目录进行处理，则对子目录也进行递归操作
            if (searchSubdirectories)
            {
                string[] childDirectories = Directory.GetDirectories(directory);
                foreach (string dir in childDirectories)
                {
                    if (!(ignoreHidden && (File.GetAttributes(dir) & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        DeleteFiles(dir, masks, searchSubdirectories, ignoreHidden, ref deletedFileCount);
                    }
                }
            }
        }

        /// <summary>
        /// 返回dbf路径
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <returns></returns>
        public string GetDbfTempletPath(string p_strObjectName) {
            string _strSQL = string.Format("select * from PT_TABLES_DICT where TABLE_NAME ='{0}' ", p_strObjectName);
            DataTable _dtobj = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtobj!=null&&_dtobj.Rows.Count>0)
            {
                return _dtobj.Rows[0]["ms"].ToString();
            }
            return "";
        }

        #region IExport 成员

        public void Export()
        {
            string _strDbfTempletPath = GetDbfTempletPath(m_dtSource.TableName);
            InsertDBF(m_dtSource, _strDbfTempletPath);
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
