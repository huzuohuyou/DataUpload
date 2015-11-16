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
        #region IExport ��Ա

        public void Export()
        {
            //AddExcel(PublicProperty.ExcelPath, PublicProperty.ExcelSource);
            DataSetToExcel(PublicProperty.ExcelSource,PublicProperty.ExcelPath);
            //InsertIntoExcel(PublicProperty.ExcelPath, PublicProperty.ExcelSource);
           
        }

        #endregion

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
        /// ��DataTable����ΪExcel(OleDb ��ʽ������
        /// </summary>
        /// <param name="dataTable">��</param>
        /// <param name="fileName">����Ĭ���ļ���</param>
        public static void DataSetToExcel(DataTable dataTable, string fileName)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "xls files (*.xls)|*.xls";
            //saveFileDialog.FileName = fileName;
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //fileName = saveFileDialog.FileName;
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                    MessageBox.Show("���ļ�����ʹ����,�ر��ļ����������������ļ�����!");
                    return;
                }
            }
            OleDbConnection oleDbConn = new OleDbConnection();
            OleDbCommand oleDbCmd = new OleDbCommand();
            string sSql = "";
            try
            {
                oleDbConn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + fileName + @";Extended ProPerties=""Excel 8.0;HDR=Yes;""";
                oleDbConn.Open();
                oleDbCmd.CommandType = CommandType.Text;
                oleDbCmd.Connection = oleDbConn;
                sSql = "CREATE TABLE sheet1 (";
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    // �ֶ����Ƴ��ֹؼ��ֻᵼ�´���
                    if (i < dataTable.Columns.Count - 1)
                        sSql += "[" + dataTable.Columns[i].Caption + "] TEXT(100) ,";
                    else
                        sSql += "[" + dataTable.Columns[i].Caption + "] TEXT(200) )";
                }
                oleDbCmd.CommandText = sSql;
                oleDbCmd.ExecuteNonQuery();
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    sSql = "INSERT INTO sheet1 VALUES('";
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i < dataTable.Columns.Count - 1)
                            sSql += dataTable.Rows[j][i].ToString() + " ','";
                        else
                            sSql += dataTable.Rows[j][i].ToString() + " ')";
                    }
                    oleDbCmd.CommandText = sSql;
                    oleDbCmd.ExecuteNonQuery();
                }
                string mess = "���ݵ����ɹ���";
                ToolFunction.uctlMessageBox.frmDisappearShow(mess);
            }
            catch (Exception ex)
            {
                MessageBox.Show("����EXCELʧ��:" + ex.Message);
            }
            finally
            {
                oleDbCmd.Dispose();
                oleDbConn.Close();
                oleDbConn.Dispose();
            }
            //}
        }
        #endregion

    }
}
