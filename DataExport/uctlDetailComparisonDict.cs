using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using ToolFunction;

namespace DataExport
{
    /// <summary>
    /// NEWROWFLAG 为1表示未插入行，为0表示数据库中数据。
    /// </summary>
    public partial class uctlDetailComparisonDict : UserControl
    {
        CommonFunction cf = new CommonFunction();
        List<string> sqllist = new List<string>();
        StringBuilder sb = new StringBuilder();
        private DataTable dtcompare = new DataTable();
        //private DataTable dttarget = null;
        private DataTable dtlocal = null;
        public uctlDetailComparisonDict()
        {
            InitializeComponent();
            InitComboxDataSource();
            InitDataTableColumns();
            InitData();
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitComboxDataSource()
        {
            cmb_pt_name.DataSource = DALUse.Query(string.Format(PublicProperty.SqlGetPT)).Tables[0];
            cmb_pt_name.DisplayMember = "pt_name";
            cmb_pt_name.ValueMember = "pt_id";


        }

        public void InitDataTableColumns()
        {
            try
            {
                if (cmb_pt_name.SelectedItem == null | cmb_target.SelectedItem == null)
                {
                    return;
                }
                string getstyle = string.Format("select distinct 0 NEWROWFLAG, T.* from PT_COMPARISON_DETAIL_DICT T WHERE  1=0", ((DataRowView)cmb_pt_name.SelectedItem).Row["pt_id"].ToString(), ((DataRowView)cmb_target.SelectedItem).Row["type_name"].ToString());
                dtcompare = DALUse.Query(getstyle).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// 初始化gc_target and gc_local两表数据
        /// </summary>
        public void InitData()
        {
            try
            {
                //string sqltarget = string.Format("select * from PT_TARGET_DICT WHERE COMPARE_FLAG ='0'and pt_id={0}", cmb_target.SelectedValue.ToString());
                //string sqllocal = string.Format("select * from PT_LOCAL_DICT WHERE pt_id={0}", cmb_local.SelectedValue.ToString());
                if (cmb_pt_name.SelectedValue==null||cmb_target.SelectedValue==null)
                {
                    return;
                }
                string sqlcompare = string.Format("select distinct 0 NEWROWFLAG, t.* from PT_COMPARISON_DETAIL_DICT  t where pt_id ='{0}' and type_name = '{1}'", cmb_pt_name.SelectedValue.ToString(), cmb_target.SelectedValue.ToString());
                //dttarget = DALUse.Query(sqltarget).Tables[0];
                //gc_target.DataSource = dttarget;
                //dtlocal = DALUse.Query(sqllocal).Tables[0];
                //gc_local.DataSource = dtlocal;
                dtcompare = DALUse.Query(sqlcompare).Tables[0];
                gc_comparison.DataSource = dtcompare.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                cf.WaitingThreadStart();
                foreach (DataRow dr in dtcompare.Rows)
                {
                    if (dr["NEWROWFLAG"].ToString() == "1")//当时才插入为新插入行
                    {
                        string sqlupdate = string.Format("update PT_COMPARISON_DETAIL_DICT set compare_name='{0}',compare_code='{1}'where id = '{2}'",
                          dr["COMPARE_NAME"].ToString().Replace("'", "''"),
                          dr["COMPARE_CODE"].ToString().Replace("'", "''"),
                          dr["ID"].ToString());
                        sqllist.Add(sqlupdate);

                    }
                }
                bool result = DALUse.ExecuteSqlTran(sqllist.ToArray());
                cf.WaitingThreadStop();
                InitData();
                if (result)
                {
                    MessageBox.Show("数据保存成功！");
                }
            }
            catch (Exception ex)
            {
                string serror = "";
                foreach (string s in sqllist)
                {
                    serror += s + "\n";
                }
                MessageBox.Show(serror + ex.ToString());
            }
        }

        public void SaveAndUpdate()
        {
            try
            {
                DALUse.ExecuteSqlTran(sqllist.ToArray());
            }
            catch (Exception)
            {

                throw;
            }
            MessageBox.Show("保存操作完成！");
            InitData();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView3.FocusedRowHandle < 0)
                {
                    return;
                }
                DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
                DialogResult dresult = MessageBox.Show(null, "确定取消绑定所选项", "确认", MessageBoxButtons.YesNo);
                if (dr["NEWROWFLAG"].ToString() != "1" && dresult == DialogResult.Yes)
                {
                    //string sqldel = string.Format("delete from PT_COMPARISON_DETAIL_DICT where target_id={0} and local_id = {1}", dr["target_id"].ToString(), dr["local_id"].ToString());
                    //string sqlupdatetarget = string.Format("update PT_TARGET_DICT set compare_flag ='{0}' where id={1}", "0", dr["target_id"].ToString());
                    //string sqlupdatelocal = string.Format("update PT_LOCAL_DICT set compare_flag = '{0}' where ID = {1}", "0", dr["local_id"].ToString());
                    //string[] sqls = { sqldel, sqlupdatetarget, sqlupdatelocal };
                    //DALUse.ExecuteSqlTran(sqls);
                    string sqlcancel = string.Format("update PT_COMPARISON_DETAIL_DICT set compare_name='{0}',compare_code='{1}'where id = '{2}'",
                          "",
                          "",
                          dr["ID"].ToString());
                    if (DALUse.ExecuteSql(sqlcancel) == 1)
                    {
                        MessageBox.Show("操作成功过！");
                        //uctlMessageBox.frmDisappearShow("取消绑定操作成功！");
                    }
                    else
                    {
                        MessageBox.Show("操作失败");
                    }

                    InitData();
                }
                else if (dr["NEWROWFLAG"].ToString() != "0" && dresult == DialogResult.Yes)
                {
                    MessageBox.Show("撤销为绑定项点击【取消】按钮。");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 将target 和local的值绑定在一起。
        /// </summary>
        public void BandingValue()
        {
            //try
            //{
            //    DataRow drlocal = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            //    DataRow drtarget = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //    DataRow drcompare = dtcompare.NewRow();
            //    drcompare["PT_ID"] = drtarget["PT_ID"].ToString();
            //    //drcompare["TYPE"] = drlocal["TYPE"].ToString();
            //    //drcompare["TYPE_NAME"] = drlocal["TYPE_NAME"].ToString();
            //    drcompare["ITEM_NAME"] = drtarget["FIELD"].ToString();//目标字段
            //    drcompare["ITEM_CODE"] = drtarget["FIELD_CODE"].ToString();
            //    drcompare["COMPARE_NAME"] = drlocal["FIELD"].ToString();
            //    drcompare["COMPARE_CODE"] = drlocal["FIELD_CODE"].ToString();
            //    drcompare["target_id"] = drtarget["id"].ToString();
            //    drcompare["local_id"] = drlocal["ID"].ToString();
            //    drcompare["NEWROWFLAG"] = "1";
            //    dtcompare.Rows.Add(drcompare);
            //    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            //    //gridView2.DeleteRow(gridView2.FocusedRowHandle);
            //    gridView1.RefreshData();
            //    gridView3.RefreshData();
            //    gridView2.RefreshData();
            //}
            //catch (Exception EX)
            //{
            //    MessageBox.Show(EX.ToString());
            //}

        }
        private void gc_local_DoubleClick(object sender, EventArgs e)
        {
            BandingValue();
        }

        private void cmb_target_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string sqltarget = string.Format("select distinct 0 NEWROWFLAG, T.* from PT_COMPARISON_DETAIL_DICT T WHERE  pt_id='{0}' and type_name='{1}'", ((DataRowView)cmb_pt_name.SelectedItem).Row["pt_id"].ToString(), ((DataRowView)cmb_target.SelectedItem).Row["type_name"].ToString());
                dtcompare = DALUse.Query(sqltarget).Tables[0];
                gc_comparison.DataSource = dtcompare;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb_local_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sqllocal = string.Format("select * from PT_LOCAL_DICT WHERE  type_name='{0}'", ((DataRowView)cmb_local.SelectedItem).Row["type_name"].ToString());
                dtlocal = DALUse.Query(sqllocal).Tables[0];
                gc_local.DataSource = dtlocal;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (gridView3.FocusedRowHandle < 0)
                {
                    return;
                }
                DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
                if (dr["NEWROWFLAG"].ToString() == "1")
                {
                    DataRow drcompare = gridView3.GetDataRow(gridView3.FocusedRowHandle);
                    drcompare["compare_name"] = "";
                    drcompare["compare_code"] = "";
                    drcompare["newrowflag"] = "0";
                    //dtcompare.Rows.Add(drcompare.ItemArray);
                }
                else
                {
                    MessageBox.Show("本行为已绑定行，如需解绑请点击【撤销绑定】");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
        private void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            sb.Length = 0;
            sb.Append('%');
            foreach (char c in txt_itemname.Text.ToCharArray())
            {
                sb.Append(c);
                sb.Append('%');
            }
            string sql = string.Format("select * from PT_LOCAL_DICT where  field like'{0}'", sb.ToString());
            gc_local.DataSource = DALUse.Query(sql).Tables[0];
        }
        private void btn_auto_compare_Click(object sender, EventArgs e)
        {
            #region 程序中对照真的好慢啊！！！
            /*
            List<string> sqllist = new List<string>();
            //JHEMR.EmrSysCom.frmLoadProgress flp = new JHEMR.EmrSysCom.frmLoadProgress();
            //int max = dttarget.Rows.Count * dtloacal.Rows.Count;
            //flp.setRange(0, max);
            //flp.Show();
            //flp.Refresh();
            //flp.setTipText("Loading....");
            //int step = 0;
            foreach (DataRow drtarget in dttarget.Rows)
            {
                foreach (DataRow drlocal in dtloacal.Rows)
                {
                    //flp.setPos(step);
                    if (drlocal["field"].ToString().Equals(drtarget["field"].ToString()))
                    {
                        string sqlinsert = string.Format("insert into PT_COMPARISON_DETAIL_DICT(pt_id,type,type_name,item_name,item_code,compare_name,compare_code,target_id,local_id) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                         drtarget["PT_ID"].ToString(),
                         "",
                        "",
                         drtarget["FIELD"].ToString(),
                         drtarget["FIELD_CODE"].ToString(),
                         drlocal["FIELD"].ToString(),
                         drlocal["FIELD_CODE"].ToString(),
                         drtarget["ID"].ToString(),
                         drlocal["ID"].ToString());
                        string sqlupdatetarget = string.Format("update PT_TARGET_DICT set compare_flag ='{0}' where id={1}", "1", drtarget["ID"].ToString());
                        string sqlupdatelocal = string.Format("update PT_TARGET_DICT set compare_flag = '{0}' where ID = {1}", "1", drlocal["ID"].ToString());
                        sqllist.Add(sqlinsert);
                        sqllist.Add(sqlupdatetarget);
                        sqllist.Add(sqlupdatelocal);
                        //dtloacal.Rows.Remove(drlocal);
                        //dttarget.Rows.Remove(drtarget);
                        //dtloacal.
                        rtb_showsql.Text = sqlinsert + "\n" + sqlupdatelocal + "\n" + sqlupdatetarget;
                    }
                    //step+=1;
                }
            }
            DALUse.ExecuteSqlTran(sqllist.ToArray());
            //flp.Close();
            InitData();
             */

            //    if (cmb_target.SelectedValue == null)
            //    {
            //        return;
            //    }
            //    cf.WaitingThreadStart();
            //    string sql = string.Format("select distinct 1 NEWROWFLAG, m.item_name, m.item_code,m.target_compare_flag,m.pt_id, m.target_id,n.compare_name,n.compare_code,n.local_compare_flag,n.local_id from (select t.field as item_name, t.field_code   as item_code,t.compare_flag as target_compare_flag, t.pt_id        as pt_id, t.id           as target_id from pt_target_dict t where compare_flag = '0' and t.pt_id={0}) m, (select f.field        as compare_name, f.field_code   as compare_code, f.compare_flag as local_compare_flag, f.id           as local_id from PT_LOCAL_DICT f where compare_flag = '0' and f.pt_id={1}) n where m.item_name = n.compare_name ", cmb_target.SelectedValue.ToString(), cmb_local.SelectedValue.ToString());
            //    dtcompare = DALUse.Query(sql).Tables[0];
            //    gc_comparison.DataSource = dtcompare;
            //    cf.WaitingThreadStop();
            //}
            #endregion
            string sql = "";
            if (rb_field.Checked && rb_yange.Checked)
            {
                foreach (DataRow dr in dtcompare.Rows)
                {
                    if (dr["compare_name"].ToString() == "")
                    {
                        sql = string.Format("select * from PT_LOCAL_DICT where pt_id = '{0}' and type_name = '{1}'and field ='{2}'", cmb_pt_name.SelectedValue.ToString(), cmb_local.SelectedValue.ToString(), dr["field"].ToString());
                        DataTable DTlocal = DALUse.Query(sql).Tables[0];
                        if (DTlocal.Rows.Count == 0)
                        {
                            continue;
                        }
                        DataRow drlocal = DTlocal.Rows[0];
                        dr["compare_name"] = drlocal["field"].ToString();
                        dr["compare_code"] = drlocal["field_code"].ToString();
                        dr["newrowflag"] = "1";
                        //dtcompare.Rows.Add(dr.ItemArray);
                    }

                }
            }
            else if (rb_code.Checked && rb_yange.Checked)
            {
                foreach (DataRow dr in dtcompare.Rows)
                {
                    if (dr["compare_name"].ToString() == "")
                    {
                        sql = string.Format("select * from PT_LOCAL_DICT where pt_id = '{0}' and type_name = '{1}'and field_code = '{2}'", cmb_pt_name.SelectedValue.ToString(), cmb_local.SelectedValue.ToString(), dr["field_code"].ToString());
                        DataTable DTlocal = DALUse.Query(sql).Tables[0];
                        if (DTlocal.Rows.Count == 0)
                        {
                            continue;
                        }
                        DataRow drlocal = DTlocal.Rows[0];
                        dr["compare_name"] = drlocal["field"].ToString();
                        dr["compare_code"] = drlocal["field_code"].ToString();
                        dr["newrowflag"] = "1";
                        //dtcompare.Rows.Add(dr.ItemArray);
                    }
                }
            }
            gc_comparison.DataSource = dtcompare;
            MessageBox.Show("对照完成："+dtcompare.Select("newrowflag =1").Length+"条数据，请点击【保存】按钮！");
        }



        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            DataRow drcompare = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            DataRow drlocal = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            drcompare["compare_name"] = drlocal["field"].ToString();
            drcompare["compare_code"] = drlocal["field_code"].ToString();
            drcompare["newrowflag"] = "1";
            //dtcompare.Rows.Add(drcompare.ItemArray);
        }
        public void InitDictData()
        {
            try
            {
                cmb_target.DataSource = DALUse.Query(string.Format("SELECT DISTINCT TYPE_NAME FROM PT_COMPARISON_DETAIL_DICT where PT_ID ='{0}'", ((DataRowView)cmb_pt_name.SelectedItem).Row["pt_id"].ToString())).Tables[0];
                cmb_target.DisplayMember = "TYPE_NAME";
                cmb_target.ValueMember = "TYPE_NAME";
                cmb_local.DataSource = DALUse.Query(string.Format("SELECT DISTINCT TYPE_NAME FROM PT_LOCAL_DICT where pt_id = '{0}'", ((DataRowView)cmb_pt_name.SelectedItem).Row["pt_id"].ToString())).Tables[0];
                cmb_local.DisplayMember = "TYPE_NAME";
                cmb_local.ValueMember = "TYPE_NAME";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cmb_pt_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDictData();
        }

        public void FindCompare()
        {
            sb.Length = 0;
            sb.Append('%');
            string sql = "";
            if (rb_field.Checked && rb_mohu.Checked)
            {
                foreach (char c in gridView3.GetDataRow(gridView3.FocusedRowHandle)["field"].ToString().ToCharArray())
                {
                    sb.Append(c);
                    sb.Append('%');
                }
                sql = string.Format("select * from PT_LOCAL_DICT where pt_id = '{0}' and type_name = '{1}'and field like'{2}'", cmb_pt_name.SelectedValue.ToString(), cmb_local.SelectedValue.ToString(), sb.ToString());
            }
            else if (rb_field.Checked && rb_yange.Checked)
            {

                sql = string.Format("select * from PT_LOCAL_DICT where pt_id = '{0}' and type_name = '{1}'and field ='{2}'", cmb_pt_name.SelectedValue.ToString(), cmb_local.SelectedValue.ToString(), gridView3.GetDataRow(gridView3.FocusedRowHandle)["field"].ToString());
            }
            else if (rb_code.Checked && rb_mohu.Checked)
            {
                foreach (char c in gridView3.GetDataRow(gridView3.FocusedRowHandle)["field_code"].ToString().ToCharArray())
                {
                    sb.Append(c);
                    sb.Append('%');
                }
                sql = string.Format("select * from PT_LOCAL_DICT where pt_id = '{0}' and type_name = '{1}'and field_code like'{2}'", cmb_pt_name.SelectedValue.ToString(), cmb_local.SelectedValue.ToString(), sb.ToString());
            }
            else if (rb_code.Checked && rb_yange.Checked)
            {
                sql = string.Format("select * from PT_LOCAL_DICT where pt_id = '{0}' and type_name = '{1}'and field_code = '{2}'", cmb_pt_name.SelectedValue.ToString(), cmb_local.SelectedValue.ToString(), gridView3.GetDataRow(gridView3.FocusedRowHandle)["field_code"].ToString());
            }
            gc_local.DataSource = DALUse.Query(sql).Tables[0];
        }
        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            FindCompare();
        }

        private void btn_deltargetdict_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("delete PT_COMPARISON_DETAIL_DICT where pt_id = '{0}' and type_name = '{1}'", cmb_pt_name.SelectedValue.ToString(), cmb_target.SelectedValue.ToString());
                if (MessageBox.Show(null, "确定删除所选字典？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (DALUse.ExecuteSql(sql) > 0)
                    {
                        InitDictData();
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_dellocaldict_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("delete PT_LOCAL_DICT where type_name = '{0}'",  cmb_local.SelectedValue.ToString());
                string sql1 = string.Format("update PT_COMPARISON_DETAIL_DICT set COMPARE_CODE = null ,COMPARE_NAME = null where  type_name = '{0}'", cmb_local.SelectedValue.ToString());
                if (MessageBox.Show(null, "确定删除所选字典？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        int r1 = DALUse.ExecuteSql(sql);
                    }
                    catch (Exception exp)
                    {
                        CommonFunction.WriteErrorLog(exp.ToString());
                    }
                    try
                    {
                        int r2 = DALUse.ExecuteSql(sql1);
                    }
                    catch (Exception exp)
                    {
                        CommonFunction.WriteErrorLog(exp.ToString());
                    }
                    InitDictData();
                }
            }
            catch (Exception exp)
            {
                CommonFunction.WriteErrorLog(exp.ToString());
            }
        }
    }
}
