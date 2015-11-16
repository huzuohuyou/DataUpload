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
    public partial class uctlRenameNode : UserControl
    {
        public static string id = null;
        public uctlRenameNode()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update pt_xml_config set field_name = '{0}' where id = '{1}'",txt_newnodename.Text,id);
                DALUse.ExecuteSql(sql);
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
