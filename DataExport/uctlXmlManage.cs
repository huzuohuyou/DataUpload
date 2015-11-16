using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using System.IO;

namespace DataExport
{
    public partial class uctlXmlManage : UserControl
    {
        public uctlXmlManage()
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


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _strXML = string.Empty;
            DataGridViewRow var = dataGridView1.CurrentRow;
            string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
            string _strMs = var.Cells["MS"].Value.ToString();
            _strXML = rtb_xml.Text;

            if (true==ExportXml.SaveXML(_strXML, _strTableName))
            {
                MessageBox.Show("±£´æ³É¹¦.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
            
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string _strXML = ExportXml.GetXML(dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString());
            this.rtb_xml.Text = _strXML;
        }

     
    }
}
