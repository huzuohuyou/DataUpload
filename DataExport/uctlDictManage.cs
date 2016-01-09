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
    public partial class uctlDictManage : UserControl
    {
        public DataTable m_dtChapter = new DataTable();
        public DataTable m_dtDict = new DataTable();
        public DataSet m_dsExcel = new DataSet();
        public uctlDictManage()
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
                _strSQL = string.Format("delete PT_COMPARISON ");
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

        public void SetChapter()
        {
            string _strSQL = string.Format(@"select distinct(field_name) CHAPTER_NAME from pt_comparison");
            DataTable _dtField = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            m_dtChapter = _dtField;
        }

        public string GetCurrentChapter()
        {
            if (dataGridView4.CurrentRow==null)
            {
                return "";
            }
            return dataGridView4.CurrentRow.Cells["chapter_name"].Value.ToString();
        }

        public string GetCurrentDictItem()
        {
            if (dataGridView4.CurrentRow.Cells["LOCAL"].Value == null)
            {
                return "";
            }
            return dataGridView3.CurrentRow.Cells["LOCAL"].Value.ToString();
        }

        public void SetDict(string p_strChapterName)
        {
            string _strSQL = string.Format(@"select local_value,target_value from pt_comparison where field_name ='{0}'", p_strChapterName);
            DataTable _dt = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            m_dtDict = _dt;
        }

        public void InitData()
        {
            SetChapter();
            dataGridView4.DataSource = m_dtChapter.DefaultView;
            string _strChapterName = GetCurrentChapter();
            SetDict(_strChapterName);
            dataGridView3.DataSource = m_dtDict.DefaultView;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_dtChapter.Rows.Add("");
            dataGridView4.DataSource = m_dtChapter.DefaultView;
            //dataGridView4.Rows.Add();
            //dataGridView3.DataSource = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定删除此章节字典", "Message", MessageBoxButtons.YesNo))
            {
                string _strChapterName = GetCurrentChapter();
                string _strSQL = string.Format(@"delete from PT_COMPARISON where field_name = '{0}'", _strChapterName);
                CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
                InitData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定删除此字典条目", "Message", MessageBoxButtons.YesNo))
            {
                string _strChapterName = GetCurrentChapter();
                string _strLocalValue = GetCurrentDictItem();
                string _strSQL = string.Format(@"delete from pt_comparison where field_name = '{0}' and local_value ='{1}'", _strChapterName, _strLocalValue);
                int _n= CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //dataGridView3.Rows.Add();
            m_dtDict.Rows.Add("", "");
            dataGridView3.DataSource = m_dtDict.DefaultView;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string _strSQL = string.Format("delete pt_comparison where field_name = '{0}'", GetCurrentChapter());
            int _nn = CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
            foreach (DataGridViewRow var in dataGridView3.Rows)
            {
                _strSQL = string.Format(@"insert into pt_comparison(field_name,local_value,target_value) values('{0}','{1}','{2}')", GetCurrentChapter(), var.Cells["local"].Value.ToString(), var.Cells["target"].Value.ToString());
                int _n = CommonFunction.OleExecuteNonQuery(_strSQL, PublicVar.m_strEmrConnection);
            }
            InitData();
        }

        private void uctlDictManage_Load(object sender, EventArgs e)
        {
            InitData();
        }

        //private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    InitDict();
        //}

        public void InitDict()
        {
            string _strChapterName = GetCurrentChapter();
            SetDict(_strChapterName);
            dataGridView3.DataSource = m_dtDict.DefaultView;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            InitDict();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitData();
        }


    }
}
