namespace DataExport
{
    partial class uctlXMLconfig
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_pt = new System.Windows.Forms.ComboBox();
            this.tl_node = new DevExpress.XtraTreeList.TreeList();
            this.item = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btn_addleaf = new System.Windows.Forms.Button();
            this.btn_updatenodename = new System.Windows.Forms.Button();
            this.btn_delnode = new System.Windows.Forms.Button();
            this.gc_node = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_newlayout = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.gc_layout = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tl_node)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_node)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_layout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "平台名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_pt
            // 
            this.cmb_pt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_pt.FormattingEnabled = true;
            this.cmb_pt.Location = new System.Drawing.Point(64, 6);
            this.cmb_pt.Name = "cmb_pt";
            this.cmb_pt.Size = new System.Drawing.Size(416, 25);
            this.cmb_pt.TabIndex = 0;
            this.cmb_pt.SelectedIndexChanged += new System.EventHandler(this.cmb_pt_SelectedIndexChanged);
            // 
            // tl_node
            // 
            this.tl_node.Appearance.Empty.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tl_node.Appearance.Empty.Options.UseFont = true;
            this.tl_node.Appearance.FocusedCell.BackColor = System.Drawing.Color.AliceBlue;
            this.tl_node.Appearance.FocusedCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tl_node.Appearance.FocusedCell.Options.UseBackColor = true;
            this.tl_node.Appearance.FocusedCell.Options.UseFont = true;
            this.tl_node.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tl_node.Appearance.HeaderPanel.Options.UseFont = true;
            this.tl_node.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tl_node.Appearance.Row.Options.UseFont = true;
            this.tl_node.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tl_node.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.item,
            this.treeListColumn1,
            this.treeListColumn2});
            this.tl_node.Location = new System.Drawing.Point(8, 34);
            this.tl_node.Name = "tl_node";
            this.tl_node.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.tl_node.Size = new System.Drawing.Size(345, 616);
            this.tl_node.TabIndex = 1;
            // 
            // item
            // 
            this.item.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.item.AppearanceCell.Options.UseFont = true;
            this.item.AppearanceCell.Options.UseImage = true;
            this.item.AppearanceCell.Options.UseTextOptions = true;
            this.item.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.item.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.item.AppearanceHeader.Options.UseFont = true;
            this.item.Caption = "名称";
            this.item.FieldName = "NAME";
            this.item.Name = "item";
            this.item.OptionsColumn.AllowEdit = false;
            this.item.OptionsColumn.AllowMove = false;
            this.item.OptionsColumn.AllowSort = false;
            this.item.OptionsColumn.ReadOnly = true;
            this.item.Visible = true;
            this.item.VisibleIndex = 0;
            this.item.Width = 169;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "ID";
            this.treeListColumn1.FieldName = "ID";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.Caption = "属性";
            this.treeListColumn2.FieldName = "MULTIPLE";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.OptionsColumn.AllowMove = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // btn_addleaf
            // 
            this.btn_addleaf.BackColor = System.Drawing.SystemColors.Control;
            this.btn_addleaf.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addleaf.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_addleaf.Location = new System.Drawing.Point(8, 656);
            this.btn_addleaf.Name = "btn_addleaf";
            this.btn_addleaf.Size = new System.Drawing.Size(90, 28);
            this.btn_addleaf.TabIndex = 1;
            this.btn_addleaf.Text = "添加";
            this.btn_addleaf.UseVisualStyleBackColor = false;
            this.btn_addleaf.Click += new System.EventHandler(this.btn_addleaf_Click);
            // 
            // btn_updatenodename
            // 
            this.btn_updatenodename.BackColor = System.Drawing.SystemColors.Control;
            this.btn_updatenodename.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_updatenodename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_updatenodename.Location = new System.Drawing.Point(133, 656);
            this.btn_updatenodename.Name = "btn_updatenodename";
            this.btn_updatenodename.Size = new System.Drawing.Size(90, 28);
            this.btn_updatenodename.TabIndex = 5;
            this.btn_updatenodename.Text = "修改";
            this.btn_updatenodename.UseVisualStyleBackColor = false;
            this.btn_updatenodename.Click += new System.EventHandler(this.btn_updatenodename_Click);
            // 
            // btn_delnode
            // 
            this.btn_delnode.BackColor = System.Drawing.SystemColors.Control;
            this.btn_delnode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delnode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_delnode.Location = new System.Drawing.Point(264, 656);
            this.btn_delnode.Name = "btn_delnode";
            this.btn_delnode.Size = new System.Drawing.Size(90, 28);
            this.btn_delnode.TabIndex = 6;
            this.btn_delnode.Text = "删除";
            this.btn_delnode.UseVisualStyleBackColor = false;
            this.btn_delnode.Click += new System.EventHandler(this.btn_delnode_Click);
            // 
            // gc_node
            // 
            this.gc_node.EmbeddedNavigator.Name = "";
            this.gc_node.Location = new System.Drawing.Point(358, 34);
            this.gc_node.MainView = this.gridView1;
            this.gc_node.Name = "gc_node";
            this.gc_node.Size = new System.Drawing.Size(336, 616);
            this.gc_node.TabIndex = 0;
            this.gc_node.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn1});
            this.gridView1.GridControl = this.gc_node;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ID";
            this.gridColumn3.FieldName = "ID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "父节点ID";
            this.gridColumn4.FieldName = "PARIENT_ID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "节点名";
            this.gridColumn5.FieldName = "FIELD_NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "字段";
            this.gridColumn1.FieldName = "FIELD";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // btn_newlayout
            // 
            this.btn_newlayout.BackColor = System.Drawing.SystemColors.Control;
            this.btn_newlayout.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newlayout.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_newlayout.Location = new System.Drawing.Point(808, 656);
            this.btn_newlayout.Name = "btn_newlayout";
            this.btn_newlayout.Size = new System.Drawing.Size(90, 28);
            this.btn_newlayout.TabIndex = 2;
            this.btn_newlayout.Text = "新建导出格式";
            this.btn_newlayout.UseVisualStyleBackColor = false;
            this.btn_newlayout.Click += new System.EventHandler(this.btn_newlayout_Click);
            // 
            // btn_del
            // 
            this.btn_del.BackColor = System.Drawing.SystemColors.Control;
            this.btn_del.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_del.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_del.Location = new System.Drawing.Point(912, 656);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(90, 28);
            this.btn_del.TabIndex = 3;
            this.btn_del.Text = "删除格式";
            this.btn_del.UseVisualStyleBackColor = false;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // gc_layout
            // 
            this.gc_layout.EmbeddedNavigator.Name = "";
            this.gc_layout.Location = new System.Drawing.Point(699, 34);
            this.gc_layout.MainView = this.gridView2;
            this.gc_layout.Name = "gc_layout";
            this.gc_layout.Size = new System.Drawing.Size(303, 616);
            this.gc_layout.TabIndex = 7;
            this.gc_layout.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gridView2.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2});
            this.gridView2.GridControl = this.gc_layout;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowColumnResizing = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.Click += new System.EventHandler(this.gridView2_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "XML格式列表";
            this.gridColumn2.FieldName = "LAYOUT_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // uctlXMLconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_layout);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_newlayout);
            this.Controls.Add(this.btn_delnode);
            this.Controls.Add(this.btn_addleaf);
            this.Controls.Add(this.btn_updatenodename);
            this.Controls.Add(this.gc_node);
            this.Controls.Add(this.tl_node);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_pt);
            this.Name = "uctlXMLconfig";
            this.Size = new System.Drawing.Size(1006, 700);
            ((System.ComponentModel.ISupportInitialize)(this.tl_node)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_node)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_layout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_node;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btn_addleaf;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_newlayout;
        private DevExpress.XtraTreeList.TreeList tl_node;
        private DevExpress.XtraTreeList.Columns.TreeListColumn item;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.Button btn_delnode;
        private System.Windows.Forms.Button btn_updatenodename;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_pt;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.GridControl gc_layout;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}
