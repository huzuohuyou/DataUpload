using System;
using System.Collections.Generic;
using System.Text;
using ToolFunction;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
namespace DataExport
{
    public class ExportExcel : IExport
    {
        private DataTable m_dtSource = null;
        #region IExport ��Ա

        public void Export()
        {
            string _strDbfTempletPath = GetExcelTempletPath(m_dtSource.TableName);
            InsertExcel(m_dtSource, _strDbfTempletPath);
        }

        /// <summary>
        /// ����dbf·��
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <returns></returns>
        public string GetExcelTempletPath(string p_strObjectName)
        {
            string _strSQL = string.Format("select * from PT_TABLES_DICT where TABLE_NAME ='{0}' ", p_strObjectName);
            DataTable _dtobj = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtobj != null && _dtobj.Rows.Count > 0)
            {
                return _dtobj.Rows[0]["ms"].ToString();
            }
            return "";
        }

        #endregion

        public ExportExcel() { }

        public ExportExcel(DataTable p_dtSource)
        {
            m_dtSource = p_dtSource;
        }

        /// <summary>
        /// �����ݼ�������ΪExcel��������ҪExcelģ��
        /// </summary>
        /// <param name="name">����excel������</param>
        /// <param name="ds">�����������ݼ�</param>
        public static void AddExcel(string name, DataTable dt)
        {
            string fileName = name;
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                    return;
                }
            }
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

        /// <summary>
        /// �����ݼ����룬��ҪExcelģ��
        /// </summary>
        /// <param name="name">����excel������</param>
        /// <param name="ds">�����������ݼ�</param>
        public static void InsertIntoExcel(string filename, DataTable dt)
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
         "Data Source=" + filename + ";" +
         "Extended Properties=Excel 8.0;";

            #region ����TestSheet������
            //string sqlCreate = "CREATE TABLE TestSheet ("
            //    + "[��������] VarChar,"
            //    + "[ҽ�Ƹ��ѷ�ʽ] VarChar,"
            //    + "[��������] VarChar,"
            //    + "[סԺ����] VarChar,"
            //    + "[������] VarChar,"
            //    + "[����] VarChar,"
            //    + "[�Ա�] VarChar,"
            //    + "[��������] VarChar,"
            //    + "[����] VarChar,"
            //    + "[����] VarChar,"
            //    + "[����(��)] VarChar,"
            //    + "[��������������(g)] VarChar,"
            //    + "[��������Ժ����(g)] VarChar,"
            //    + "[������] VarChar,"
            //    + "[����] VarChar,"
            //    + "[����] VarChar,"
            //    + "[���֤��] VarChar,"
            //    + "[ְҵ] VarChar,"
            //    + "[����] VarChar,"
            //    + "[��סַ] VarChar,"
            //    + "[��סַ�绰] VarChar,"
            //    + "[��סַ�ʱ�] VarChar,"
            //    + "[���ڵ�ַ] VarChar,"
            //    + "[���ڵ�ַ�ʱ�] VarChar,"
            //    + "[������λ����ַ] VarChar,"
            //    + "[��λ�绰] VarChar,"
            //    + "[��ϵ������] VarChar,"
            //    + "[��ϵ] VarChar,"
            //    + "[��ַ] VarChar,"
            //    + "[�绰] VarChar,"
            //    + "[��Ժ;��] VarChar,"
            //    + "[�������] VarChar,"
            //    + "[��Ժ����] VarChar,"
            //    + "[��Ժʱ��(ʱ)] VarChar,"
            //    + "[��Ժ�Ʊ�] VarChar,"
            //    + "[��Ժ����] VarChar,"
            //    + "[ת�ƿƱ�] VarChar,"
            //    + "[��Ժ����] VarChar,"
            //    + "[��Ժʱ��(ʱ)] VarChar,"
            //    + "[��Ժ�Ʊ�] VarChar,"
            //    + "[��Ժ����] VarChar,"
            //    + "[ʵ��סԺ(��)] VarChar,"
            //    + "[��(��)�����(��ҽ)] VarChar,"
            //    + "[��ҽ��ϼ�������] VarChar,"
            //    + "[��(��)�����(��ҽ)] VarChar,"
            //    + "[��ҽ��ϼ�������] VarChar,"
            //    + "[ʵʩ�ٴ�·��] VarChar,"
            //    + "[ʹ��ҽ�ƻ�����ҩ�Ƽ�] VarChar,"
            //    + "[ʹ����ҽ�����豸] VarChar,"
            //    + "[ʹ����ҽ���Ƽ���] VarChar,"
            //    + "[��֤ʩ��] VarChar,"
            //    + "[��ҽ��Ժ���] VarChar,"
            //    + "[��ҽ��������] VarChar,"
            //    + "[��ҽ��Ժ����] VarChar,"
            //    + "[��ҽ��Ժ���] VarChar,"
            //    + "[��ҽ��������] VarChar,"
            //    + "[��ҽ��Ժ����] VarChar,"

            //    + "[��ҽ��Ժ���1] VarChar,"
            //    + "[��ҽ��������1] VarChar,"
            //    + "[��ҽ��Ժ����1] VarChar,"
            //    + "[��ҽ��Ժ���1] VarChar,"
            //    + "[��ҽ��������1] VarChar,"
            //    + "[��ҽ��Ժ����1] VarChar,"

            //    + "[��ҽ��Ժ���2] VarChar,"
            //    + "[��ҽ��������2] VarChar,"
            //    + "[��ҽ��Ժ����2] VarChar,"
            //    + "[��ҽ��Ժ���2] VarChar,"
            //    + "[��ҽ��������2] VarChar,"
            //    + "[��ҽ��Ժ����2] VarChar,"

            //    + "[��ҽ��Ժ���3] VarChar,"
            //    + "[��ҽ��������3] VarChar,"
            //    + "[��ҽ��Ժ����3] VarChar,"
            //    + "[��ҽ��Ժ���3] VarChar,"
            //    + "[��ҽ��������3] VarChar,"
            //    + "[��ҽ��Ժ����3] VarChar,"

            //    + "[��ҽ��Ժ���4] VarChar,"
            //    + "[��ҽ��������4] VarChar,"
            //    + "[��ҽ��Ժ����4] VarChar,"
            //    + "[��ҽ��Ժ���4] VarChar,"
            //    + "[��ҽ��������4] VarChar,"
            //    + "[��ҽ��Ժ����4] VarChar,"

            //    + "[��ҽ��Ժ���5] VarChar,"
            //    + "[��ҽ��������5] VarChar,"
            //    + "[��ҽ��Ժ����5] VarChar,"
            //    + "[��ҽ��Ժ���5] VarChar,"
            //    + "[��ҽ��������5] VarChar,"
            //    + "[��ҽ��Ժ����5] VarChar,"

            //    + "[��ҽ��Ժ���6] VarChar,"
            //    + "[��ҽ��������6] VarChar,"
            //    + "[��ҽ��Ժ����6] VarChar,"
            //    + "[��ҽ��Ժ���6] VarChar,"
            //    + "[��ҽ��������6] VarChar,"
            //    + "[��ҽ��Ժ����6] VarChar,"

            //    + "[��ҽ��Ժ���7] VarChar,"
            //    + "[��ҽ��������7] VarChar,"
            //    + "[��ҽ��Ժ����7] VarChar,"
            //    + "[��ҽ��Ժ���7] VarChar,"
            //    + "[��ҽ��������7] VarChar,"
            //    + "[��ҽ��Ժ����7] VarChar,"

            //    + "[���ˡ��ж����ⲿԭ��] VarChar,"
            //    + "[���ˡ��ж���������] VarChar,"
            //    + "[�������] VarChar,"
            //    + "[������ϼ�������] VarChar,"
            //    + "[�����] VarChar,"
            //    + "[ҩ�����] VarChar,"
            //    + "[����ҩ��] VarChar,"
            //    + "[��������ʬ��] VarChar,"
            //    + "[Ѫ��] VarChar,"
            //    + "[������] VarChar,"
            //    + "[����(������)ҽʦ] VarChar,"
            //    + "[����ҽʦ] VarChar,"
            //    + "[סԺҽʦ] VarChar,"
            //    + "[���λ�ʿ] VarChar,"
            //    + "[����ҽʦ] VarChar,"
            //    + "[ʵϰҽʦ] VarChar,"
            //    + "[����Ա] VarChar,"
            //    + "[��������] VarChar,"
            //    + "[�ʿ�ҽʦ] VarChar,"
            //    + "[�ʿػ�ʿ] VarChar,"
            //    + "[�ʿ�����] VarChar,"

            //    + "[��������������1] VarChar,"
            //    + "[��������������1] VarChar,"
            //    + "[��������1] VarChar,"
            //    + "[��������������1] VarChar,"
            //    + "[����1] VarChar,"
            //    + "[����1] VarChar,"
            //    + "[����1] VarChar,"
            //    + "[�п����ϵȼ�����1] VarChar,"
            //    + "[�п����ϵȼ�����1] VarChar,"
            //    + "[����ʽ1] VarChar,"
            //    + "[����ҽʦ1] VarChar,"

            //    + "[��������������2] VarChar,"
            //    + "[��������������2] VarChar,"
            //    + "[��������2] VarChar,"
            //    + "[��������������2] VarChar,"
            //    + "[����2] VarChar,"
            //    + "[����2] VarChar,"
            //    + "[����2] VarChar,"
            //    + "[�п����ϵȼ�����2] VarChar,"
            //    + "[�п����ϵȼ�����2] VarChar,"
            //    + "[����ʽ2] VarChar,"
            //    + "[����ҽʦ2] VarChar,"

            //    + "[��������������3] VarChar,"
            //    + "[��������������3] VarChar,"
            //    + "[��������3] VarChar,"
            //    + "[��������������3] VarChar,"
            //    + "[����3] VarChar,"
            //    + "[����3] VarChar,"
            //    + "[����3] VarChar,"
            //    + "[�п����ϵȼ�����3] VarChar,"
            //    + "[�п����ϵȼ�����3] VarChar,"
            //    + "[����ʽ3] VarChar,"
            //    + "[����ҽʦ3] VarChar,"

            //    + "[��������������4] VarChar,"
            //    + "[��������������4] VarChar,"
            //    + "[��������4] VarChar,"
            //    + "[��������������4] VarChar,"
            //    + "[����4] VarChar,"
            //    + "[����4] VarChar,"
            //    + "[����4] VarChar,"
            //    + "[�п����ϵȼ�����4] VarChar,"
            //    + "[�п����ϵȼ�����4] VarChar,"
            //    + "[����ʽ4] VarChar,"
            //    + "[����ҽʦ4] VarChar,"

            //    + "[��������������5] VarChar,"
            //    + "[��������������5] VarChar,"
            //    + "[��������5] VarChar,"
            //    + "[��������������5] VarChar,"
            //    + "[����5] VarChar,"
            //    + "[����5] VarChar,"
            //    + "[����5] VarChar,"
            //    + "[�п����ϵȼ�����5] VarChar,"
            //    + "[�п����ϵȼ�����5] VarChar,"
            //    + "[����ʽ5] VarChar,"
            //    + "[����ҽʦ5] VarChar,"

            //    + "[��������������6] VarChar,"
            //    + "[��������������6] VarChar,"
            //    + "[��������6] VarChar,"
            //    + "[��������������6] VarChar,"
            //    + "[����6] VarChar,"
            //    + "[����6] VarChar,"
            //    + "[����6] VarChar,"
            //    + "[�п����ϵȼ�����6] VarChar,"
            //    + "[�п����ϵȼ�����6] VarChar,"
            //    + "[����ʽ6] VarChar,"
            //    + "[����ҽʦ6] VarChar,"

            //    + "[��Ժ��ʽ] VarChar,"
            //    + "[ҽ��תԺ,����ҽ�ƻ�������] VarChar,"
            //    + "[ҽ��ת����/����,����ҽ�ƻ�������] VarChar,"
            //    + "[�Ƿ��г�Ժ31����סԺ�ƻ�] VarChar,"
            //    + "[Ŀ��] VarChar,"
            //    + "[��Ժǰ��] VarChar,"
            //    + "[��Ժǰʱ] VarChar,"
            //    + "[��Ժǰ��] VarChar,"
            //    + "[��Ժ����] VarChar,"
            //    + "[��Ժ��ʱ] VarChar,"
            //    + "[��Ժ���] VarChar,"


            //    + "[�ܷ���] VarChar,"
            //    + "[�Ը����] VarChar,"
            //    + "[һ��ҽ�Ʒ����] VarChar,"
            //    + "[��ҽ֤���η�] VarChar,"
            //    + "[��ҽ��֤���λ����] VarChar,"
            //    + "[һ�����Ʋ�����] VarChar,"
            //    + "[�����] VarChar,"
            //    + "[��ҽ��������] VarChar,"
            //    + "[������Ϸ�] VarChar,"
            //    + "[ʵ������Ϸ�] VarChar,"
            //    + "[Ӱ��ѧ��Ϸ�] VarChar,"
            //    + "[�ٴ������Ŀ��] VarChar,"
            //    + "[������������Ŀ��] VarChar,"
            //    + "[�ٴ��������Ʒ�] VarChar,"
            //    + "[�������Ʒ�] VarChar,"
            //    + "[�����] VarChar,"
            //    + "[������] VarChar,"
            //    + "[������] VarChar,"
            //    + "[��ҽ���] VarChar,"
            //    + "[��ҽ����] VarChar,"
            //    + "[��ҽ����] VarChar,"
            //    + "[��ҽ����] VarChar,"
            //    + "[�����ķ�] VarChar,"
            //    + "[��ҽ��������] VarChar,"
            //    + "[��ҽ�س�����] VarChar,"
            //    + "[��ҽ��������] VarChar,"
            //    + "[��ҽ����] VarChar,"
            //    + "[��ҩ�������ӹ�] VarChar,"
            //    + "[��֤ʩ��] VarChar,"
            //    + "[��ҩ��] VarChar,"
            //    + "[����ҩ�����] VarChar,"
            //    + "[�г�ҩ��] VarChar,"
            //    + "[ҽ�ƻ�����ҩ�Ƽ���] VarChar,"
            //    + "[�в�ҩ��] VarChar,"
            //    + "[Ѫ��] VarChar,"
            //    + "[�׵�������Ʒ��] VarChar,"
            //    + "[�򵰰�����Ʒ��] VarChar,"
            //    + "[��Ѫ��������Ʒ��] VarChar,"
            //    + "[ϸ����������Ʒ��] VarChar,"
            //    + "[�����һ����ҽ�ò��Ϸ�] VarChar,"
            //    + "[������һ����ҽ�ò��Ϸ�] VarChar,"
            //    + "[������һ����ҽ�ò��Ϸ�] VarChar,"
            //    + "[��������] VarChar)";
            //try
            //{

            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception exp)
            //{
            //    CommonFunction.WriteErrotLog(exp.ToString());
            //}
            #endregion

            OleDbConnection cn = new OleDbConnection(sConnectionString);
            OleDbCommand cmd = cn.CreateCommand();
            cn.Open();
            string strSQL = "INSERT INTO Sheet1 VALUES( {0} )";
            //�������
            foreach (DataRow _dr in dt.Rows)
            {
                string strvalues = "";
                foreach (DataColumn _dc in dt.Columns)
                {
                    strvalues += "'" + _dr[_dc] + "',";
                }
                strvalues = strvalues.Trim(',');
                cmd.CommandText = string.Format(strSQL, strvalues);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    CommonFunction.WriteError(exp.ToString() + strSQL);
                    continue;
                }
            }
            //�ر�����
            cn.Close();
            
        }

        #region ��DataTable����ΪExcel(OleDb ��ʽ������

        /// <summary>
        /// ��ȡExcel������
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection GetOleDbConnection(string p_strTarget)
        {
            string _strTarget = p_strTarget;
            OleDbConnection oleDbConn = new OleDbConnection();
            oleDbConn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + _strTarget + @";Extended ProPerties=""Excel 8.0;HDR=Yes;""";
            try
            {
                oleDbConn.Open();
            }
            catch (Exception)
            {
                oleDbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _strTarget + @";Extended ProPerties=""Excel 8.0;HDR=Yes;""";
                oleDbConn.Open();
            }
            return oleDbConn;
        }

        /// <summary>
        /// ��DataTable����ΪExcel(OleDb ��ʽ������
        /// </summary>
        /// <param name="dataTable">��</param>
        /// <param name="fileName">����Ĭ���ļ���</param>
        public static void InsertExcel(DataTable p_dtSource, string p_strTempletFullName)
        {
            string _strFileName = p_strTempletFullName.Substring(p_strTempletFullName.LastIndexOf('\\') + 1);
            string _strTarget = CommonFunction.GetConfig("ExcelOutPutDir") + "\\" + _strFileName;
            if (!File.Exists(_strTarget))
            {
                CommonFunction.CopyFile(p_strTempletFullName, _strTarget, false);
            }
            string strColumn = GetColumnStr(_strTarget);//���Ҫ������ֶ���
            DataTable dtColumn = ReadExcel(_strTarget);//���Ҫ������ֶ���
            int _icount = 0;
            foreach (DataRow dr in p_dtSource.Rows)
            {
                _icount++;
                string _strSQL = "insert into [sheet1$]( ";//sql�Ŀ�ͷ
                _strSQL += strColumn;
                _strSQL += ") values (";
                _strSQL += ReturnStrValues(dr, dtColumn);
                _strSQL += ")";
                //CommonFunction.WriteLog("SQL:" + _strSQL);
                if (1 == WriteExcel(_strTarget, _strSQL))
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
        }

        private static int WriteExcel(string _strTarget, string p_strSQL)
        {
            using (OleDbConnection oleDbConn = GetOleDbConnection(_strTarget))
            {
                OleDbCommand oleDbCmd = new OleDbCommand();
                string _strSQL = p_strSQL;
                try
                {
                    oleDbCmd.CommandType = CommandType.Text;
                    oleDbCmd.Connection = oleDbConn;
                    oleDbCmd.CommandText = _strSQL;
                    //CommonFunction.WriteLog("start insert...");
                    int _n = oleDbCmd.ExecuteNonQuery();
                    //CommonFunction.WriteLog("end insert..." + _n);
                    return _n;
                }
                catch (Exception ex)
                {
                    CommonFunction.WriteError(ex.ToString());
                }
            }
            return -1;
        }

        private static string ReturnStrValues(DataRow p_drSource, DataTable p_dtExcel)
        {
            DataTable dt = p_drSource.Table;
            string strSqlValues = "";
            foreach (DataColumn _dc in p_dtExcel.Columns)
            {
                if (dt.Columns.Contains(_dc.ColumnName))
                {
                    string _strTemp = p_drSource[_dc.ColumnName.ToUpper()].ToString();
                    if (_dc.DataType.Name == "String"|true) //ƴ���ַ�������
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
                    //else if (_dc.DataType.Name.Contains("Int") || _dc.DataType.Name.Contains("Decimal") || _dc.DataType.Name.Contains("Double"))//ƴ����������
                    //{
                    //    if (_strTemp != null && _strTemp != "")
                    //    {
                    //        strSqlValues += p_drSource[_dc].ToString() + ",";
                    //    }
                    //    else
                    //    {
                    //        strSqlValues += "null,";
                    //    }

                    //}
                    //else if (_dc.DataType.Name == "DateTime")//ƴ����������
                    //{
                    //    if (_strTemp != null && _strTemp != "")
                    //    {
                    //        strSqlValues += GetDate(p_drSource[_dc].ToString()) + ",";
                    //    }
                    //    else
                    //    {
                    //        strSqlValues += GetDate("1949-01-01") + ",";
                    //        //strSqlValues += "1949-01-01,";
                    //    }
                    //}
                    //else
                    //{
                    //    string _s = _dc.DataType.Name;
                    //    strSqlValues += "null,";
                    //}
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
        /// ��ȡExcle������ɵ�column�ַ���
        /// </summary>
        /// <param name="_strTarget"></param>
        /// <returns></returns>
        private static string GetColumnStr(string _strTarget)
        {
            DataTable _dtTemp = ReadExcel(_strTarget);
            try
            {
                string _strColumn = string.Empty;
                foreach (DataColumn var in _dtTemp.Columns)
                {
                    _strColumn += var.Caption + ",";
                }
                _strColumn = _strColumn.Trim(',');
                return _strColumn;
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError("GetColumnStr");
            }
            return "������ʧ��";
        }

        /// <summary>
        /// ��ȡExcle
        /// </summary>
        /// <param name="_strTarget"></param>
        /// <returns></returns>
        private static DataTable ReadExcel(string _strTarget)
        {
            CommonFunction.WriteLog("��ʼ��ȡExcel" + _strTarget);
            using (OleDbConnection oleDbConn = GetOleDbConnection(_strTarget))
            {
                string _strSQL = string.Empty;
                try
                {
                    _strSQL = "select * from  [sheet1$] where 1=0";
                    OleDbDataAdapter da = new OleDbDataAdapter(_strSQL, oleDbConn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Templet");
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    CommonFunction.WriteError(ex.ToString());
                }
            }
            return null;
        }

        private static string ReColumnStr(string _strTarget)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        /// <summary>
        /// ��DataTable����ΪExcel(OleDb ��ʽ������
        /// </summary>
        /// <param name="dataTable">��</param>
        /// <param name="fileName">����Ĭ���ļ���</param>
        //public static void InsertExcel(DataTable dataTable, string p_strTempletFullName)
        //{
        //    string _strFileName = p_strTempletFullName.Substring(strFileName.LastIndexOf('\\') + 1);
        //    string _strTarget = CommonFunction.GetConfig("ExcelOutPutDir") + "\\" + _strFileName;
        //    CommonFunction.CopyFile(p_strTempletFullName, _strTarget, false);
        //    OleDbConnection oleDbConn = new OleDbConnection();
        //    OleDbCommand oleDbCmd = new OleDbCommand();
        //    string sSql = "";
        //    try
        //    {
        //        oleDbConn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + p_strTempletFullName + @";Extended ProPerties=""Excel 8.0;HDR=Yes;""";
        //        oleDbConn.Open();
        //        oleDbCmd.CommandType = CommandType.Text;
        //        oleDbCmd.Connection = oleDbConn;
        //        sSql = "CREATE TABLE sheet1 (";
        //        for (int i = 0; i < dataTable.Columns.Count; i++)
        //        {
        //            // �ֶ����Ƴ��ֹؼ��ֻᵼ�´���
        //            if (i < dataTable.Columns.Count - 1)
        //                sSql += "[" + dataTable.Columns[i].Caption + "] TEXT(100) ,";
        //            else
        //                sSql += "[" + dataTable.Columns[i].Caption + "] TEXT(200) )";
        //        }
        //        oleDbCmd.CommandText = sSql;
        //        oleDbCmd.ExecuteNonQuery();
        //        for (int j = 0; j < dataTable.Rows.Count; j++)
        //        {
        //            sSql = "INSERT INTO sheet1 VALUES('";
        //            for (int i = 0; i < dataTable.Columns.Count; i++)
        //            {
        //                if (i < dataTable.Columns.Count - 1)
        //                    sSql += dataTable.Rows[j][i].ToString() + " ','";
        //                else
        //                    sSql += dataTable.Rows[j][i].ToString() + " ')";
        //            }
        //            oleDbCmd.CommandText = sSql;
        //            oleDbCmd.ExecuteNonQuery();
        //        }
        //        string mess = "���ݵ����ɹ���";
        //        ToolFunction.uctlMessageBox.frmDisappearShow(mess);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("����EXCELʧ��:" + ex.Message);
        //    }
        //    finally
        //    {
        //        oleDbCmd.Dispose();
        //        oleDbConn.Close();
        //        oleDbConn.Dispose();
        //    }
        //}
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
