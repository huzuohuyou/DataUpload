using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using System.Diagnostics;
using System.Data.OleDb;
using JHEMR.EmrSysDAL;
using System.Xml;
using System.IO;

namespace DataExport
{
    public partial class uctlDataImport : UserControl
    {
        CommonFunction t = new CommonFunction();
        private DataSet myDs = new DataSet();
        private string tablename = "";
        private string excel = "Provider=Microsoft.Ace.OleDb.12.0;data source='{0}';Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
        //private string connectionStringFormat = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '{0}';Extended Properties=Excel 8.0";
        public uctlDataImport()
        {
            InitializeComponent();
            InitComboxDatasource();
        }
        public void InitComboxDatasource()
        {
            cmb_pt.DataSource = DALUse.Query(PublicProperty.SqlGetPT).Tables[0];
            cmb_pt.ValueMember = "pt_id";
            cmb_pt.DisplayMember = "pt_name";
        }
        private void lnkExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                const string helpfile = "资料导入模板.xls";
                Process.Start(helpfile);
            }
            catch (Exception)
            {
                MessageBox.Show("打开文件失败！");
            }
        }



        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "F:\\工作文档";
            openFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = openFileDialog1.FileName;
                this.txt_dict_name.Text = openFileDialog1.SafeFileName.Split('.')[0];
            }
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {

            if (this.txtFilePath.Text == "")
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            if (txtFilePath.Text.ToString().Contains("xml"))
            {
                gridView1.Columns.Clear();

                gc_excel.DataSource = ConvertToDateSetByXmlString().Tables[0];
            }
            else
            {
                string connectString = string.Format(excel, this.txtFilePath.Text);
                try
                {
                    myDs.Tables.Clear();
                    //MessageBox.Show("载入字典");
                    DataTable dt = CommonFunction.ExcelToDataSet(connectString).Tables[0];
                    gc_excel.DataSource = dt;
                    //MessageBox.Show("表列数：" + dt.Rows.Count.ToString());
                    myDs.Tables.Add(dt.Copy());
                }
                catch (Exception exp)
                {
                    CommonFunction.WriteErrorLog(exp.ToString());
                }
            }

        }

        public void InisertIntoDict()
        {


        }

        /// <summary>
        /// 检查Excel的列是否符合格式规范 type_name ,field_code , field;
        /// </summary>
        /// <param name="dt">获取的ExcleDataTable</param>
        public bool CheckDictColumns(DataTable dt)
        {
            bool result = true;
            if (dt.Columns.Count != 2 || (dt.Columns[0].ColumnName != "FIELD_CODE" || dt.Columns[1].ColumnName != "FIELD"))
            {
                MessageBox.Show("列设置错误！依次应为[field_code][field]");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 检查数据字典插入是否选择完成
        /// </summary>
        /// <returns>满足要求返回true</returns>
        private bool CheckSelected()
        {
            bool result = true;
            if (this.cmb_pt.Text == "")
            {
                MessageBox.Show("请选择平台");
                result = false;
            }
            if (this.txtFilePath.Text == "")
            {
                MessageBox.Show("请选择指定文件");
                result = false;
            }
            if (gc_excel.DataSource == null)
            {
                MessageBox.Show("请先点击【查看数据】检验数据正确性。");
                result = false;
            }
           
            if (cmb_table_name.Text == "本地字典表")
            {
                tablename = "PT_COMPARISON_DETAIL_DICT";
            }
            else if (cmb_table_name.Text == "对照字典表")
            {
                tablename = "pt_local_dict";
            }
            else
            {
                MessageBox.Show("请选择插入的表！");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 插入字典信息
        /// 1.选择平台
        /// 2.选择所维护的Excel字典文件
        /// 3.选择所插入的字典表
        /// 4.填写字典名（默认为文件名）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            string insertSql = "";
            string insertColumnString = "pt_id,id,type_name,field_code,field";
            if (CheckSelected() && MessageBox.Show(null, "该操作将把数据导入到系统的用户数据库中，您确定是否继续？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            //t.WaitingThreadStart();
            //MessageBox.Show(myDs.Tables.Count.ToString() + "--" + myDs.Tables[0].Rows.Count.ToString());
            if (myDs != null && myDs.Tables.Count > 0 && myDs.Tables[0].Rows.Count > 0 && CheckDictColumns(myDs.Tables[0]))
            {
                try
                {
                    foreach (DataRow dr in myDs.Tables[0].Rows)
                    {
                        if (dr[0].ToString() == "")
                        {
                            continue;
                        }
                        string insertValueString = "";
                        insertValueString = "'" + cmb_pt.SelectedValue.ToString() + "','" + Guid.NewGuid().ToString() + "','" + txt_dict_name.Text + "',";
                        for (int i = 0; i < myDs.Tables[0].Columns.Count; i++)
                        {
                            insertValueString += string.Format("'{0}',", dr[i].ToString().Replace("'", "''"));
                        }
                        insertValueString = insertValueString.Trim(',');
                        insertSql = string.Format(@"INSERT INTO {0} ({1}) VALUES({2})", tablename, insertColumnString, insertValueString);
                        DALUse.ExecuteSql(insertSql);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    CommonFunction.WriteErrorLog(ex.ToString() + "\n" + insertSql);
                }
            }
            //t.WaitingThreadStop();
            //}
            GetDictDetail();
            //string tips = string.Format("共导入{0}条数据", ((DataTable)gc_excel.DataSource).Rows.Count);
            //MessageBox.Show(tips);
        }


        /// <summary>
        /// 获取插入字典的内容
        /// </summary>
        public void GetDictDetail()
        {
            DataTable dt = null;
            string sqlgetinsertdata = (string.Format("select TYPE_NAME,FIELD_CODE,FIELD from {0} where pt_id = '{1}' and type_name = '{2}'", tablename, cmb_pt.SelectedValue.ToString(), txt_dict_name.Text.ToString()));
            try
            {
                dt = DALUse.Query(sqlgetinsertdata).Tables[0];
            }
            catch (Exception ex)
            {
                CommonFunction.WriteErrorLog(ex.ToString());
            }
            gc_database.DataSource = dt;
        }

        private bool CheckIsDate(string columnName)
        {
            string str = ",PREPARE_DATE,COPY_DATE,COPY_VALIDITY,BUSINESS_VALIDITY,OPENING_APPROVAL_DATE,OPENING_DATE,EDITTIME,LICENSE_DATE,LICENSE_VALIDITY,TEMP_OPENING_DATE,LICENSE_START_DATE,ADDTIME,EDITTIME,";
            return str.Contains("," + columnName.ToUpper() + ",");
        }

        private bool CheckIsNumeric(string columnName)
        {
            string str = ",FIXED_CAPITAL,REG_CAPITAL,MARGIN,PARK_AREA,PARK_SPACE_NUMBER,";
            return str.Contains("," + columnName.ToUpper() + ",");
        }

        /// <summary>
        /// 将xml数据导入到数据库
        /// </summary>
        private void ImportDataToDataBase(string tablename)
        {


        }

        public void InsertIntoTable(DataSet ds)
        {
            #region 将生成的dataset存入数据库
            List<string> sqllist = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string sql = "";
                string insertValueString = "";
                string insertColumnString = "";
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    ///读取信息 然后导入到数据库 
                    insertValueString += string.Format("'{0}',", dr[dc].ToString());
                    insertColumnString += string.Format("{0},", dc.ToString());
                }
                insertColumnString = insertColumnString.Trim(',');
                insertValueString = insertValueString.Trim(',');
                sql = string.Format(@"insert into {0}({1}) values({2})", txt_dict_name.Text, insertColumnString, insertValueString);
                sqllist.Add(sql);
            }

            try
            {
                DALUse.ExecuteSqlTran(sqllist.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion
        }

        /// <summary>
        /// 将XML字符串转换成DATASET
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public DataSet ConvertToDateSetByXmlString()
        {
            XmlDocument x = new XmlDocument();
            x.Load(txtFilePath.Text);///xml路径  
            string xmlStr = x.InnerXml;
            if (xmlStr.Length > 0)
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据 
                    //ds.ReadXmlSchema(Xmlrdr);
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public void EasyReadXML()
        {

        }

        public bool XMLDataImport()
        {

            List<string> sqllist = new List<string>();
            try
            {
                XmlDocument x = new XmlDocument();
                x.Load(txtFilePath.Text);///xml路径  

                foreach (XmlNode xn in x.ChildNodes)
                {

                    foreach (XmlNode xn1 in xn.ChildNodes)
                    {
                        string sql = "";
                        string insertValueString = "";
                        string insertColumnString = "";
                        foreach (XmlNode xn2 in xn1.ChildNodes)
                        {
                            ///读取信息 然后导入到数据库 
                            insertValueString += string.Format("'{0}',", xn2.InnerText.ToString());
                            insertColumnString += string.Format("{0},", xn2.Name.ToString());
                        }
                        insertColumnString = insertColumnString.Trim(',');
                        insertValueString = insertValueString.Trim(',');
                        sql = string.Format(@"insert into {0}({1}) values({2})", txt_dict_name.Text, insertColumnString, insertValueString);
                        sqllist.Add(sql);
                    }

                }
                DALUse.ExecuteSqlTran(sqllist.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return true;
        }
    }
}
