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
    public partial class uctlAddpt : UserControl
    {
        public static string kind = null;
        public static string ptid = null;
        public static string pt_name = null;
        public uctlAddpt()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string sqlupdatept = string.Format("update pt_dict set pt_name = '{0}' where pt_id ='{1}'", txt_pt_name.Text, ptid);
            string sqlinsertpt = "";
            //if (publicProperty.DATABASETYPE =="ORACLE")
            //{
            //Guid g = new Guid();
                sqlinsertpt = string.Format("insert into pt_dict(pt_id,pt_name) values('{1}','{0}')", txt_pt_name.Text,Guid.NewGuid());
            //}
            //else if (true)
            //{
            //    sqlinsertpt = string.Format("insert into pt_dict(pt_name) values('{0}')", txt_pt_name.Text);
            //}
            
            try
            {
                if (kind == "update" && txt_pt_name.Text != "")
                {

                    DALUse.ExecuteSql(sqlupdatept);
                }
                else if (kind == "add" && txt_pt_name.Text != "")
                {
                    DALUse.ExecuteSql(sqlinsertpt);
                }
                else
                {
                    MessageBox.Show("平台名不能为空！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.FindForm().Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
