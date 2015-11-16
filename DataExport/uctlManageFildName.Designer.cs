namespace ConfirmFileName
{
    partial class AddFildName
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
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.cmb_ptname = new System.Windows.Forms.ComboBox();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_pt_target = new DevExpress.XtraGrid.GridControl();
            this.gc_table_detail = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemRadioGroup2 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemRadioGroup3 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.btn_save = new System.Windows.Forms.Button();
            this.excel_import = new System.Windows.Forms.Button();
            this.btn_sel_file = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_sny = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_pt_target)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_table_detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup3)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "平台名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_update
            // 
            this.btn_update.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_update.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_update.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_update.Location = new System.Drawing.Point(90, 0);
            this.btn_update.Margin = new System.Windows.Forms.Padding(4);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(90, 32);
            this.btn_update.TabIndex = 12;
            this.btn_update.Text = "修改";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Visible = false;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_del
            // 
            this.btn_del.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_del.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_del.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_del.Location = new System.Drawing.Point(0, 0);
            this.btn_del.Margin = new System.Windows.Forms.Padding(4);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(90, 32);
            this.btn_del.TabIndex = 12;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // cmb_ptname
            // 
            this.cmb_ptname.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmb_ptname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ptname.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_ptname.FormattingEnabled = true;
            this.cmb_ptname.Location = new System.Drawing.Point(47, 0);
            this.cmb_ptname.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_ptname.Name = "cmb_ptname";
            this.cmb_ptname.Size = new System.Drawing.Size(416, 25);
            this.cmb_ptname.TabIndex = 17;
            this.cmb_ptname.SelectedIndexChanged += new System.EventHandler(this.cmb_ptname_SelectedIndexChanged);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9});
            this.gridView2.GridControl = this.gc_pt_target;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.Caption = "平台号";
            this.gridColumn1.FieldName = "PT_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Width = 82;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.Caption = "平台名";
            this.gridColumn2.FieldName = "PT_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Width = 90;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.Caption = "表名";
            this.gridColumn3.FieldName = "TABLE_NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Width = 132;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.Caption = "字段名";
            this.gridColumn4.FieldName = "FIELD_NAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 150;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.Caption = "字段";
            this.gridColumn5.FieldName = "FIELD";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 106;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.Caption = "类型";
            this.gridColumn6.FieldName = "FIELD_TYPE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 66;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.Caption = "COMPARE_ID";
            this.gridColumn9.FieldName = "COMPARE_ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gc_pt_target
            // 
            this.gc_pt_target.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_pt_target.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gc_pt_target.EmbeddedNavigator.Name = "";
            this.gc_pt_target.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gc_pt_target.Location = new System.Drawing.Point(0, 0);
            this.gc_pt_target.MainView = this.gridView2;
            this.gc_pt_target.Margin = new System.Windows.Forms.Padding(4);
            this.gc_pt_target.Name = "gc_pt_target";
            this.gc_pt_target.Size = new System.Drawing.Size(673, 623);
            this.gc_pt_target.TabIndex = 14;
            this.gc_pt_target.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gc_table_detail
            // 
            this.gc_table_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_table_detail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gc_table_detail.EmbeddedNavigator.Name = "";
            this.gc_table_detail.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gc_table_detail.Location = new System.Drawing.Point(0, 0);
            this.gc_table_detail.MainView = this.gridView1;
            this.gc_table_detail.Margin = new System.Windows.Forms.Padding(4);
            this.gc_table_detail.Name = "gc_table_detail";
            this.gc_table_detail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRadioGroup1,
            this.repositoryItemRadioGroup2,
            this.repositoryItemCheckEdit1,
            this.repositoryItemRadioGroup3});
            this.gc_table_detail.Size = new System.Drawing.Size(337, 659);
            this.gc_table_detail.TabIndex = 7;
            this.gc_table_detail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.ActiveFilterEnabled = false;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.GridControl = this.gc_table_detail;
            this.gridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridView1.GroupPanelText = "字段明细";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.Caption = "平台ID";
            this.gridColumn7.FieldName = "PT_ID";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.Width = 30;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.Caption = "表名";
            this.gridColumn8.FieldName = "TABLE_NAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            // 
            // repositoryItemRadioGroup2
            // 
            this.repositoryItemRadioGroup2.Name = "repositoryItemRadioGroup2";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemRadioGroup3
            // 
            this.repositoryItemRadioGroup3.Name = "repositoryItemRadioGroup3";
            // 
            // btn_save
            // 
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_save.Location = new System.Drawing.Point(450, 0);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(90, 32);
            this.btn_save.TabIndex = 20;
            this.btn_save.Text = "添加";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // excel_import
            // 
            this.excel_import.Dock = System.Windows.Forms.DockStyle.Left;
            this.excel_import.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.excel_import.Location = new System.Drawing.Point(180, 0);
            this.excel_import.Name = "excel_import";
            this.excel_import.Size = new System.Drawing.Size(90, 32);
            this.excel_import.TabIndex = 21;
            this.excel_import.Text = "导入数据";
            this.excel_import.UseVisualStyleBackColor = true;
            this.excel_import.Visible = false;
            this.excel_import.Click += new System.EventHandler(this.excel_import_Click);
            // 
            // btn_sel_file
            // 
            this.btn_sel_file.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sel_file.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sel_file.Location = new System.Drawing.Point(360, 0);
            this.btn_sel_file.Name = "btn_sel_file";
            this.btn_sel_file.Size = new System.Drawing.Size(90, 32);
            this.btn_sel_file.TabIndex = 22;
            this.btn_sel_file.Text = "选择文件";
            this.btn_sel_file.UseVisualStyleBackColor = true;
            this.btn_sel_file.Visible = false;
            this.btn_sel_file.Click += new System.EventHandler(this.btn_sel_file_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_sny
            // 
            this.btn_sny.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sny.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sny.Location = new System.Drawing.Point(270, 0);
            this.btn_sny.Name = "btn_sny";
            this.btn_sny.Size = new System.Drawing.Size(90, 32);
            this.btn_sny.TabIndex = 22;
            this.btn_sny.Text = "同步字段";
            this.btn_sny.UseVisualStyleBackColor = true;
            this.btn_sny.Click += new System.EventHandler(this.btn_sny_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmb_ptname);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 692);
            this.splitContainer1.SplitterDistance = 27;
            this.splitContainer1.TabIndex = 23;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gc_table_detail);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1018, 661);
            this.splitContainer2.SplitterDistance = 339;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.gc_pt_target);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btn_save);
            this.splitContainer3.Panel2.Controls.Add(this.btn_sel_file);
            this.splitContainer3.Panel2.Controls.Add(this.btn_sny);
            this.splitContainer3.Panel2.Controls.Add(this.excel_import);
            this.splitContainer3.Panel2.Controls.Add(this.btn_update);
            this.splitContainer3.Panel2.Controls.Add(this.btn_del);
            this.splitContainer3.Size = new System.Drawing.Size(673, 659);
            this.splitContainer3.SplitterDistance = 623;
            this.splitContainer3.TabIndex = 0;
            // 
            // AddFildName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddFildName";
            this.Size = new System.Drawing.Size(1018, 692);
            this.Load += new System.EventHandler(this.AddFildName_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_pt_target)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_table_detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup3)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.ComboBox cmb_ptname;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.GridControl gc_pt_target;
        private DevExpress.XtraGrid.GridControl gc_table_detail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup3;
        private System.Windows.Forms.Button btn_save;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private System.Windows.Forms.Button excel_import;
        private System.Windows.Forms.Button btn_sel_file;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_sny;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;

    }
}
