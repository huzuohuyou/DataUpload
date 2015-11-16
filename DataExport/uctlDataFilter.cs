using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using ToolFunction;
namespace DataExport
{
    public partial class uctlDataFilter : UserControl
    {
        DataTable dtselected = new DataTable();
        bool isopenflag = false;
        public uctlDataFilter()
        {
            InitializeComponent();
            
            GetTempList();
            InitComboxDatasource();
            //GetSelectedList();
            isopenflag = true;
            GetField();
        }

        public void InitComboxDatasource()
        {
            try
            {
                cmb_pt.DataSource = DALUse.Query(PublicProperty.SqlGetPT).Tables[0];
                cmb_pt.DisplayMember = "pt_name";
                cmb_pt.ValueMember = "pt_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GetSelectedList()
        {
            //try
            //{
            //    string sql = string.Format("select distinct * from (select CLASS_NAME,ITEM_CODE,ITEM_NAME from pt_temp_field where pt_id = '{0}')",cmb_pt.SelectedValue);
            //    dtselected = DALUse.Query(sql).Tables[0];
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
          
        }
        public void GetTempList()
        {
            string strSql = "";
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                strSql = "select * from emr_monitor_item_dict";
                //gc_AllTemplet.DataSource = CommonFunction.ExecuteBySQL(strSql, dict, "AllTemp");
                gc_AllTemplet.DataSource = DALUse.Query(strSql).Tables[0];
                //string sqlselect = "SELECT 1 as INPUT_FLAG,a.MR_CODE,a.MR_CLASS,a.MR_NAME,a.DEPT_CODE,a.ACCESS_PATH,a.TEMPLET_FILE_NAME FROM MR_TEMPLET_INDEX a WHERE  (a.mr_attr = '1' and a.TEMPLET_FILE_NAME IS NOT NULL and 1=0) ";
                //strSql += " UNION ";
                //strSql += "SELECT 1 as INPUT_FLAG,c.MR_CODE,c.MR_CLASS,c.MR_NAME,c.DEPT_CODE,c.ACCESS_PATH,c.TEMPLET_FILE_NAME FROM MR_ITEM_INDEX c WHERE  (c.mr_attr = '1' and c.TEMPLET_FILE_NAME IS NOT NULL and 1=0)";
                //dtselected = CommonFunction.ExecuteBySQL(sqlselect, dict, "SelectTemp");
                //gc_selected.DataSource = dtselected;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow dradd = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                //params object[]
                DataRow dr = dtselected.NewRow();
                dr["CLASS_NAME"] = dradd["CLASS_NAME"].ToString();
                dr["ITEM_CODE"] = dradd["ITEM_CODE"].ToString();
                dr["ITEM_NAME"] = dradd["ITEM_NAME"].ToString();
                dtselected.Rows.Add(dr.ItemArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            if (cmb_pt.SelectedItem==null)
            {
                return;
            }
            Form f = new Form();
            f.FormBorderStyle = FormBorderStyle.None;
            f.Size = new Size(440, 147);
            uctlAddTempField atf = new uctlAddTempField();
            uctlAddTempField.pt_name = cmb_pt.Text.ToString();
            uctlAddTempField.pt_id = cmb_pt.SelectedValue.ToString();
            if (gridView1.FocusedRowHandle<0)
            {
                MessageBox.Show("未选择行！");
                return;
            }
            uctlAddTempField.class_name = gridView1.GetDataRow(gridView1.FocusedRowHandle)["class_name"].ToString();
            uctlAddTempField.item_name = gridView1.GetDataRow(gridView1.FocusedRowHandle)["item_name"].ToString();
            uctlAddTempField.item_code = gridView1.GetDataRow(gridView1.FocusedRowHandle)["item_code"].ToString();
            CommonFunction.AddForm(f,atf);
            GetField();
        }

        public void GetField()
        {
            if (gridView1.FocusedRowHandle < 0 || cmb_pt.SelectedItem==null)
            {
                return;
            }
            string sql = string.Format("select CENGCI_CODE,FIELD_NAME from pt_temp_field where pt_name = '{0}' and item_name='{1}'", ((DataRowView)cmb_pt.SelectedItem).Row["pt_name"].ToString(), gridView1.GetDataRow(gridView1.FocusedRowHandle)["item_name"].ToString());
            gc_field.DataSource = DALUse.Query(sql).Tables[0];
        }
        
        private void btn_del_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                MessageBox.Show("未选择行！");
                return;
            }
            if (DialogResult.No ==MessageBox.Show(null,"确定删除所选项？","删除",MessageBoxButtons.YesNo))
            {
                return;
            }
            DALUse.ExecuteSql(string.Format("delete from pt_temp_field where class_name = '{0}'", gridView1.GetDataRow(gridView1.FocusedRowHandle)["class_name"].ToString()));
            //GetSelectedList();//获取选择的模板
            GetField();//获取字段
        }

        private void cmb_pt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isopenflag)
            {
                //GetSelectedList();
                GetField();
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GetField();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
