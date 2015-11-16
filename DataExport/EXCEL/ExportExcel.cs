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
        #region IExport 成员

        public void Export()
        {
            //AddExcel(PublicProperty.ExcelPath, PublicProperty.ExcelSource);
            DataSetToExcel(PublicProperty.ExcelSource,PublicProperty.ExcelPath);
            //InsertIntoExcel(PublicProperty.ExcelPath, PublicProperty.ExcelSource);
           
        }

        #endregion

        /// <summary>
        /// 将数据集导出成为Excel，不需求要Excel模板
        /// </summary>
        /// <param name="name">导出excel的名称</param>
        /// <param name="ds">所导出的数据集</param>
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
            GC.Collect();//垃圾回收 
        }

        /// <summary>
        /// 将数据集插入，需要Excel模板
        /// </summary>
        /// <param name="name">导出excel的名称</param>
        /// <param name="ds">所导出的数据集</param>
        public static void InsertIntoExcel(string filename, DataTable dt)
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
         "Data Source=" + filename + ";" +
         "Extended Properties=Excel 8.0;";

            #region 创建TestSheet工作表
            //string sqlCreate = "CREATE TABLE TestSheet ("
            //    + "[机构名称] VarChar,"
            //    + "[医疗付费方式] VarChar,"
            //    + "[健康卡号] VarChar,"
            //    + "[住院次数] VarChar,"
            //    + "[病案号] VarChar,"
            //    + "[姓名] VarChar,"
            //    + "[性别] VarChar,"
            //    + "[出生日期] VarChar,"
            //    + "[年龄] VarChar,"
            //    + "[国籍] VarChar,"
            //    + "[年龄(月)] VarChar,"
            //    + "[新生儿出生体重(g)] VarChar,"
            //    + "[新生儿入院体重(g)] VarChar,"
            //    + "[出生地] VarChar,"
            //    + "[籍贯] VarChar,"
            //    + "[名族] VarChar,"
            //    + "[身份证号] VarChar,"
            //    + "[职业] VarChar,"
            //    + "[婚姻] VarChar,"
            //    + "[现住址] VarChar,"
            //    + "[现住址电话] VarChar,"
            //    + "[现住址邮编] VarChar,"
            //    + "[户口地址] VarChar,"
            //    + "[户口地址邮编] VarChar,"
            //    + "[工作单位及地址] VarChar,"
            //    + "[单位电话] VarChar,"
            //    + "[联系人姓名] VarChar,"
            //    + "[关系] VarChar,"
            //    + "[地址] VarChar,"
            //    + "[电话] VarChar,"
            //    + "[入院途径] VarChar,"
            //    + "[治疗类别] VarChar,"
            //    + "[入院日期] VarChar,"
            //    + "[入院时间(时)] VarChar,"
            //    + "[入院科别] VarChar,"
            //    + "[入院病房] VarChar,"
            //    + "[转科科别] VarChar,"
            //    + "[出院日期] VarChar,"
            //    + "[出院时间(时)] VarChar,"
            //    + "[出院科别] VarChar,"
            //    + "[出院病房] VarChar,"
            //    + "[实际住院(天)] VarChar,"
            //    + "[门(急)诊诊断(中医)] VarChar,"
            //    + "[中医诊断疾病编码] VarChar,"
            //    + "[门(急)诊诊断(西医)] VarChar,"
            //    + "[西医诊断疾病编码] VarChar,"
            //    + "[实施临床路径] VarChar,"
            //    + "[使用医疗机构中药制剂] VarChar,"
            //    + "[使用中医诊疗设备] VarChar,"
            //    + "[使用中医诊疗技术] VarChar,"
            //    + "[辩证施护] VarChar,"
            //    + "[中医出院诊断] VarChar,"
            //    + "[中医疾病编码] VarChar,"
            //    + "[中医入院病情] VarChar,"
            //    + "[西医出院诊断] VarChar,"
            //    + "[西医疾病编码] VarChar,"
            //    + "[西医入院病情] VarChar,"

            //    + "[中医出院诊断1] VarChar,"
            //    + "[中医疾病编码1] VarChar,"
            //    + "[中医入院病情1] VarChar,"
            //    + "[西医出院诊断1] VarChar,"
            //    + "[西医疾病编码1] VarChar,"
            //    + "[西医入院病情1] VarChar,"

            //    + "[中医出院诊断2] VarChar,"
            //    + "[中医疾病编码2] VarChar,"
            //    + "[中医入院病情2] VarChar,"
            //    + "[西医出院诊断2] VarChar,"
            //    + "[西医疾病编码2] VarChar,"
            //    + "[西医入院病情2] VarChar,"

            //    + "[中医出院诊断3] VarChar,"
            //    + "[中医疾病编码3] VarChar,"
            //    + "[中医入院病情3] VarChar,"
            //    + "[西医出院诊断3] VarChar,"
            //    + "[西医疾病编码3] VarChar,"
            //    + "[西医入院病情3] VarChar,"

            //    + "[中医出院诊断4] VarChar,"
            //    + "[中医疾病编码4] VarChar,"
            //    + "[中医入院病情4] VarChar,"
            //    + "[西医出院诊断4] VarChar,"
            //    + "[西医疾病编码4] VarChar,"
            //    + "[西医入院病情4] VarChar,"

            //    + "[中医出院诊断5] VarChar,"
            //    + "[中医疾病编码5] VarChar,"
            //    + "[中医入院病情5] VarChar,"
            //    + "[西医出院诊断5] VarChar,"
            //    + "[西医疾病编码5] VarChar,"
            //    + "[西医入院病情5] VarChar,"

            //    + "[中医出院诊断6] VarChar,"
            //    + "[中医疾病编码6] VarChar,"
            //    + "[中医入院病情6] VarChar,"
            //    + "[西医出院诊断6] VarChar,"
            //    + "[西医疾病编码6] VarChar,"
            //    + "[西医入院病情6] VarChar,"

            //    + "[中医出院诊断7] VarChar,"
            //    + "[中医疾病编码7] VarChar,"
            //    + "[中医入院病情7] VarChar,"
            //    + "[西医出院诊断7] VarChar,"
            //    + "[西医疾病编码7] VarChar,"
            //    + "[西医入院病情7] VarChar,"

            //    + "[损伤、中毒的外部原因] VarChar,"
            //    + "[损伤、中毒疾病编码] VarChar,"
            //    + "[病理诊断] VarChar,"
            //    + "[病理诊断疾病编码] VarChar,"
            //    + "[病理号] VarChar,"
            //    + "[药物过敏] VarChar,"
            //    + "[过敏药物] VarChar,"
            //    + "[死亡患者尸检] VarChar,"
            //    + "[血型] VarChar,"
            //    + "[科主任] VarChar,"
            //    + "[主任(副主任)医师] VarChar,"
            //    + "[主治医师] VarChar,"
            //    + "[住院医师] VarChar,"
            //    + "[责任护士] VarChar,"
            //    + "[进修医师] VarChar,"
            //    + "[实习医师] VarChar,"
            //    + "[编码员] VarChar,"
            //    + "[病案质量] VarChar,"
            //    + "[质控医师] VarChar,"
            //    + "[质控护士] VarChar,"
            //    + "[质控日期] VarChar,"

            //    + "[手术及操作编码1] VarChar,"
            //    + "[手术及操作日期1] VarChar,"
            //    + "[手术级别1] VarChar,"
            //    + "[手术及操作名称1] VarChar,"
            //    + "[术者1] VarChar,"
            //    + "[Ⅰ助1] VarChar,"
            //    + "[Ⅱ助1] VarChar,"
            //    + "[切口愈合等级级别1] VarChar,"
            //    + "[切口愈合等级类型1] VarChar,"
            //    + "[麻醉方式1] VarChar,"
            //    + "[麻醉医师1] VarChar,"

            //    + "[手术及操作编码2] VarChar,"
            //    + "[手术及操作日期2] VarChar,"
            //    + "[手术级别2] VarChar,"
            //    + "[手术及操作名称2] VarChar,"
            //    + "[术者2] VarChar,"
            //    + "[Ⅰ助2] VarChar,"
            //    + "[Ⅱ助2] VarChar,"
            //    + "[切口愈合等级级别2] VarChar,"
            //    + "[切口愈合等级类型2] VarChar,"
            //    + "[麻醉方式2] VarChar,"
            //    + "[麻醉医师2] VarChar,"

            //    + "[手术及操作编码3] VarChar,"
            //    + "[手术及操作日期3] VarChar,"
            //    + "[手术级别3] VarChar,"
            //    + "[手术及操作名称3] VarChar,"
            //    + "[术者3] VarChar,"
            //    + "[Ⅰ助3] VarChar,"
            //    + "[Ⅱ助3] VarChar,"
            //    + "[切口愈合等级级别3] VarChar,"
            //    + "[切口愈合等级类型3] VarChar,"
            //    + "[麻醉方式3] VarChar,"
            //    + "[麻醉医师3] VarChar,"

            //    + "[手术及操作编码4] VarChar,"
            //    + "[手术及操作日期4] VarChar,"
            //    + "[手术级别4] VarChar,"
            //    + "[手术及操作名称4] VarChar,"
            //    + "[术者4] VarChar,"
            //    + "[Ⅰ助4] VarChar,"
            //    + "[Ⅱ助4] VarChar,"
            //    + "[切口愈合等级级别4] VarChar,"
            //    + "[切口愈合等级类型4] VarChar,"
            //    + "[麻醉方式4] VarChar,"
            //    + "[麻醉医师4] VarChar,"

            //    + "[手术及操作编码5] VarChar,"
            //    + "[手术及操作日期5] VarChar,"
            //    + "[手术级别5] VarChar,"
            //    + "[手术及操作名称5] VarChar,"
            //    + "[术者5] VarChar,"
            //    + "[Ⅰ助5] VarChar,"
            //    + "[Ⅱ助5] VarChar,"
            //    + "[切口愈合等级级别5] VarChar,"
            //    + "[切口愈合等级类型5] VarChar,"
            //    + "[麻醉方式5] VarChar,"
            //    + "[麻醉医师5] VarChar,"

            //    + "[手术及操作编码6] VarChar,"
            //    + "[手术及操作日期6] VarChar,"
            //    + "[手术级别6] VarChar,"
            //    + "[手术及操作名称6] VarChar,"
            //    + "[术者6] VarChar,"
            //    + "[Ⅰ助6] VarChar,"
            //    + "[Ⅱ助6] VarChar,"
            //    + "[切口愈合等级级别6] VarChar,"
            //    + "[切口愈合等级类型6] VarChar,"
            //    + "[麻醉方式6] VarChar,"
            //    + "[麻醉医师6] VarChar,"

            //    + "[离院方式] VarChar,"
            //    + "[医嘱转院,接收医疗机构名称] VarChar,"
            //    + "[医嘱转社区/乡镇,接收医疗机构名称] VarChar,"
            //    + "[是否有出院31天再住院计划] VarChar,"
            //    + "[目的] VarChar,"
            //    + "[入院前天] VarChar,"
            //    + "[入院前时] VarChar,"
            //    + "[入院前分] VarChar,"
            //    + "[入院后天] VarChar,"
            //    + "[入院后时] VarChar,"
            //    + "[入院后分] VarChar,"


            //    + "[总费用] VarChar,"
            //    + "[自付金额] VarChar,"
            //    + "[一般医疗服务费] VarChar,"
            //    + "[中医证论治费] VarChar,"
            //    + "[中医辨证论治会诊费] VarChar,"
            //    + "[一般治疗操作费] VarChar,"
            //    + "[护理费] VarChar,"
            //    + "[中医其他费用] VarChar,"
            //    + "[病理诊断费] VarChar,"
            //    + "[实验室诊断费] VarChar,"
            //    + "[影像学诊断费] VarChar,"
            //    + "[临床诊断项目费] VarChar,"
            //    + "[非手术治疗项目费] VarChar,"
            //    + "[临床物理治疗费] VarChar,"
            //    + "[手术治疗费] VarChar,"
            //    + "[麻醉费] VarChar,"
            //    + "[手术费] VarChar,"
            //    + "[康复费] VarChar,"
            //    + "[中医诊断] VarChar,"
            //    + "[中医治疗] VarChar,"
            //    + "[中医外治] VarChar,"
            //    + "[中医骨伤] VarChar,"
            //    + "[针刺与灸法] VarChar,"
            //    + "[中医推拿治疗] VarChar,"
            //    + "[中医肛肠治疗] VarChar,"
            //    + "[中医特殊治疗] VarChar,"
            //    + "[中医其他] VarChar,"
            //    + "[中药特殊调配加工] VarChar,"
            //    + "[辨证施膳] VarChar,"
            //    + "[西药费] VarChar,"
            //    + "[抗菌药物费用] VarChar,"
            //    + "[中成药费] VarChar,"
            //    + "[医疗机构中药制剂费] VarChar,"
            //    + "[中草药费] VarChar,"
            //    + "[血费] VarChar,"
            //    + "[白蛋白类制品费] VarChar,"
            //    + "[球蛋白类制品费] VarChar,"
            //    + "[凝血因子类制品费] VarChar,"
            //    + "[细胞因子类制品费] VarChar,"
            //    + "[检查用一次性医用材料费] VarChar,"
            //    + "[治疗用一次性医用材料费] VarChar,"
            //    + "[手术用一次性医用材料费] VarChar,"
            //    + "[其他费用] VarChar)";
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
            //添加数据
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
            //关闭连接
            cn.Close();
            
        }

        #region 将DataTable导出为Excel(OleDb 方式操作）


        /// <summary>
        /// 将DataTable导出为Excel(OleDb 方式操作）
        /// </summary>
        /// <param name="dataTable">表</param>
        /// <param name="fileName">导出默认文件名</param>
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
                    MessageBox.Show("该文件正在使用中,关闭文件或重新命名导出文件再试!");
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
                    // 字段名称出现关键字会导致错误。
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
                string mess = "数据导出成功！";
                ToolFunction.uctlMessageBox.frmDisappearShow(mess);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出EXCEL失败:" + ex.Message);
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
