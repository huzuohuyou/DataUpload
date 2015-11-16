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
    public partial class uctlShowImportField : UserControl
    {
        public DataSet myDs = new DataSet();
        public string table_name = "";
        public string pt_name = "";
        public string pt_id = "";
        public uctlShowImportField()
        {
            
            InitializeComponent();
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //t.WaitingThreadStart();
            string insertSql = "";
            int insertcount = 0;
            if (myDs != null && myDs.Tables[0].Rows.Count > 0)
            {
                string insertColumnString = "pt_id,pt_name,table_name,compare_id,field_name,field,field_type";
                DataTable dt = myDs.Tables[0];
                try
                {
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[0].ToString() == "")
                        {
                            continue;
                        }
                        string insertValueString = "";
                        insertValueString = "'" + pt_id + "','" + pt_name + "','" + table_name + "','" + Guid.NewGuid().ToString() + "',";
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            insertValueString += string.Format("'{0}',", dr[i].ToString().Replace("'", "''"));
                        }
                        insertValueString = insertValueString.Trim(',');
                        insertSql = string.Format(@"INSERT INTO {0} ({1}) VALUES({2})", "pt_target_field", insertColumnString, insertValueString);
                        if (DALUse.ExecuteSql(insertSql)==1)
                        {
                            insertcount++;
                        } 
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (insertcount != 0)
                {
                    MessageBox.Show("成功导入" + insertcount.ToString() + "条数据！");
                }
            }
            //t.WaitingThreadStop();
        }

        private void uctlShowImportField_Load(object sender, EventArgs e)
        {
            if (myDs.Tables.Count != 0)
            {
                gridControl1.DataSource = myDs.Tables[0].DefaultView;
            }
        }
    }
}
