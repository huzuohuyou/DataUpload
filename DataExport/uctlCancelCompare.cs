using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ConfirmFileName;

namespace DataExport
{
    public partial class uctlCancelCompare : UserControl
    {
        /// <summary>
        /// gridview1的数据源
        /// </summary>
        private DataTable source = null;
        public uctlCancelCompare()
        {
            InitializeComponent();
            source = InitData();
            gc_comparison.DataSource = source.DefaultView;
        }

        /// <summary>
        /// 初始化表数据
        /// </summary>
        /// <returns>返回一个datatable为数据源</returns>
        public DataTable InitData()
        {
            string sql = string.Format("select  distinct 0 CHK, t.* from pt_comparison t where pt_id ='{0}' and table_name = '{1}'", BandingSQL.pt_id, BandingSQL.table_name);
            return DALUse.Query(sql).Tables[0];
        }

        //private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        GridHitInfo info = this.gridView1.CalcHitInfo(e.Location);
        //        if (info != null && info.InRow && info.Column.Caption == "CHK")
        //        {
        //            object j = gridView1.GetRow(info.RowHandle);
        //            if (j != null)
        //            {
        //                DataRowView view = (DataRowView)j;
        //                DataRow dr = view.Row;
        //                if (dr["CHK"].ToString() == "0")
        //                {
        //                    dr["CHK"] = "1";
        //                }
        //                else
        //                {
        //                    dr["CHK"] = "0";
        //                }
        //            }
        //            gridView1.RefreshData();
        //        }
        //    }
        //    catch (Exception ex) {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.Name.ToString() == "gridColumn1" & e.IsGetData)
            {
                if (source != null && source.Rows.Count >0 && source.DefaultView[e.ListSourceRowIndex]["CHK"].ToString() == "1")
                {
                    e.Value = true;
                }
                else
                {
                    e.Value = false;
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(null, "确定删除所选项", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            try
            {
                
                foreach (DataRow dr in source.Rows)
                {
                    if (dr["CHK"].ToString()=="1")
                    {
                        string sql = "delete from pt_comparison where compare_id = '" + dr["compare_id"]+"'";
                        DALUse.ExecuteSql(sql);
                    }
                }
                MessageBox.Show("取消绑定成功！");
                source = InitData();
                gc_comparison.DataSource = source.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Parent.FindForm().Close();
        }

        private void gc_comparison_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle>=0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr["CHK"].ToString() == "0")
                {
                    dr["CHK"] = "1";
                }
                else
                {
                    dr["CHK"] = "0";
                }
            }
        }
    }
}
