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
    public partial class uctlAddTempField : UserControl
    {
        public static string pt_name;
        public static string class_name;
        public static string item_code;
        public static string item_name;
        public static string pt_id;
        public uctlAddTempField()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                bool ischecked = rb_hong.Checked | rb_element.Checked | rb_cengci.Checked;
                if (!ischecked)
                {
                    MessageBox.Show("请选择保存类型！");
                    return;
                }
                string sqlinsert = "";
                string sourcetype = "";
                //if (publicProperty.DATABASETYPE=="ORACLE")
                //{
                //Guid g = new Guid();
                if (rb_cengci.Checked)//层次号
                {
                    sourcetype = "0";
                    sqlinsert = string.Format("insert into pt_temp_field(id,pt_name,class_name,item_name,item_code,cengci_code,field_name,pt_id,sourcetype) values('{8}','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", pt_name, class_name, item_name, item_code, txt_cengci_code.Text.ToString(), txt_field.Text.ToString(), pt_id, sourcetype, Guid.NewGuid());
                }
                else if (rb_hong.Checked)//宏
                {
                    sourcetype = "1";
                    sqlinsert = string.Format("insert into pt_temp_field(id,pt_name,class_name,item_name,item_code,field_name,cengci_code,pt_id,sourcetype) values('{8}','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", pt_name, class_name, item_name, item_code, txt_field.Text.ToString(), "**", pt_id, sourcetype,Guid.NewGuid());
                }
                else if (rb_element.Checked)//元素
                {
                    sourcetype = "2";
                    sqlinsert = string.Format("insert into pt_temp_field(id,pt_name,class_name,item_name,item_code,field_name,cengci_code,pt_id,sourcetype) values('{8}','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", pt_name, class_name, item_name, item_code, txt_field.Text.ToString(), "**", pt_id, sourcetype, Guid.NewGuid());
                }
                //}
                //else if (publicProperty.DATABASETYPE =="mssqlserver")
                //{
                //    if (rb_cengci.Checked)//层次号
                //    {
                //        sourcetype = "0";
                //        sqlinsert = string.Format("insert into pt_temp_field(pt_name,class_name,item_name,item_code,cengci_code,field_name,pt_id,sourcetype) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", pt_name, class_name, item_name, item_code, txt_cengci_code.Text.ToString(), txt_field.Text.ToString(), pt_id, sourcetype);
                //    }
                //    else if (rb_hong.Checked)//宏
                //    {
                //        sourcetype = "1";
                //        sqlinsert = string.Format("insert into pt_temp_field(pt_name,class_name,item_name,item_code,field_name,cengci_code,pt_id,sourcetype) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", pt_name, class_name, item_name, item_code, txt_field.Text.ToString(), "**", pt_id, sourcetype);
                //    }
                //    else if (rb_element.Checked)//元素
                //    {
                //        sourcetype = "2";
                //        sqlinsert = string.Format("insert into pt_temp_field(pt_name,class_name,item_name,item_code,field_name,cengci_code,pt_id,sourcetype) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", pt_name, class_name, item_name, item_code, txt_field.Text.ToString(), "**", pt_id, sourcetype);
                //    }
                //}
                
                DALUse.ExecuteSql(sqlinsert);
                MessageBox.Show("保存成功！");
                this.FindForm().Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void rb_cengci_Click(object sender, EventArgs e)
        {
            txt_cengci_code.Enabled = true;
          

        }

        private void rb_hong_Click(object sender, EventArgs e)
        {
           
            txt_cengci_code.Enabled = false;
        }

        private void rb_element_Click(object sender, EventArgs e)
        {
           
            txt_cengci_code.Enabled = false;
        }
    }
}
