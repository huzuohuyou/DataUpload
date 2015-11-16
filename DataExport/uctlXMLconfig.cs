using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using JHEMR.EmrSysDAL;

namespace DataExport
{
    public partial class uctlXMLconfig : UserControl
    {
        public TreeListNode selectedNodeParient = null;
        private List<TreeListNode> tlist = new List<TreeListNode>();
        public uctlXMLconfig()
        {
            InitializeComponent();
            initptdatasource();
            //InitLayOutData();
            //InitTreeListData();
            //Initgc_nodeData();
            //tl_node.ExpandAll();
        }

        public void initptdatasource()
        {
            try
            {
                cmb_pt.DataSource = DALUse.Query(PublicProperty.SqlGetPT).Tables[0];
                cmb_pt.DisplayMember = "pt_name";
                cmb_pt.ValueMember = "pt_id";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        /// <summary>
        /// 初始化gc_xmllayoutlist控件内容。
        /// </summary>
        public void InitLayOutData()
        {
            //try
            //{
            //    gc_xmllayoutlist.DataSource = DALUse.Query("SELECT DISTINCT LAYOUT_NAME FROM PT_XML_CONFIG").Tables[0];
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        }

        public void Initgc_nodeData()
        {
            try
            {
                if (gridView2.FocusedRowHandle < 0)
                {
                    return;
                }
                DataTable dtgc_node = DALUse.Query(string.Format("select * from pt_xml_config where layout_name = '{0}'", gridView2.GetDataRow(gridView2.FocusedRowHandle)["layout_name"].ToString())).Tables[0];
                gc_node.DataSource = dtgc_node;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void InitTreeListData()
        {
            try
            {
                if (gridView2.FocusedRowHandle < 0)
                {
                    return;
                }
                string sql = string.Format("select ID, PARIENT_ID AS ParentID, FIELD_NAME AS NAME ,MULTIPLE FROM PT_XML_CONFIG WHERE layout_name = '{0}'", gridView2.GetDataRow(gridView2.FocusedRowHandle)["layout_name"].ToString());
                DataSet ds = DALUse.Query(sql);
                if (ds.Tables.Count == 1)
                {
                    tl_node.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("没有设置平台格式！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btn_addroot_Click(object sender, EventArgs e)
        {
            //if (tl_node.Nodes.Count == 1)
            //{
            //    uctlRenameNode.id = gridView2.GetDataRow(gridView2.FocusedRowHandle)["ID"].ToString(); ;
            //    uctlAddNode.layout_name = gridView2.GetDataRow(gridView2.FocusedRowHandle)["layout_name"].ToString();
            //    Form f = new Form();
            //    f.FormBorderStyle = FormBorderStyle.None;
            //    f.Size = new Size(492, 73);
            //    uctlRenameNode an = new uctlRenameNode();
            //    CommonFunction.AddForm(f, an);
            //    InitTreeListData();
            //    tl_node.ExpandAll();
            //}

        }

        /// <summary>
        /// 添加根节点
        /// </summary>
        public void AddRootNode()
        {
            try
            {

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void btn_addleaf_Click(object sender, EventArgs e)
        {
            try
            {
                if (tl_node.FocusedNode == null)
                {
                    return;
                }
                uctlAddNode.layout_name = gridView2.GetDataRow(gridView2.FocusedRowHandle)["layout_name"].ToString();
                uctlAddNode.parientid = ((DataRowView)tl_node.GetDataRecordByNode(tl_node.FocusedNode)).Row["ID"].ToString();
                uctlAddNode.pt_id = cmb_pt.SelectedValue.ToString();
                Form f = new Form();
                f.FormBorderStyle = FormBorderStyle.None;
                f.Size = new Size(640, 475);
                uctlAddNode an = new uctlAddNode();
                CommonFunction.AddForm(f, an);

                InitTreeListData();
                Initgc_nodeData();
                tl_node.ExpandAll();
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
                if (gridView2.FocusedRowHandle<0)
                {
                    return;
                }
                DALUse.ExecuteSql(string.Format("delete pt_xml_config where layout_name = '{0}'", gridView2.GetDataRow(gridView2.FocusedRowHandle)["layout_name"].ToString()));
                //InitLayOutData();
                //Initgc_nodeData();
                //InitTreeListData();
                //tl_node.ExpandAll();
                getLayoutData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Form f = new Form();
            //    f.Size = new Size(492, 73);
            //    f.FormBorderStyle = FormBorderStyle.None;
            //    uctlRenameLayout rl = new uctlRenameLayout();
            //    uctlRenameLayout.layout_name = gridView2.GetDataRow(gridView2.FocusedRowHandle)["layout_name"].ToString();
            //    CommonFunction.AddForm(f, rl);
            //    InitLayOutData();
            //    Initgc_nodeData();
            //    tl_node.ExpandAll();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString());
            //}
        }

        public void getLayoutData()
        {
            try
            {
                gc_layout.DataSource = DALUse.Query(string.Format("select distinct LAYOUT_NAME from pt_xml_config where pt_id = '{0}'",cmb_pt.SelectedValue)).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btn_newlayout_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.FormBorderStyle = FormBorderStyle.None;
            uctlAddNewXMLLayout anxl = new uctlAddNewXMLLayout();
            if (cmb_pt.SelectedItem==null)
            {
                return;
            }
            anxl.pt_id = cmb_pt.SelectedValue.ToString();
            f.Size = new Size(528, 71);
            CommonFunction.AddForm(f, anxl);
            getLayoutData();

            //try
            //{
            //    string sql = "";
            //    //if (publicProperty.DATABASETYPE =="ORACLE")
            //    //{
            //    //Guid g = new Guid();
            //        sql = string.Format("insert into pt_xml_config(ID,FIELD_NAME,pt_id,field) values('{1}','根节点','{0}','MYROOT')", cmb_pt.SelectedValue.ToString(),Guid.NewGuid());
            //    //}
            //    //else if (publicProperty.DATABASETYPE=="SQLSERVER")
            //    //{
            //    //    sql = string.Format("insert into pt_xml_config(FIELD_NAME,pt_id) values('根节点',{0})", cmb_pt.SelectedValue.ToString());
            //    //}
            //    DALUse.ExecuteSql(sql);
            //    //this.FindForm().Close();
            //    uctlMessageBox.frmDisappearShow("添加成功！");
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString());
            //}
            //InitTreeListData();
            //Initgc_nodeData();
            //tl_node.ExpandAll();
        }

        private void btn_updatenodename_Click(object sender, EventArgs e)
        {
            if (tl_node.FocusedNode==null)
            {
                return;
            }
            uctlRenameNode.id = ((DataRowView)tl_node.GetDataRecordByNode(tl_node.FocusedNode)).Row["ID"].ToString();
            Form f = new Form();
            f.FormBorderStyle = FormBorderStyle.None;
            f.Size = new Size(492, 73);
            uctlRenameNode rn = new uctlRenameNode();
            CommonFunction.AddForm(f, rn);
            Initgc_nodeData();
            InitTreeListData();
            tl_node.ExpandAll();
        }

        /// <summary>
        /// 递归删除选定节点下的所有节点
        /// </summary>
        /// <param name="tln">焦点节点</param>
        private void RecursiveDel(TreeListNode tln)
        {
            //if (tln.HasChildren)
            //{
            //    foreach (TreeListNode tlnchild in tln.Nodes)
            //    {
            //        RecursiveDel((TreeListNode)tlnchild);
            //    }
            //}
            //else
            //{
            //    DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = {0}", ((DataRowView)tl_node.GetDataRecordByNode(tln)).Row["ID"].ToString()));
            //    //RecursiveDel(tln);
            //}

            //-----------
            //if (tln.Nodes.Count >= 0)
            //{
            //    if (tln.Nodes.Count == 0)
            //    {
            //        DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = {0}", ((DataRowView)tl_node.GetDataRecordByNode(tln)).Row["ID"].ToString()));
            //    }
            //    else
            //    {
            //        foreach (TreeListNode node in tln.Nodes)
            //        {
            //            DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = {0}", ((DataRowView)tl_node.GetDataRecordByNode(tln)).Row["ID"].ToString()));
            //            if (node.Nodes.Count > 0)
            //            {
            //                RecursiveDel(node);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = {0}", ((DataRowView)tl_node.GetDataRecordByNode(tln)).Row["ID"].ToString()));
            //}
            //-----------
            if (tl_node.FocusedNode==null)
            {
                return;
            }
            if (tln.Nodes.Count > 0)
            {
                foreach (TreeListNode node in tln.Nodes)
                {
                    //tlist.Add(node);
                    DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = '{0}'", ((DataRowView)tl_node.GetDataRecordByNode(node)).Row["ID"].ToString()));
                    if (node.Nodes.Count > 0)
                    {
                        RecursiveDel(node);
                    }
                }
            }
        }
        private void btn_delnode_Click(object sender, EventArgs e)
        {
            //if (tl_node.FocusedNode.HasChildren)
            //{
            //    MessageBox.Show("请先清除子节点！");
            //}
            //else
            //{
            //DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = {0}", ((DataRowView)tl_node.GetDataRecordByNode(tl_node.FocusedNode)).Row["ID"].ToString()));
            //}
            if (tl_node.FocusedNode==null)
            {
                return;
            }
            TreeListNode tln = tl_node.FocusedNode;
            RecursiveDel(tln);
            DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = '{0}'", ((DataRowView)tl_node.GetDataRecordByNode(tln)).Row["ID"].ToString()));
            foreach (TreeListNode deln in tlist)
            {
                DALUse.ExecuteSql(string.Format("delete pt_xml_config where id = '{0}'", ((DataRowView)tl_node.GetDataRecordByNode(deln)).Row["ID"].ToString()));
            }
            Initgc_nodeData();
            InitTreeListData();
            tl_node.ExpandAll();
        }

        private void gc_xmllayoutlist_Click(object sender, EventArgs e)
        {
            //if (gridView2.GetDataRow(gridView2.FocusedRowHandle) != null)
            //{
            //    InitTreeListData();
            //    Initgc_nodeData();
            //    tl_node.ExpandAll();
            //}
        }

        private void cmb_pt_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLayoutData();
            InitTreeListData();
            Initgc_nodeData();
            tl_node.ExpandAll();
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            InitTreeListData();
            Initgc_nodeData();
            //getLayoutData();
            tl_node.ExpandAll();
        }


    }
}
