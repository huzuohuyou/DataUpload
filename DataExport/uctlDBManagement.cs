using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using ToolFunction;
using System.Configuration;
using ConfirmFileName;
using System.IO;
using System.Diagnostics;
namespace DataExport
{
    public partial class uctlDBManagement : UserControl
    {
        public uctlDBManagement()
        {
            InitializeComponent();
            InitData();
        }

        public void InitData()
        {

            string _strSQL = string.Format("select TABLE_NAME,MS,EXPORTFLAG  from pt_tables_dict ");
            DataTable _dtOject = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtOject==null)
            {
                MessageBox.Show("δ�B�Ӕ�����");
            }
            dataGridView1.DataSource = _dtOject;
            _strSQL = string.Format(@"select  table_name,class,chapter_name,data_detail,'' CHOSE from pt_chapter_dict where table_name = '{0}'", GetCurrentObjectName());
            DataTable _dtChapter = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView2.DataSource = _dtChapter;
        }

        /// <summary>
        /// ��ȡ��ǰ������
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

        /// <summary>
        /// ��ȡ��ǰ�½���
        /// </summary>
        /// <returns></returns>
        public string GetCurrentChapterName()
        {
            if (dataGridView2.CurrentRow != null)
            {
                return dataGridView2.CurrentRow.Cells["CHAPTER_NAME"].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// ��ȡ��ǰ�����
        /// </summary>
        /// <returns></returns>
        public string GetCurrentChapterClass()
        {
            if (dataGridView2.CurrentRow != null)
            {
                return dataGridView2.CurrentRow.Cells["CLASS"].Value.ToString();
            }
            return "";
        }




        private void button1_Click(object sender, EventArgs e)
        {
            string _strSQL = string.Empty;
            DataGridViewRow var = dataGridView1.CurrentRow;
            string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
            string _strExportFlag = var.Cells["EXPORTFLAG"].Value.ToString();
            string _strMs = var.Cells["MS"].Value.ToString();
            //string _strSQLValue = rtb_sql.Text.Trim();
            //if (_strSQLValue=="")
            //{
            //    MessageBox.Show("SQL����Ϊ��.");
            //    return;
            //}
            _strSQL = string.Format("select count(*) mycount from pt_tables_dict where  TABLE_NAME = '{0}'", _strTableName);
            int _nCount = int.Parse(CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR").Rows[0]["mycount"].ToString());
            if (0 == _nCount)
            {
                _strSQL = string.Format("insert into pt_tables_dict(table_name,MS,exportflag) values('{0}','{1}','{2}')", _strTableName, _strMs, _strExportFlag);
            }
            else
            {
                _strSQL = string.Format("update pt_tables_dict set table_name= '{0}',ms = '{1}' ,exportflag ='{2}' where table_name = '{0}'", _strTableName, _strMs, _strExportFlag);
            }

            //if ((DialogResult.OK == MessageBox.Show("ȷ������?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)) && (_strSQLValue.Length > 0))
            //{
            //    ExportDB.SaveSQL(_strSQLValue, _strTableName);
            //if (1 == CommonFunction.OleExecuteNonQuery(_strSQL, "EMR"))
            //{
            //    MessageBox.Show("����ɹ�!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            if (1 == CommonFunction.OleExecuteNonQuery(_strSQL, "EMR"))
            {
                uctlMessageBox.frmDisappearShow("����ɹ���");
            }
            else
            {
                uctlMessageBox.frmDisappearShow("����ʧ��,���������־��");
            }
            //}
        }
    


        private void button2_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Add();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string _strSQLValue = ExportDB.GetSQL(dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString());
            rtb_sql.Text = _strSQLValue;
        }
   

        private void button3_Click(object sender, EventArgs e)
        {
            //string _strExePath = Application.StartupPath + @"\MessagePlatform.exe";
            //Process.Start(_strExePath);
            //RemoteMessage.InitClient();
            DataTable _dt = (DataTable)dataGridView1.DataSource;
            ShowFixInfo bs = new ShowFixInfo(_dt);
            //CommonFunction.AddForm2(bs);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show(this, "ȷ��ɾ��", "", MessageBoxButtons.OKCancel))
            {
                DataGridViewRow var = dataGridView1.CurrentRow;
                string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
                string _strSQL = string.Format("delete pt_tables_dict where table_name = '{0}'", _strTableName);
                CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                InitData();
            } 
           
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string _strSQL = "update pt_tables_dict set exportflag = 'TRUE'";
                CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
            }
            else
            {
                string _strSQL = "update pt_tables_dict set exportflag = 'FALSE'";
                CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
            }
            InitData();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow _drTemp = dataGridView1.CurrentRow;
            string _strTableName = _drTemp.Cells["TABLE_NAME"].Value.ToString();
            string _strMs = _drTemp.Cells["MS"].Value.ToString();
            DataTable _dt = (DataTable)dataGridView1.DataSource;
            ShowFixInfo bs = new ShowFixInfo(_strTableName, _strMs);
            CommonFunction.AddForm2(bs);
        }

        public void SaveChapter()
        {
            DataGridViewRow _drObject = dataGridView1.CurrentRow;
            DataGridViewRow _drChapter = dataGridView2.CurrentRow;
            string _strTableName = _drObject.Cells["TABLE_NAME"].Value.ToString();
            string _strChapterName = _drChapter.Cells["CHAPTER_NAME"].Value.ToString();
            string _strClass = _drChapter.Cells["CLASS"].Value.ToString();
            string _strDataDetail = _drChapter.Cells["DATA_DETAIL"].Value.ToString().Replace("'", "''");
            string _strSQL = @"update pt_chapter_dict set data_detail = '{0}', class = '{1}' where table_name = '{2}' and chapter_name = '{3}'";
            _strSQL = string.Format(_strSQL, _strDataDetail, _strClass, _strTableName, _strChapterName);
            if (1 == CommonFunction.OleExecuteNonQuery(_strSQL, "EMR"))
            {
                uctlMessageBox.frmDisappearShow("����ɹ���");
            }
            else
            {
                uctlMessageBox.frmDisappearShow("����ʧ��,���������־��");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataGridViewRow var = dataGridView1.CurrentRow;
            string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
            string _strSQLValue = rtb_sql.Text.Trim();
            ExportDB.SaveSQL(_strSQLValue, _strTableName);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string _strBandedSQL = string.Empty;
            DataGridViewRow _drObject = dataGridView1.CurrentRow;
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            string _strObjectName = _drObject.Cells["TABLE_NAME"].Value.ToString();
            string _strSQL = string.Empty;// string.Format(@"delete pt_chapter_dict where table_name = '{0}'", _strObjectName);
            DataTable _dtObject = null;
            if (DialogResult.OK == MessageBox.Show("ȷ��ͬ��[�½�]���ݣ�", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                //CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                switch (_strExportType)
                {
                    case "DB":
                        _strBandedSQL = ExportDB.GetObjectBandedSQL(_strObjectName);
                        break;
                    case "XML":
                        _dtObject = ExportXml.GetObject(_strObjectName);
                        break;
                    case "EXCEL":
                        break;
                    case "DBF":
                        break;
                    default:
                        break;
                }
                rtb_sql.Text = _strBandedSQL;
                //SycnChapterData(_strObjectName, _dtObject);
                InitData();
            }
        }

        /// <summary>
        /// ͬ���½���Ϣ
        /// 2015-11-06
        /// �⺣��
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
        /// �ж��Ƿ�����½�
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

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string _strType = GetCurrentChapterClass();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == 4)
            {
                string _strTableChapter = dataGridView2.CurrentRow.Cells["CHAPTER_NAME"].Value.ToString();
                string _strDataDetail = dataGridView2.CurrentRow.Cells["DATA_DETAIL"].Value.ToString();
                DBTemplet dt = new DBTemplet(_strTableChapter, _strDataDetail);
                dt.ShowDialog();
                if (dt.m_bSave)
                {
                    dataGridView2.CurrentRow.Cells["DATA_DETAIL"].Value = dt.m_strDataDetail;
                    dataGridView2.CurrentRow.Cells["CLASS"].Value = dt.m_strClass;
                    SaveChapter();
                }
                
            }
        }


    }
}
