using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using JHEMR.EmrSysDAL;
using System.Xml;

namespace DataExport
{
    public partial class uctlDataExchange : UserControl
    {
        DataTable source = null;
        CommonFunction cf = new CommonFunction();
        bool openflag = false;
        public uctlDataExchange()
        {
            InitializeComponent();
            InitComBoxDataSource();
            openflag = true;
            GetKindData();
        }
        /// <summary>
        /// 将获取到的数据进行字典转换
        /// </summary>
        /// <param name="ds">病人列表集合</param>
        /// <param name="pt_id">目标平台的字典标准</param>
        /// <returns></returns>
        public DataSet DoExchange(DataSet ds, string pt_id)
        {
            try
            {
                string sqltype = string.Format("select distinct m.type,n.compare_name from pt_dict m,pt_comparison n where m.pt_id = '{0}' and m.type = n.field", pt_id);
                DataTable dttype = DALUse.Query(sqltype).Tables[0];
                string sqlcompare = "select * from pt_comparison_detail_dict t where pt_id ='{0}' and type = '{1}' ";
                foreach (DataTable mydt in ds.Tables)
                {
                    foreach (DataRow drsource in mydt.Rows)
                    {
                        foreach (DataColumn dcsource in mydt.Columns)
                        {
                            foreach (DataRow drtype in dttype.Rows)
                            {
                                if (dcsource.ToString() == drtype["compare_name"].ToString())
                                {
                                    string sql = string.Format(sqlcompare, pt_id, drtype["type"].ToString());
                                    DataTable dt = DALUse.Query(sql).Tables[0];
                                    if (dt.Rows.Count != 0)
                                    {
                                        foreach (DataRow drreplace in dt.Rows)
                                        {
                                            if (drreplace["compare_name"].ToString() == drsource[dcsource].ToString())
                                            {
                                                drsource[dcsource] = drreplace["item_name"].ToString();
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return ds;
        }
        private void btn_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtb_sql.Text.Length == 0)
                {
                    MessageBox.Show("无效SQL语句！");
                    return;
                }
                gridView1.Columns.Clear();
                gc_querycontent.DataMember = null;
                source = DALUse.Query(string.Format(rtb_sql.Text)).Tables[0];
                DoExchange(source.DataSet, cmb_pt.SelectedValue.ToString());
                gc_querycontent.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// 初始化cmb_pt数据源。
        /// </summary>
        private void InitComBoxDataSource()
        {
            //try
            //{
            //    cmb_pt.DataSource = cmb_pt.DataSource = DALUse.Query(publicProperty.sqlGetPT).Tables[0];
            //    cmb_pt.DisplayMember = "pt_name";
            //    cmb_pt.ValueMember = "pt_id";
            //    string sqlxml = "select distinct layout_name from pt_xml_config";
            //    cmb_xml.DataSource = DALUse.Query(sqlxml).Tables[0];
            //    cmb_xml.DisplayMember = "layout_name";
            //    cmb_xml.ValueMember = "layout_name";
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfd_save.ShowDialog(this) == DialogResult.OK)
                {
                    cf.WaitingThreadStart();
                    if (rb_excel.Checked)//excel导出
                    {
                        gridView1.ExportToXls(sfd_save.FileName + ".xls");
                    }
                    else if (rb_pdf.Checked)//pdf导出
                    {
                        MessageBox.Show("未完成");
                    }
                    else if (rb_xml.Checked)//xml导出
                    {
                        XmlDocument doc = new XmlDocument();
                        string sqlgetlayout = string.Format("select * from pt_xml_config where layout_name = '{0}'", cmb_xml.Text);
                        DataTable dtlayout = DALUse.Query(sqlgetlayout).Tables[0];
                        DataRow[] drroot = dtlayout.Select("parient_id is null");
                        XmlNode root = doc.CreateElement(drroot[0]["field_name"].ToString());
                        root.InnerText = "";
                        doc.AppendChild(root);

                        DataRow[] drsecondes = dtlayout.Select(string.Format("parient_id ='{0}'", drroot[0]["id"].ToString()));
                        foreach (DataRow drdata in source.Rows)
                        {
                            XmlElement xn = doc.CreateElement(drsecondes[0]["field_name"].ToString());
                            root.AppendChild(xn);
                            foreach (DataRow drfield in dtlayout.Rows)
                            {
                                if (source.Columns.Contains(drfield["field"].ToString()))
                                {
                                    XmlNode xnleaf = doc.CreateElement(drfield["field_name"].ToString());
                                    xnleaf.InnerText = drdata[drfield["field"].ToString()].ToString();
                                    xn.AppendChild(xnleaf);
                                }
                            }
                        }
                        CommonFunction.ConverDataSetToXMLFile(doc.InnerXml, sfd_save.FileName + ".xml");
                    }
                    cf.WaitingThreadStop();
                    uctlMessageBox.frmDisappearShow("数据导出成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void sfd_save_FileOk(object sender, CancelEventArgs e)
        {
            if (source.Rows.Count == 0)
            {
                MessageBox.Show("数据为空，无法导出！");
                return;
            }

            cf.WaitingThreadStart();
            CommonFunction.SaveAsExcel(sfd_save.FileName, source);
            cf.WaitingThreadStop();
            MessageBox.Show("导出成功！");
        }

        public void GetKindData()
        {
            //if (!openflag)
            //{
            //    return;
            //}
            //try
            //{
            //    string sql = string.Format("select ID, PARIENT_ID AS ParentID, FIELD_NAME AS NAME FROM PT_XML_CONFIG WHERE LAYOUT_NAME = '{0}'", cmb_xml.Text);
            //    tl_node.DataSource = DALUse.Query(sql).Tables[0];
            //    tl_node.ExpandAll();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        private void cmb_xml_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetKindData();

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            uctlAddSQLName.sql = rtb_sql.Text;
            uctlAddSQLName.ptid = cmb_pt.SelectedValue.ToString();
            Form f = new Form();
            f.Size = new Size(489, 60);
            f.FormBorderStyle = FormBorderStyle.None;
            uctlAddSQLName sn = new uctlAddSQLName();
            CommonFunction.AddForm(f, sn);
            //FreshSQL();
            CommonFunction.WriteErrorLog(this, "FreshSQL", null);
        }

        /// <summary>
        /// 刷新txt_sql gc_sql
        /// </summary>
        public void FreshSQL()
        {
            try
            {
                rtb_sql.Text = "";
                string sql = string.Format("select * from pt_sql where pt_id = {0}", ((DataRowView)cmb_pt.SelectedItem).Row["PT_ID"].ToString());
                gc_sql.DataSource = DALUse.Query(sql).Tables[0];
                if (gridView2.FocusedRowHandle < 0)
                {
                    return;
                }

                string sqlgettxt = string.Format("select * from pt_sql where id = {0}", gridView2.GetDataRow(gridView2.FocusedRowHandle)["ID"].ToString());
                rtb_sql.Text = DALUse.Query(sql).Tables[0].Rows[0]["SQL"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("delete from pt_sql where id = {0}", gridView2.GetDataRow(gridView2.FocusedRowHandle)["ID"].ToString());
                DALUse.ExecuteSql(sql);
                MessageBox.Show("删除成功！");
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
            FreshSQL();
        }

        private void cmb_pt_SelectedIndexChanged(object sender, EventArgs e)
        {
            FreshSQL();
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0)
            {
                return;
            }
            string sql = string.Format("select * from pt_sql where id = {0}", gridView2.GetDataRow(gridView2.FocusedRowHandle)["ID"].ToString());
            rtb_sql.Text = DALUse.Query(sql).Tables[0].Rows[0]["SQL"].ToString();
        }

        private void rb_xml_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_xml.Checked)
            {
                cmb_xml.Enabled = true;
                tl_node.Enabled = true;
            }
            else
            {
                tl_node.DataSource = null;
                cmb_xml.Enabled = false;
                tl_node.Enabled = false;

            }
        }


    }
}
