/*----------------------------------------------------------------
            // Copyright (C) 2010 �κ�������Ϣ�������޹�˾
            // ��Ȩ���С� 
            //
            // �ļ�����ExportDBF
            // �ļ�����������ʵ�ֽ���׼����ת����DBF������ݵĲ���
            //
            // 
            // ������ʶ���⺣�� 2015-01-15
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
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

        #region ����DBF�ļ�
        /// <summary>
        /// ����DBF���� ����� 2012-12-12
        /// </summary>
        /// <param name="strFileName">DBF�ļ�ȫ·��</param>
        /// <param name="strSql">SQL���</param>
        /// <returns>true��ִ�гɹ� false ִ��ʧ��</returns>
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
        /// ��ȡdbf�ļ�����
        /// </summary>
        /// <param name="strFileName">�ļ�·��</param>
        /// <returns>��ṹ</returns>
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
        /// ��ȡdbf�ļ�����
        /// </summary>
        /// <param name="strFileName">�ļ�·��</param>
        /// <returns>���������е��ַ�ƴ�ӣ��Զ������</returns>
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
        /// ����DBF�ļ� ����� 2012-12-15
        /// </summary>
        /// <param name="strFilePath">Ҫ������ļ�·��</param>
        /// <param name="strFileName">�ļ���</param>
        /// <param name="strSql">SQL������������</param>
        /// <returns>true�������ɹ� false ����ʧ��</returns>
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

        #region ����ZIP�ļ�
        /// <summary>
        /// ѹ��Ŀ¼ ����� 2012-12-16
        /// </summary>
        /// <param name="FolderToZip">��ѹ�����ļ��У�ȫ·����ʽ</param>
        /// <param name="ZipedFile">ѹ������ļ�����ȫ·����ʽ</param>
        /// <returns>true��ѹ���ɹ� false ѹ��ʧ��</returns>
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
        /// �ݹ�ѹ���ļ��з��� ����� 2012-12-16
        /// </summary>
        /// <param name="FolderToZip">��ѹ�����ļ��У�ȫ·����ʽ</param>
        /// <param name="s">ZipOutputStream����</param>
        /// <param name="ParentFolderName">����Ŀ¼</param>
        /// <returns>true���ɹ� false ʧ��</returns>
        //private bool ZipFileDictory(string strFolderToZip, ZipOutputStream s, string strParentFolderName)
        //{
        //    bool res = true;
        //    string[] folders, filenames;
        //    ZipEntry entry = null;
        //    FileStream fs = null;
        //    Crc32 crc = new Crc32();

        //    try
        //    {

        //        //������ǰ�ļ���
        //        //entry = new ZipEntry(Path.Combine(ParentFolderName, "/"));  //���� ��/�� �Żᵱ�����ļ��д���
        //        //s.PutNextEntry(entry);
        //        s.Flush();


        //        //��ѹ���ļ����ٵݹ�ѹ���ļ��� 
        //        filenames = Directory.GetFiles(strFolderToZip);
        //        foreach (string file in filenames)
        //        {
        //            //��ѹ���ļ�
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

        #region �ϴ��ļ���ҽ��˾ƽ̨
        /// <summary>
        /// ����Token���� ����� 2012-12-17
        /// </summary>
        /// <param name="URL">�����ַ</param>
        /// <returns>����Tokenֵ</returns>
        public string GetToken(string strUrl)
        {
            string sReturn = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);
                req.Timeout = 10000; //��ʱʱ��    
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
        /// ������������ ����� 2012-12-17
        /// </summary>
        /// <param name="file">ZIP�ļ�ȫ·��</param>
        /// <param name="url">���ݴ��������ַ</param>
        /// <returns>�����ϴ���־</returns>
        public string PostFile(string strFile, string strUrl)
        {
            //���� 
            WebRequest req = WebRequest.Create(strUrl);
            req.Method = "POST";
            req.ContentType = "multipart/form-data";
            FileStream fileStream = new FileStream(strFile, FileMode.Open, FileAccess.Read);
            //post�ܳ��� 
            long length = fileStream.Length;
            req.ContentLength = length;
            Stream requestStream = req.GetRequestStream();
            //�ļ����� 
            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);
            //��Ӧ 
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

        #region ��DBF�в�������

        
        #endregion

        #region ���������ļ�
        /// <summary>
        /// д��INI�ļ� ����� 2012-12-17
        /// </summary>
        /// <param name="strSection">��Ŀ����(�� [TypeName] )</param>
        /// <param name="strKey">��</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="strIniPath">INI�ļ�·��</param>
        /// <returns>trueд��ɹ� falseд��ʧ��</returns>
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
        /// ����INI�ļ�  ����� 2012-12-17
        /// </summary>
        /// <param name="strSection">��Ŀ����(�� [TypeName] )</param>
        /// <param name="strKey">��</param>
        /// <returns>��ȡ�������ַ�</returns>
        public string IniReadValue(string strSection, string strKey, string strIniPath)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(strSection, strKey, "", temp, 500, strIniPath);
            return temp.ToString();
        }
        /// <summary>
        /// ��֤�ļ��Ƿ���� ����� 2012-12-17
        /// </summary>
        /// <returns>true���� false ������</returns>
        public bool ExistINIFile(string strIniPath)
        {
            return File.Exists(strIniPath);
        }

        /// <summary>
        /// ��datarow��װֵ�ַ���
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string ReturnStrValues(DataRow drsource)
        {
            string strSqlValues = "";
            foreach (DataColumn s in drsource.Table.Columns)
            {
                if (s.DataType.Name == "String") //ƴ���ַ�������
                {
                    strSqlValues += "'" + drsource[s].ToString() + "',";
                }
                //else if (s.DataType.Name.Contains("Int"))//ƴ����������
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
                //else if (s.DataType.Name == "DateTime")//ƴ����������
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
       /// 2015-01-15 �⺣�� ���ݡ����ء��е��������ͣ�����ƴ��values���ֵ��ַ���
       /// </summary>
       /// <param name="drsource">Ŀ����</param>
       /// <param name="strColumn">����</param>
       /// <returns>ƴ����ɵ��ַ���</returns>
        public string ReturnStrValues(DataRow drsource, string strColumn)
        {
            DataTable dt = drsource.Table;
            string strSqlValues = "";
            foreach (string s in strColumn.Split(','))
            {
                if (dt.Columns.Contains(s))
                {
                    if (dt.Columns[s].DataType.Name == "String") //ƴ���ַ�������
                    {
                        string _strTemp = drsource[s].ToString();
                        if (_strTemp.Contains("'"))
                        {
                            _strTemp = _strTemp.Replace("'", "''");
                        }
                        strSqlValues += "'" + _strTemp + "',";
                    }
                    else if (dt.Columns[s].DataType.Name.Contains("Int"))//ƴ����������
                    {
                        strSqlValues += drsource[s].ToString() + ",";
                    }
                    else if (dt.Columns[s].DataType.Name == "DateTime")//ƴ����������
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
        /// 2015-01-15 �⺣�� ���ݡ����ء��е��������ͣ�����ƴ��values���ֵ��ַ���
        /// </summary>
        /// <param name="drsource">Ŀ����</param>
        /// <param name="strColumn">����</param>
        /// <returns>ƴ����ɵ��ַ���</returns>
        public string ReturnStrValues(DataRow drsource, DataTable p_dtDbf)
        {
            DataTable dt = drsource.Table;
            string strSqlValues = "";
            foreach (DataColumn _dc in p_dtDbf.Columns)
            {
                if (dt.Columns.Contains(_dc.ColumnName))
                {
                    string _strTemp = drsource[_dc.ColumnName.ToUpper()].ToString();
                    if (_dc.DataType.Name == "String") //ƴ���ַ�������
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
                    else if (_dc.DataType.Name.Contains("Int") || _dc.DataType.Name.Contains("Decimal") || _dc.DataType.Name.Contains("Double"))//ƴ����������
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
                    else if (_dc.DataType.Name == "DateTime")//ƴ����������
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
        /// 2015-01-14 �⺣�� ��dbf�ļ��������� 
        /// </summary>
        /// <param name="dt">����Դ</param>
        /// <param name="strFileName">�ļ�·��</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool InsertDBF(DataTable p_dtSource, string p_strTempletFileFullName)
        {
            try
            {
                string _strFileName = p_strTempletFileFullName.Substring(p_strTempletFileFullName.LastIndexOf('\\') + 1);
                string _strTarget = CommonFunction.GetConfig("DbfOutPutDir") + "\\" + _strFileName;
                CommonFunction.CopyFile(p_strTempletFileFullName, _strTarget,false);
                string strColumn = GetColumnStr(_strTarget);//���Ҫ������ֶ���
                DataTable dtColumn = ReadDBF(_strTarget);//���Ҫ������ֶ���
                CommonFunction cf = new CommonFunction();
                foreach (DataRow dr in p_dtSource.Rows)
                {
                    string strsql = "insert into TableName( ";//sql�Ŀ�ͷ
                    strsql += strColumn;
                    strsql += ") values (";
                    strsql += ReturnStrValues(dr, dtColumn);
                    //strsql += ReturnStrValues(dr, strColumn);
                    strsql += ")";
                    if (WriteDBF(_strTarget, strsql))
                    {
                        PublicVar.m_nSuccessCount++;
                        RemoteMessage.SendMessage("����ɹ�...");
                    }
                    else
                    {
                        PublicVar.m_nFalseCount++;
                        RemoteMessage.SendMessage("����ʧ��...");
                    }
                }
                
                //string mess = "" + DateTime.Now.ToString() + "���ݵ������\n�����ɹ�" + successcount.ToString() + "���� \n ����ʧ��" + falsecount.ToString() + "���� \n ������Ϣ�鿴Errorlog.txt!";
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

        #region ��������
        /// <summary>
        /// ������ת����DBFʶ��ĸ�ʽ ����� 2012-12-16
        /// </summary>
        /// <param name="strDate">����</param>
        /// <returns>DBFʶ������ڸ�ʽ</returns>
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
        /// ��ȡ��־ ����� 2012-12-24
        /// ����޸ģ������� 2013-3-6
        /// �޸����ݣ����û�ȡ����ȴ�ʱ��Ϊ1����
        /// </summary>
        /// <param name="URL">�����ַ</param>
        /// <returns>��־��Ϣ</returns>
        public string GetLog(string strUrl)
        {
            string sReturn = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);
            req.Timeout = 10000; //��ʱʱ��    
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
        /// ɾ��ָ��Ŀ¼�µ�ָ����׺�����ļ� ����� 2012-12-24
        /// </summary>
        /// <param name="directory">Ҫɾ�����ļ����ڵ�Ŀ¼���Ǿ���Ŀ¼����d:\temp</param>
        /// <param name="masks">Ҫɾ�����ļ��ĺ�׺����һ�����飬����masks�а�����.cs,.vb,.c������Ԫ��</param>
        /// <param name="searchSubdirectories">��ʾ�Ƿ���Ҫ�ݹ�ɾ�������Ƿ�ҲҪɾ����Ŀ¼����Ӧ���ļ�</param>
        /// <param name="ignoreHidden">��ʾ�Ƿ���������ļ�</param>
        /// <param name="deletedFileCount">��ʾ�ܹ�ɾ�����ļ���</param>
        public void DeleteFiles(string directory, string[] masks, bool searchSubdirectories, bool ignoreHidden, ref int deletedFileCount)
        {
            //��ɾ����ǰĿ¼��ָ����׺���������ļ�
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

            //�����Ҫ����Ŀ¼���д��������Ŀ¼Ҳ���еݹ����
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
        /// ����dbf·��
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

        #region IExport ��Ա

        public void Export()
        {
            string _strDbfTempletPath = GetDbfTempletPath(m_dtSource.TableName);
            InsertDBF(m_dtSource, _strDbfTempletPath);
        }

        #endregion

        #region IExport ��Ա


        public void LogFalse(List<string> p_list)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IExport ��Ա


        public string SynSQL(string p_strObjName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
