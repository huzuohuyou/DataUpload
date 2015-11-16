using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;

namespace DataExport
{
    public partial class uctlConfigDict : UserControl
    {
        public DataSet m_dsExcel = new DataSet();
        public uctlConfigDict()
        {
            InitializeComponent();
            string _strSQL = string.Format("select * from PT_COMPARISON");
            DataTable _dtDict = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView2.DataSource = _dtDict.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string _strPath = openFileDialog1.FileName;
            m_dsExcel = CommonFunction.ExcelToDataSet(_strPath, "Sheet1$", true, ToolFunction.CommonFunction.ExcelType.Excel2007);
            dataGridView1.DataSource = m_dsExcel.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _strSQL = string.Empty;
            if (m_dsExcel.Tables[0].Rows.Count > 0)
            {
                _strSQL = string.Format("delete PT_COMPARISON where FIELD_NAME = '{0}'", m_dsExcel.Tables[0].Rows[0]["FIELD_NAME"].ToString());
                CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                progressBar1.Maximum = m_dsExcel.Tables[0].Rows.Count;
                foreach (DataRow var in m_dsExcel.Tables[0].Rows)
                {
                    progressBar1.Value += 1;
                    _strSQL = string.Format("insert into PT_COMPARISON(FIELD_NAME,LOCAL_VALUE,TARGET_VALUE) VALUES('{0}','{1}','{2}')", var["FIELD_NAME"].ToString(), var["LOCAL_VALUE"].ToString(), var["TARGET_VALUE"].ToString());
                    CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                }
                progressBar1.Visible = false;
            }
            _strSQL = string.Format("select * from PT_COMPARISON");
            DataTable _dtDict =  CommonFunction.OleExecuteBySQL(_strSQL,"","EMR");
            dataGridView2.DataSource = _dtDict.DefaultView;
        }
    }
}
