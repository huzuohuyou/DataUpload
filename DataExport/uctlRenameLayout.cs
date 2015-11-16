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
    public partial class uctlRenameLayout : UserControl
    {
        public static string  layout_name = null;
        public uctlRenameLayout()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update pt_xml_config set layout_name = '{0}' where layout_name = '{1}'", txt_layoutname.Text, layout_name);
                DALUse.ExecuteSql(sql);
                this.FindForm().Close();
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
