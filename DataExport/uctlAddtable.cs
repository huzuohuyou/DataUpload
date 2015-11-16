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
    public partial class uctladdtable : UserControl
    {

        public static string pt_id = null;
        public static string table_id = null;
        public static string table_name = null;
        public static string kind = null;
        public static string oldtablename = null;
        public uctladdtable()
        {
            InitializeComponent();
            lab_id.Text = pt_id;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string sqlupdatetable = string.Format("update pt_tables_dict set table_name = '{0}' where id ='{1}'", txt_tablename.Text, table_id);
            string sqlupdatetable2 = string.Format("update PT_TARGET_FIELD set table_name = '{0}' where pt_id = '{1}' and table_name = '{2}'",txt_tablename.Text,pt_id,oldtablename);
            string sqlinserttable = "";
            //if (publicProperty.DATABASETYPE=="ORACLE")
            //{
            //Guid g= new Guid();
            sqlinserttable = string.Format("insert into pt_tables_dict(id,pt_id,table_name,exportflag) values('{2}','{0}','{1}','TRUE')", pt_id, txt_tablename.Text, Guid.NewGuid());
            //}
            //else if (publicProperty.DATABASETYPE=="SQLSERVER")
            //{
            //    sqlinserttable = string.Format("insert into pt_tables_dict(pt_id,table_name) values({0},'{1}')", pt_id, txt_tablename.Text);
            //}
            if (kind == "update" && txt_tablename.Text != "")
            {
                DALUse.ExecuteSql(sqlupdatetable);
                DALUse.ExecuteSql(sqlupdatetable2);
            }
            else if (kind == "add" && txt_tablename.Text != "")
            {
                DALUse.ExecuteSql(sqlinserttable);
            }
            else
            {
                MessageBox.Show("表名不能为空！");
            }
            this.FindForm().Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
