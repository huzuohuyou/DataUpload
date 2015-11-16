using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
namespace DataExport
{
    public partial class uctlUploadDataToDatabase : UserControl
    {
        public uctlUploadDataToDatabase()
        {
            InitializeComponent();
            InitComboxDatasource();
        }

        public void InitComboxDatasource()
        {
            try
            {
                cmb_pt.DataSource = DALUse.Query(PublicProperty.SqlGetPT).Tables[0];
                cmb_pt.ValueMember = "pt_id";
                cmb_pt.DisplayMember = "pt_name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb_pt_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string sqlgettargettable = string.Format("select  TABLE_NAME FROM pt_tables_dict WHERE PT_ID = '{0}'", ((DataRowView)cmb_pt.SelectedItem).Row["PT_ID"].ToString());
            gc_target_table.DataSource = DALUse.Query(sqlgettargettable).Tables[0];
            string sqlgetdata = string.Format("select * from PT_Up_DataBase_Setting where pt_id = '{0}'", ((DataRowView)cmb_pt.SelectedItem).Row["PT_ID"].ToString());
            DataTable connstr = DALUse.Query(sqlgetdata).Tables[0];
            if (connstr.Rows.Count == 1)
            {
                txt_database.Text = connstr.Rows[0]["PT_DataBase"].ToString();
                txt_connstr.Text = connstr.Rows[0]["Joins"].ToString();
            }
            else
            {
                txt_connstr.Text = "";
                txt_database.Text = "";
                txt_databasetable.Text = "";
                txt_targetbale.Text = "";
            }
            getData();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle<0)
            {
                return;
            }
            txt_targetbale.Text = gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString();
            txt_databasetable.Text = gridView1.GetDataRow(gridView1.FocusedRowHandle)["TABLE_NAME"].ToString();
        }

        public void getData()
        {
            try
            {
                string sqlgetdata = string.Format("select * from PT_Up_DataBase_Table where pt_id = '{0}'", ((DataRowView)cmb_pt.SelectedItem).Row["pt_id"].ToString());
                gc_upload.DataSource = DALUse.Query(sqlgetdata).Tables[0];
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
                if (cmb_pt.SelectedItem==null)
                {
                    return;
                }
                string sqlinsert = string.Format("insert into PT_Up_DataBase_Table(PT_ID,PT_TABLE,PT_DATA,PT_DATA_TABLE,ID) values('{0}','{1}','{2}','{3}','{4}')", cmb_pt.SelectedValue.ToString(), txt_targetbale.Text, txt_database.Text, txt_databasetable.Text, Guid.NewGuid().ToString());
                if (DALUse.ExecuteSql(sqlinsert) == 1)
                {
                    getData();
                    MessageBox.Show("保存成功！");
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0)
            {
                return;
            }
            try
            {
                string sqldel = string.Format("delete from PT_Up_DataBase_Table where id = '{0}'", gridView2.GetDataRow(gridView2.FocusedRowHandle)["ID"].ToString());
                if (DALUse.ExecuteSql(sqldel) == 1)
                {
                    getData();
                    MessageBox.Show("删除成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_sqlconn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_pt.SelectedItem == null)
                {
                    return;
                }
                string sqlgetdata = string.Format("select * from PT_Up_DataBase_Setting where pt_id = '{0}'", ((DataRowView)cmb_pt.SelectedItem).Row["PT_ID"].ToString());
                DataTable connstr = DALUse.Query(sqlgetdata).Tables[0];
                if (connstr.Rows.Count == 1)
                {
                    string sqlupdate = string.Format("update PT_Up_DataBase_Setting set joins = '{0}',PT_DataBase ='{2}' where pt_id = '{1}'", txt_connstr.Text, cmb_pt.SelectedValue.ToString(), txt_database.Text);
                    if (DALUse.ExecuteSql(sqlupdate) == 1)
                    {
                        MessageBox.Show("修改成功！");
                    }
                }
                else if (connstr.Rows.Count == 0)
                {
                    string sqlinsert = string.Format("insert into  PT_Up_DataBase_Setting (pt_id,Joins,PT_DataBase) values('{0}','{1}','{2}')",
                        cmb_pt.SelectedValue.ToString(),
                        txt_connstr.Text,
                        txt_database.Text);
                    if (DALUse.ExecuteSql(sqlinsert) == 1)
                    {
                        MessageBox.Show("添加成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
