namespace DataExport
{
    partial class uctlDataExchange
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rb_xml = new System.Windows.Forms.RadioButton();
            this.btn_del = new System.Windows.Forms.Button();
            this.rb_pdf = new System.Windows.Forms.RadioButton();
            this.rb_excel = new System.Windows.Forms.RadioButton();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_query = new System.Windows.Forms.Button();
            this.gc_sql = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmb_pt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_sql = new System.Windows.Forms.RichTextBox();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gc_querycontent = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tl_node = new DevExpress.XtraTreeList.TreeList();
            this.item = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmb_xml = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sfd_save = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_sql)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_querycontent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_node)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer7);
            this.splitContainer1.Size = new System.Drawing.Size(900, 650);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.19141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.68359F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.57422F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.55078F));
            this.tableLayoutPanel1.Controls.Add(this.rb_xml, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_del, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.rb_pdf, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.rb_excel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_export, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_query, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gc_sql, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmb_pt, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtb_sql, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 265);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // rb_xml
            // 
            this.rb_xml.AutoSize = true;
            this.rb_xml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_xml.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb_xml.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rb_xml.Location = new System.Drawing.Point(770, 90);
            this.rb_xml.Name = "rb_xml";
            this.rb_xml.Size = new System.Drawing.Size(126, 36);
            this.rb_xml.TabIndex = 3;
            this.rb_xml.TabStop = true;
            this.rb_xml.Text = "XML  文件";
            this.rb_xml.UseVisualStyleBackColor = true;
            this.rb_xml.CheckedChanged += new System.EventHandler(this.rb_xml_CheckedChanged);
            // 
            // btn_del
            // 
            this.btn_del.AutoSize = true;
            this.btn_del.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_del.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_del.Location = new System.Drawing.Point(648, 133);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(115, 36);
            this.btn_del.TabIndex = 0;
            this.btn_del.Text = "删除sql";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // rb_pdf
            // 
            this.rb_pdf.AutoSize = true;
            this.rb_pdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_pdf.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb_pdf.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rb_pdf.Location = new System.Drawing.Point(770, 47);
            this.rb_pdf.Name = "rb_pdf";
            this.rb_pdf.Size = new System.Drawing.Size(126, 36);
            this.rb_pdf.TabIndex = 1;
            this.rb_pdf.TabStop = true;
            this.rb_pdf.Text = "PDF   文件";
            this.rb_pdf.UseVisualStyleBackColor = true;
            this.rb_pdf.CheckedChanged += new System.EventHandler(this.rb_xml_CheckedChanged);
            // 
            // rb_excel
            // 
            this.rb_excel.AutoSize = true;
            this.rb_excel.Checked = true;
            this.rb_excel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_excel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb_excel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rb_excel.Location = new System.Drawing.Point(770, 4);
            this.rb_excel.Name = "rb_excel";
            this.rb_excel.Size = new System.Drawing.Size(126, 36);
            this.rb_excel.TabIndex = 0;
            this.rb_excel.TabStop = true;
            this.rb_excel.Text = "EXCEL文件";
            this.rb_excel.UseVisualStyleBackColor = true;
            this.rb_excel.CheckedChanged += new System.EventHandler(this.rb_xml_CheckedChanged);
            // 
            // btn_save
            // 
            this.btn_save.AutoSize = true;
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(648, 90);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(115, 36);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "保存sql";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_export
            // 
            this.btn_export.AutoSize = true;
            this.btn_export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_export.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_export.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_export.Location = new System.Drawing.Point(648, 47);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(115, 36);
            this.btn_export.TabIndex = 0;
            this.btn_export.Text = "导出数据";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_query
            // 
            this.btn_query.AutoSize = true;
            this.btn_query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_query.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_query.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_query.Location = new System.Drawing.Point(648, 4);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(115, 36);
            this.btn_query.TabIndex = 0;
            this.btn_query.Text = "执行SQL";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // gc_sql
            // 
            this.gc_sql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_sql.EmbeddedNavigator.Name = "";
            this.gc_sql.Location = new System.Drawing.Point(418, 90);
            this.gc_sql.MainView = this.gridView2;
            this.gc_sql.Name = "gc_sql";
            this.tableLayoutPanel1.SetRowSpan(this.gc_sql, 4);
            this.gc_sql.Size = new System.Drawing.Size(223, 171);
            this.gc_sql.TabIndex = 0;
            this.gc_sql.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.FocusedCell.BackColor = System.Drawing.Color.SteelBlue;
            this.gridView2.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.Red;
            this.gridView2.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3});
            this.gridView2.GridControl = this.gc_sql;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView2.OptionsFilter.AllowFilterEditor = false;
            this.gridView2.OptionsFilter.AllowMRUFilterList = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.Click += new System.EventHandler(this.gridView2_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "SQL名称";
            this.gridColumn1.FieldName = "SQL_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "id";
            this.gridColumn3.FieldName = "ID";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // cmb_pt
            // 
            this.cmb_pt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_pt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pt.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_pt.FormattingEnabled = true;
            this.cmb_pt.Location = new System.Drawing.Point(418, 47);
            this.cmb_pt.Name = "cmb_pt";
            this.cmb_pt.Size = new System.Drawing.Size(223, 29);
            this.cmb_pt.TabIndex = 0;
            this.cmb_pt.SelectedIndexChanged += new System.EventHandler(this.cmb_pt_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(418, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "目标平台名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtb_sql
            // 
            this.rtb_sql.BackColor = System.Drawing.SystemColors.Window;
            this.rtb_sql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_sql.ForeColor = System.Drawing.SystemColors.InfoText;
            this.rtb_sql.Location = new System.Drawing.Point(4, 4);
            this.rtb_sql.Name = "rtb_sql";
            this.tableLayoutPanel1.SetRowSpan(this.rtb_sql, 6);
            this.rtb_sql.Size = new System.Drawing.Size(407, 257);
            this.rtb_sql.TabIndex = 0;
            this.rtb_sql.Text = "";
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer7.Size = new System.Drawing.Size(900, 381);
            this.splitContainer7.SplitterDistance = 647;
            this.splitContainer7.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gc_querycontent);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(647, 381);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导出数据内容";
            // 
            // gc_querycontent
            // 
            this.gc_querycontent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_querycontent.EmbeddedNavigator.Name = "";
            this.gc_querycontent.Location = new System.Drawing.Point(3, 25);
            this.gc_querycontent.MainView = this.gridView1;
            this.gc_querycontent.Name = "gc_querycontent";
            this.gc_querycontent.Size = new System.Drawing.Size(641, 353);
            this.gc_querycontent.TabIndex = 0;
            this.gc_querycontent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gc_querycontent;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 381);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "格式选择";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tl_node, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmb_xml, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(243, 353);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tl_node
            // 
            this.tl_node.Appearance.Empty.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_node.Appearance.Empty.Options.UseFont = true;
            this.tl_node.Appearance.FocusedCell.BackColor = System.Drawing.Color.Red;
            this.tl_node.Appearance.FocusedCell.Options.UseBackColor = true;
            this.tl_node.Appearance.FocusedRow.BackColor = System.Drawing.Color.Red;
            this.tl_node.Appearance.FocusedRow.Options.UseBackColor = true;
            this.tl_node.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tl_node.Appearance.HeaderPanel.Options.UseFont = true;
            this.tl_node.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_node.Appearance.Row.Options.UseFont = true;
            this.tl_node.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tl_node.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.item,
            this.treeListColumn1});
            this.tl_node.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tl_node.Enabled = false;
            this.tl_node.Location = new System.Drawing.Point(4, 46);
            this.tl_node.Name = "tl_node";
            this.tl_node.OptionsBehavior.PopulateServiceColumns = true;
            this.tl_node.Size = new System.Drawing.Size(235, 372);
            this.tl_node.TabIndex = 0;
            // 
            // item
            // 
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
            this.treeListColumn1.Caption = "ID";
            this.treeListColumn1.FieldName = "ID";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // cmb_xml
            // 
            this.cmb_xml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_xml.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_xml.Enabled = false;
            this.cmb_xml.FormattingEnabled = true;
            this.cmb_xml.Location = new System.Drawing.Point(4, 25);
            this.cmb_xml.Name = "cmb_xml";
            this.cmb_xml.Size = new System.Drawing.Size(235, 29);
            this.cmb_xml.TabIndex = 4;
            this.cmb_xml.SelectedIndexChanged += new System.EventHandler(this.cmb_xml_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "XML文件格式：";
            // 
            // sfd_save
            // 
            this.sfd_save.Filter = "所有文件|*.*";
            // 
            // uctlDataExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "uctlDataExchange";
            this.Size = new System.Drawing.Size(900, 650);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_sql)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_querycontent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_node)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtb_sql;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.SaveFileDialog sfd_save;
        private DevExpress.XtraGrid.GridControl gc_querycontent;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.RadioButton rb_xml;
        private System.Windows.Forms.RadioButton rb_pdf;
        private System.Windows.Forms.RadioButton rb_excel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_pt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_xml;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraTreeList.TreeList tl_node;
        private DevExpress.XtraTreeList.Columns.TreeListColumn item;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraGrid.GridControl gc_sql;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_save;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
