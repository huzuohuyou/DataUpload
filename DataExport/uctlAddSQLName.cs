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
    public partial class uctlAddSQLName : UserControl
    {
        public static string sql = null;
        public static string ptid = null;
        public static string table_id = null;
        public static string table_name = null;
        public uctlAddSQLName()
        {
            InitializeComponent();
            textBox1.Text = table_name;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                string s = sql.Replace("'", "''");
                string sqlinsert = string.Format("insert into pt_sql(pt_id,sql_name,sql,table_id,table_name) values('{0}','{1}','{2}','{3}','{4}')", ptid, txt_sqlname.Text, s, table_id,table_name);
                if (DALUse.ExecuteSql(sqlinsert)!=0)
                {
                    MessageBox.Show("±£´æ³É¹¦£¡");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_sqlname_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

      
    }
}
