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
    public partial class uctlXmlStrManage : UserControl
    {
        public uctlXmlStrManage()
        {
            InitializeComponent();
            InitData();
        }

        /// <summary>
        /// 获取当前对象名
        /// </summary>
        /// <returns></returns>
        public string GetCurrentObjectName()
        {
            if (dataGridView1.CurrentRow != null)
            {
                return dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString();
            }
            return "";
        }

        public void InitData()
        {
            string _strSQL = string.Format("select TABLE_NAME,MS  from pt_tables_dict ");
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView1.DataSource = _dtTemp;

            //dataGridView1.DataSource = _dtOject;
            _strSQL = string.Format(@"select  table_name,class,chapter_name,data_detail,'' CHOSE from pt_chapter_dict where table_name = '{0}'", GetCurrentObjectName());
            DataTable _dtChapter = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView2.DataSource = _dtChapter;
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
                MessageBox.Show("保存成功.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
            
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string _strXML = ExportXml.GetXML(dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString());
            this.rtb_xml.Text = _strXML;
        }

        /// <summary>
        /// 同步章节信息
        /// 2015-11-06
        /// 吴海龙
        /// </summary>
        public void SycnChapterData(string p_strObjectName, DataTable p_dtObject)
        {
            if (null == p_dtObject)
            {
                return;
            }
            foreach (DataRow var in p_dtObject.Rows)
            {
                if (!ExistChapter(p_strObjectName, var["CHAPTER_NAME"].ToString()))
                {
                    string _strSQL = string.Format(@"insert into pt_chapter_dict(table_name,chapter_name,class) values('{0}','{1}','{2}')", p_strObjectName, var["CHAPTER_NAME"].ToString(), var["CLASS"].ToString());
                    CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                }
            }
        }
        /// <summary>
        /// 判断是否存在章节
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <param name="p_strChapterName"></param>
        /// <returns></returns>
        public bool ExistChapter(string p_strObjectName, string p_strChapterName)
        {
            string _strSQL = string.Format(@"select count(*) mycount from pt_chapter_dict where table_name = '{0}' and chapter_name = '{1}'", p_strObjectName, p_strChapterName);
            DataTable _dtCount = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            if (_dtCount != null)
            {
                int _nCount = int.Parse(_dtCount.Rows[0]["mycount"].ToString());
                if (_nCount > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string _strBandedSQL = string.Empty;
            DataGridViewRow _drObject = dataGridView1.CurrentRow;
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            string _strObjectName = _drObject.Cells["TABLE_NAME"].Value.ToString();
            string _strSQL = string.Empty;// string.Format(@"delete pt_chapter_dict where table_name = '{0}'", _strObjectName);
            DataTable _dtObject = null;
            if (DialogResult.OK == MessageBox.Show("确定同步[章节]数据？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                _dtObject = ExportXml.GetObject(_strObjectName);
                //rtb_sql.Text = _strBandedSQL;
                SycnChapterData(_strObjectName, _dtObject);
                InitData();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

     
    }
}
