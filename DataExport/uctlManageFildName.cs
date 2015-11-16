using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ToolFunction;
using DataExport;
using System.Threading;
using System.Data.OleDb;
namespace ConfirmFileName
{
    public partial class AddFildName : UserControl
    {
        CommonFunction t = new CommonFunction();
        private DataTable dttemp = null;
        private DataTable dttarget = null;
        private DataTable dttabledetail = null;
        private string connstr = "";
        private bool needfix = false;
        private bool isopenflag = false;
        private string excel = "Provider=Microsoft.Ace.OleDb.12.0;data source='{0}';Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
        private DataSet myDs = new DataSet();
        public AddFildName()
        {
            InitializeComponent();
            InitComBoxDataSource();
            InitData();
            isopenflag = true;
            cmb_ptname.Focus();
        }

        private void InitData()
        {
            if (cmb_ptname.SelectedValue == null || gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            string sql = string.Format("select * from PT_TABLES_DICT where pt_id = '{0}'", cmb_ptname.SelectedValue.ToString());
            dttabledetail = DALUse.Query(sql).Tables[0];
            gc_table_detail.DataSource = dttabledetail;
            string sqltrget = string.Format("select * from pt_target_field where table_name ='{0}' and pt_id = '{1}'", gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), cmb_ptname.SelectedValue.ToString());
            dttemp = DALUse.Query(sqltrget).Tables[0];
            gc_pt_target.DataSource = dttemp;
        }

        public void InitComBoxDataSource()
        {
            try
            {
                cmb_ptname.DataSource = DALUse.Query(PublicProperty.SqlGetPT).Tables[0];
                cmb_ptname.DisplayMember = "PT_NAME";
                cmb_ptname.ValueMember = "PT_ID";
                //string sql = "select * from pt_dict";
                //cmb_ptname.DataSource = DALUse.Query(sql).Tables[0];
                //cmb_ptname.DisplayMember = "PT_NAME";
                //cmb_ptname.ValueMember = "PT_ID";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btn_del_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0)
            {
                return;
            }

            string sql = string.Format("delete from PT_TARGET_FIELD where compare_id='{0}'", gridView2.GetDataRow(gridView2.FocusedRowHandle)["compare_id"].ToString());
            string sql1 = string.Format("delete from pt_comparison where pt_id = '{0}' and table_name = '{1}' and field = '{2}'", cmb_ptname.SelectedValue.ToString(), gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), gridView2.GetDataRow(gridView2.FocusedRowHandle)["FIELD"].ToString());
            if (DialogResult.Yes == MessageBox.Show(null, "确定删除所选项！", "删除", MessageBoxButtons.YesNo))
            {
                DALUse.ExecuteSql(sql);
                DALUse.ExecuteSql(sql1);
            }
            RefreshContent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (cmb_ptname.SelectedItem == null)
            {
                return;
            }
            uctlManageField.actionflag = "add";
            uctlManageField.pt_id = cmb_ptname.SelectedValue.ToString();
            uctlManageField.pt_name = cmb_ptname.Text.Trim();
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            uctlManageField.table_name = gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString();
            Form f = new Form();
            f.Size = new Size(531, 102);
            f.FormBorderStyle = FormBorderStyle.None;
            uctlManageField mf = new uctlManageField();
            CommonFunction.AddForm(f, mf);
            RefreshContent();

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if ((gridView1.FocusedRowHandle < 0) || gridView2.FocusedRowHandle < 0)
            {
                return;
            }
            uctlManageField.actionflag = "update";
            uctlManageField.pt_id = cmb_ptname.SelectedValue.ToString();
            uctlManageField.pt_name = cmb_ptname.Text.Trim();
            uctlManageField.table_name = gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString();
            uctlManageField.compare_id = gridView2.GetDataRow(gridView2.FocusedRowHandle)["COMPARE_ID"].ToString();
            Form f = new Form();
            f.Size = new Size(531, 102);
            f.FormBorderStyle = FormBorderStyle.None;
            uctlManageField mf = new uctlManageField();
            CommonFunction.AddForm(f, mf);

            RefreshContent();

        }



        /// <summary>
        /// 每当修改，删除，保存操作后进行刷新操作，跟新实时数据。
        /// </summary>
        public void RefreshContent()
        {
            string sql = string.Format("select * from pt_target_field where table_name ='{0}' and pt_id ='{1}'", gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), ((DataRowView)cmb_ptname.SelectedItem).Row["pt_id"].ToString());
            dttemp = DALUse.Query(sql).Tables[0];
            gc_pt_target.DataSource = dttemp;
        }


        private void gc_table_detail_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                GridHitInfo info = this.gridView1.CalcHitInfo(e.Location);
                if (info != null && info.InRow)
                {
                    //gridView1.get
                    object j = gridView1.GetRow(info.RowHandle);
                    if (j != null)
                    {
                        DataRowView view = (DataRowView)j;
                        DataRow dr = view.Row;
                        if (dr["CHK"].ToString() == "×")
                        {
                            //gridView1.getr
                            dr["CHK"] = "√";
                        }
                        else if (dr["CHK"].ToString() == "√")
                        {
                            dr["CHK"] = "√";
                        }
                    }
                    gridView1.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb_ptname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                gc_pt_target.DataSource = null;
                string sql = string.Format("select * from PT_TABLES_DICT where pt_id = '{0}'", ((DataRowView)cmb_ptname.SelectedItem).Row["pt_id"].ToString());
                dttabledetail = DALUse.Query(sql).Tables[0];
                gc_table_detail.DataSource = dttabledetail;
                if (dttabledetail.Rows.Count != 0)
                {
                    string sqltrget = string.Format("select * from pt_target_field where table_name ='{0}' and pt_id = '{1}'", gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), ((DataRowView)cmb_ptname.SelectedItem).Row["pt_id"].ToString());
                    dttemp = DALUse.Query(sqltrget).Tables[0];
                    gc_pt_target.DataSource = dttemp;
                }



            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取本地临时表的列
        /// </summary>
        /// <param name="dt">录入的临时表</param>
        /// <returns></returns>
        public DataTable GetTempColumns(DataTable dt)
        {
            DataTable dtmytable = new DataTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataColumn dc = null;
                    if (dtmytable.Columns.Contains(dr["field"].ToString()))
                    {
                        needfix = true;
                        CommonFunction.WriteErrorLog("目标表:" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString() + "重复录入名为：" + dr["field"].ToString() + "的字段！\n");
                    }
                    else
                    {
                        dc = new DataColumn(dr["field"].ToString());
                        dtmytable.Columns.Add(dc);

                    }
                }
            }
            return dtmytable;
        }
        /// <summary>
        ///  检查：
        /// 1.所录入的目标表在目标库是否存在，
        /// 2.录入的列是否存在
        /// 3.是否录入了不存在的字段
        /// </summary>
        /// <param name="dttemp">本地录入的临时表</param>
        /// <param name="dttarget">目标库的真是表</param>
        public void CheckTargetDict(DataTable dttemp, DataTable dttarget)
        {
            try
            {

                CommonFunction cf = new CommonFunction();
                cf.WaitingThreadStart();
                if (dttarget == null)
                {
                    needfix = true;
                    CommonFunction.WriteErrorLog("目标表:" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString() + "不存在！\n");
                }
                
                DataTable dttemp1 = GetTempColumns(dttemp);
                if (dttemp1.Columns.Count == 0)
                {
                    needfix = true;
                    CommonFunction.WriteErrorLog("目标表:" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString() + "未录入字段！\n");
                }
                else
                {
                    //判断多余字段
                    foreach (DataColumn dc in dttemp1.Columns)
                    {
                        if (!dttarget.Columns.Contains(dc.ColumnName))
                        {
                            needfix = true;
                            CommonFunction.WriteErrorLog("目标表:" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString() + "不存在名为：【" + dc.ColumnName + "】的字段！\n");
                        }
                    }
                    //判断缺少字段
                    foreach (DataColumn dc in dttarget.Columns)
                    {
                        if (!dttemp1.Columns.Contains(dc.ColumnName))
                        {
                            needfix = true;
                            CommonFunction.WriteErrorLog("字段：【" + dc.ColumnName + "】未录入目标表:" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString() + "\n");
                        }
                    }
                }
                cf.WaitingThreadStop();
                if (needfix)
                {
                    MessageBox.Show("字段录入有误请核对，详情参照错误日志ErrorLog.txt！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
               
                needfix = false;
                if (dttabledetail.Rows.Count != 0)
                {
                    DataSet dsconnstr = DALUse.Query(string.Format("select * from PT_Setting where pt_id ='{0}'", cmb_ptname.SelectedValue.ToString()));
                    if (dsconnstr.Tables.Count > 0 && dsconnstr.Tables[0].Rows.Count > 0)
                    {
                        connstr = dsconnstr.Tables[0].Rows[0]["connstr"].ToString();
                    }
                    string sqltemp = string.Format("select * from pt_target_field where table_name ='{0}' and pt_id = '{1}'", gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), ((DataRowView)cmb_ptname.SelectedItem).Row["pt_id"].ToString());
                    string sqltarget = string.Format("select * from {0} where 1=0 ", gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString());
                    DataSet dstarget = DALUseSpecial.Query(sqltarget, connstr);
                    if (dstarget.Tables.Count > 0)
                    {
                        dttarget = dstarget.Tables[0];
                    }
                    DataSet dstemp = DALUse.Query(sqltemp);
                    if (dstemp.Tables.Count > 0)
                    {
                        dttemp = dstemp.Tables[0];
                    }
                    CheckTargetDict(dttemp, dttarget);
                    gc_pt_target.DataSource = dttemp;
                }
               
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.ToString());
            }
        }

        private void AddFildName_Load(object sender, EventArgs e)
        {

        }

        private void excel_import_Click(object sender, EventArgs e)
        {

            //t.WaitingThreadStart();
            //string insertSql = "";
            //if (myDs != null && myDs.Tables[0].Rows.Count > 0)
            //{
            //    string insertColumnString = "pt_id,pt_name,table_name,compare_id,field_name,field,field_type";
            //    DataTable dt = myDs.Tables[0];
            //    try
            //    {
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            if (dr[0].ToString() == "")
            //            {
            //                continue;
            //            }
            //            string insertValueString = "";
            //            insertValueString = "'" + cmb_ptname.SelectedValue.ToString() + "','" + cmb_ptname.Text.ToString() + "','" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString() + "','" + Guid.NewGuid().ToString() +"',";
            //            for (int i = 0; i < dt.Columns.Count; i++)
            //            {
            //                insertValueString += string.Format("'{0}',", dr[i].ToString().Replace("'", "''"));
            //            }
            //            insertValueString = insertValueString.Trim(',');
            //            insertSql = string.Format(@"INSERT INTO {0} ({1}) VALUES({2})", "pt_target_field", insertColumnString, insertValueString);
            //            DALUse.ExecuteSql(insertSql);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //t.WaitingThreadStop();
        }

        private void btn_sel_file_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string connectString = string.Format(excel, openFileDialog1.FileName);
            try
            {
                //t.WaitingThreadStart();
                myDs.Tables.Clear();
                myDs.Clear();
                OleDbConnection cnnxls = new OleDbConnection(connectString);
                OleDbDataAdapter myDa = new OleDbDataAdapter("select * from [Sheet1$]", cnnxls);
                myDa.Fill(myDs, "c");
                //t.WaitingThreadStop();
                Form f = new Form();
                f.Size = new Size(549, 446);
                f.FormBorderStyle = FormBorderStyle.None;
                uctlShowImportField sif = new uctlShowImportField();
                sif.myDs = myDs;
                sif.table_name = gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString();
                sif.pt_name = cmb_ptname.Text.ToString();
                sif.pt_id = cmb_ptname.SelectedValue.ToString();
                CommonFunction.AddForm(f, sif);
                //MessageBox.Show("数据载入完成！");
            }
            catch (Exception ex)
            {
                //t.WaitingThreadStop();
                MessageBox.Show(ex.Message);
            }

        }

        //将目标表的字段同步到本地
        public void SynTargetField()
        {
            try
            {
                string sqltarget = string.Format("select * from {0} where 1=0 ", gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString());
                DataSet dstarget = DALUseSpecial.Query(sqltarget, connstr);
                if (dstarget.Tables.Count > 0)
                {
                    foreach (DataColumn dc in dstarget.Tables[0].Columns)
                    {
                        string columntype = "";
                        if (dc.DataType.Name.ToString().Contains("String"))
                        {
                            columntype = "STRING";
                        }
                        else if (dc.DataType.Name.ToString().Contains("Decimal") || dc.DataType.Name.ToString().Contains("Int"))
                        {
                            columntype = "NUMBER";
                        }
                        else if (dc.DataType.Name.ToString().Contains("DateTime"))
                        {
                            columntype = "DATE";
                        }
                        else
                        {
                            MessageBox.Show(dc.DataType.Name.ToString());
                        }
                        
                        string sql = string.Format("insert into PT_TARGET_FIELD(compare_id,pt_name,table_name,field_name,field,field_type,compare_name,pt_id) values('{7}','{0}','{1}','{2}','{3}','{4}','{5}','{6}')", cmb_ptname.Text, gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), dc.ColumnName, dc.ColumnName, columntype, "", cmb_ptname.SelectedValue.ToString(), Guid.NewGuid());
                        string sql1 = string.Format("select * from PT_TARGET_FIELD where pt_id = '{0}' and table_name = '{1}' and field = '{2}'", cmb_ptname.SelectedValue.ToString(), gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString(), dc.ColumnName);
                        DataSet ds = DALUse.Query(sql1);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                        {
                            DALUse.ExecuteSql(sql);
                        }
                       
                    }
                }
                else
                {
                    MessageBox.Show("目标库不存在表：" + gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btn_sny_Click(object sender, EventArgs e)
        {
            SynTargetField();
        }
    }
}