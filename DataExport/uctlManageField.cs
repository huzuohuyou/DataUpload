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
    public partial class uctlManageField : UserControl
    {
        public static string pt_id = "";
        public static string pt_name = "";
        public static string table_name = "";
        public static string actionflag = "";
        public static string compare_id = "";
        public uctlManageField()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }


        public void InsertPt_Target_Field()
        {
            try
            {
                if (actionflag == "add")
                {
                    string sql = "";
                    //if (publicProperty.DATABASETYPE=="ORACLE")
                    //{
                    //Guid g = new Guid();
                    sql = string.Format("insert into PT_TARGET_FIELD(compare_id,pt_name,table_name,field_name,field,field_type,compare_name,pt_id) values('{7}','{0}','{1}','{2}','{3}','{4}','{5}','{6}')", pt_name, table_name, txt_fieldname.Text.Trim(), txt_field.Text.Trim(), cmb_type.Text.Trim(), "", pt_id, Guid.NewGuid());
                    //}
                    //else if (publicProperty.DATABASETYPE=="SQLSERVER")
                    //{
                    //    sql = string.Format("insert into PT_TARGET_FIELD(pt_name,table_name,field_name,field,field_type,compare_name,pt_id) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", pt_name, table_name, txt_fieldname.Text.Trim(), txt_field.Text.Trim(), cmb_type.Text.Trim(), "", pt_id);
                    //}

                    int effectresult = DALUse.ExecuteSql(sql);
                    if (effectresult == 1)
                    {
                        MessageBox.Show("保存成功！");
                    }
                    else
                    {
                        return;
                    }
                }
                else if (actionflag == "update")
                {
                    if (MessageBox.Show(null, "确定修改该所选项！", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string sql = string.Format("update PT_TARGET_FIELD set field = '{0}',field_name='{1}',field_type='{2}' where compare_id ='{3}'", txt_field.Text.Trim(), txt_fieldname.Text.Trim(), cmb_type.Text.Trim(), compare_id);
                        if (DALUse.ExecuteSql(sql) == 1)
                        {
                            uctlMessageBox.frmDisappearShow("修改操作完成！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this.FindForm().Close();
            
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            InsertPt_Target_Field();
            txt_field.Text = "";
            txt_fieldname.Text = "";
        }
    }
}
