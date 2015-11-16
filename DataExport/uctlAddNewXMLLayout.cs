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
    public partial class uctlAddNewXMLLayout : UserControl
    {
        public string pt_id = "";
        public uctlAddNewXMLLayout()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "";

                //if (publicProperty.DATABASETYPE=="ORACLE")
                //{
                //Guid g = new Guid();
                sql = string.Format("insert into pt_xml_config(layout_name,ID,FIELD_NAME,pt_id) values('{0}','{2}','根节点','{1}')", txt_layout_name.Text, pt_id,Guid.NewGuid());
                //}
                //else if (publicProperty.DATABASETYPE=="SQLSERVER")
                //{
                //    sql = string.Format("insert into pt_xml_config(layout_name,FIELD_NAME,pt_id) values('{0}','根节点',{1})", txt_layout_name.Text, int.Parse(pt_id));
                //}
                if ( DALUse.ExecuteSql(sql)==1)
                {
                    this.FindForm().Close();
                    uctlMessageBox.frmDisappearShow("添加成功！");
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
