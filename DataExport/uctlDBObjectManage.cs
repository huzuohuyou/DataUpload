using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using JHEMR.EmrSysDAL;
using ToolFunction;

namespace DataExport
{
    public partial class uctlDBObjectManage : UserControl
    {
        public uctlDBObjectManage()
        {
            InitializeComponent();
            InitData();
        }
        public void InitData()
        {
            string _strSQL = string.Format("select TABLE_NAME,MS  from pt_tables_dict ");
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView1.DataSource = _dtTemp;
        }
       
    }
}
