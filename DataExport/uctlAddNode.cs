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
    public partial class uctlAddNode : UserControl
    {
        public static string pt_id = null;
        public static string parientid = null;
        public bool isfirstopen = false;
        public static string layout_name = "";
        public uctlAddNode()
        {
            
            InitializeComponent();
            lab_parientid.Text = parientid;
            InitGc_nodeDataSource();
        }
      
        /// <summary>
        /// 列表数据源
        /// </summary>
        public void InitGc_nodeDataSource()
        {
            string sql = string.Format("select * from pt_comparison where pt_id ='{0}' ", pt_id);
            gc_nodelist.DataSource = DALUse.Query(sql).Tables[0];
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_node_name.Text!="")
                {
                    string multiple = "";
                    if (rb_no.Checked)
                    {
                        multiple = "NO";
                    }
                    else if (rb_yes.Checked)
                    {
                        multiple = "YES";
                    }
                    string sqlinsert = "";
                    //if (publicProperty.DATABASETYPE == "ORACLE")
                    //{
                    //Guid g = new Guid();
                    string field = "";
                    if (ckb_banding.Checked)
                    {
                        field = gridView1.FocusedRowHandle >= 0 ? gridView1.GetDataRow(gridView1.FocusedRowHandle)["COMPARE_NAME"].ToString() : "";
                    }
                    sqlinsert = string.Format("insert into pt_xml_config(id,parient_id,field_name,field,pt_id,multiple,layout_name) values('{5}','{0}','{1}','{2}','{3}','{4}','{6}')",
                        lab_parientid.Text.ToString() == "" ? "-1" : lab_parientid.Text.ToString(),
                        txt_node_name.Text,
                        field,
                        pt_id,
                        multiple,
                        Guid.NewGuid(),
                        layout_name
                        );
                    //}
                    //else if (publicProperty.DATABASETYPE=="SQLSERVER")
                    //{
                    //    string.Format("insert into pt_xml_config(parient_id,field_name,field,pt_id,multipel) values({0},'{1}','{2}',{3},'{4}')",
                    //    lab_parientid.Text.ToString() == "" ? "-1" : lab_parientid.Text.ToString(),
                    //    txt_node_name.Text,
                    //    gridView1.FocusedRowHandle >= 0 ? gridView1.GetDataRow(gridView1.FocusedRowHandle)["COMPARE_NAME"].ToString() : "",
                    //    pt_id,
                    //    multiple);
                    //}   
                       
                    DALUse.ExecuteSql(sqlinsert);
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            this.FindForm().Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            txt_node_name.Text = gridView1.GetFocusedValue().ToString();
        }

        private void cmb_tablename_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitGc_nodeDataSource();
        }


    }
}
