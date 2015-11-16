namespace DataExport
{
    partial class uctlDetailComparisonDict
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_target = new System.Windows.Forms.ComboBox();
            this.cmb_pt_name = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gc_local = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_itemname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_local = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rb_code = new System.Windows.Forms.RadioButton();
            this.rb_field = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rb_mohu = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rb_yange = new System.Windows.Forms.RadioButton();
            this.gc_comparison = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_cancel_compared = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_auto_compare = new System.Windows.Forms.Button();
            this.btn_dellocaldict = new System.Windows.Forms.Button();
            this.btn_deltargetdict = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_local)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_comparison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn14.AppearanceCell.Options.UseFont = true;
            this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn14.AppearanceHeader.Options.UseFont = true;
            this.gridColumn14.Caption = "NEWROWFLAG";
            this.gridColumn14.FieldName = "NEWROWFLAG";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = " 字典名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = " 平台名:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmb_target
            // 
            this.cmb_target.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_target.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_target.Location = new System.Drawing.Point(64, 37);
            this.cmb_target.Name = "cmb_target";
            this.cmb_target.Size = new System.Drawing.Size(310, 25);
            this.cmb_target.TabIndex = 0;
            this.cmb_target.SelectedIndexChanged += new System.EventHandler(this.cmb_target_SelectedIndexChanged);
            // 
            // cmb_pt_name
            // 
            this.cmb_pt_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pt_name.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_pt_name.Location = new System.Drawing.Point(64, 6);
            this.cmb_pt_name.Name = "cmb_pt_name";
            this.cmb_pt_name.Size = new System.Drawing.Size(416, 25);
            this.cmb_pt_name.TabIndex = 0;
            this.cmb_pt_name.SelectedIndexChanged += new System.EventHandler(this.cmb_pt_name_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gc_local);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_itemname);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(623, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 589);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对照字典";
            // 
            // gc_local
            // 
            this.gc_local.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gc_local.EmbeddedNavigator.Name = "";
            this.gc_local.Location = new System.Drawing.Point(3, 45);
            this.gc_local.MainView = this.gridView2;
            this.gc_local.Name = "gc_local";
            this.gc_local.Size = new System.Drawing.Size(374, 541);
            this.gc_local.TabIndex = 2;
            this.gc_local.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gc_local.DoubleClick += new System.EventHandler(this.gc_local_DoubleClick);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView2.GridControl = this.gc_local;
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
            this.gridView2.DoubleClick += new System.EventHandler(this.gridView2_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "项目名称";
            this.gridColumn1.FieldName = "FIELD";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "项目编码";
            this.gridColumn2.FieldName = "FIELD_CODE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(3, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = " 项目名:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_itemname
            // 
            this.txt_itemname.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_itemname.Location = new System.Drawing.Point(63, 16);
            this.txt_itemname.Name = "txt_itemname";
            this.txt_itemname.Size = new System.Drawing.Size(310, 23);
            this.txt_itemname.TabIndex = 3;
            this.txt_itemname.TextChanged += new System.EventHandler(this.txt_itemname_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(626, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = " 字典名:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmb_local
            // 
            this.cmb_local.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_local.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_local.FormattingEnabled = true;
            this.cmb_local.Location = new System.Drawing.Point(680, 37);
            this.cmb_local.Name = "cmb_local";
            this.cmb_local.Size = new System.Drawing.Size(310, 25);
            this.cmb_local.TabIndex = 0;
            this.cmb_local.SelectedIndexChanged += new System.EventHandler(this.cmb_local_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Controls.Add(this.gc_comparison);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(13, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(606, 588);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "对照明细";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(9, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rb_code);
            this.splitContainer1.Panel1.Controls.Add(this.rb_field);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rb_mohu);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.rb_yange);
            this.splitContainer1.Size = new System.Drawing.Size(594, 25);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.TabIndex = 5;
            // 
            // rb_code
            // 
            this.rb_code.AutoSize = true;
            this.rb_code.Location = new System.Drawing.Point(157, 1);
            this.rb_code.Name = "rb_code";
            this.rb_code.Size = new System.Drawing.Size(50, 21);
            this.rb_code.TabIndex = 8;
            this.rb_code.Text = "代码";
            this.rb_code.UseVisualStyleBackColor = true;
            // 
            // rb_field
            // 
            this.rb_field.AutoSize = true;
            this.rb_field.Checked = true;
            this.rb_field.Location = new System.Drawing.Point(61, 1);
            this.rb_field.Name = "rb_field";
            this.rb_field.Size = new System.Drawing.Size(50, 21);
            this.rb_field.TabIndex = 7;
            this.rb_field.TabStop = true;
            this.rb_field.Text = "字段";
            this.rb_field.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "对照分类:";
            // 
            // rb_mohu
            // 
            this.rb_mohu.AutoSize = true;
            this.rb_mohu.Location = new System.Drawing.Point(148, 1);
            this.rb_mohu.Name = "rb_mohu";
            this.rb_mohu.Size = new System.Drawing.Size(74, 21);
            this.rb_mohu.TabIndex = 3;
            this.rb_mohu.Text = "模糊对照";
            this.rb_mohu.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "对照等级:";
            // 
            // rb_yange
            // 
            this.rb_yange.AutoSize = true;
            this.rb_yange.Checked = true;
            this.rb_yange.Location = new System.Drawing.Point(68, 1);
            this.rb_yange.Name = "rb_yange";
            this.rb_yange.Size = new System.Drawing.Size(74, 21);
            this.rb_yange.TabIndex = 4;
            this.rb_yange.TabStop = true;
            this.rb_yange.Text = "严格对照";
            this.rb_yange.UseVisualStyleBackColor = true;
            // 
            // gc_comparison
            // 
            this.gc_comparison.EmbeddedNavigator.Name = "";
            this.gc_comparison.Location = new System.Drawing.Point(3, 46);
            this.gc_comparison.MainView = this.gridView3;
            this.gc_comparison.Name = "gc_comparison";
            this.gc_comparison.Size = new System.Drawing.Size(600, 542);
            this.gc_comparison.TabIndex = 2;
            this.gc_comparison.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn11,
            this.gridColumn14});
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Column = this.gridColumn14;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Value1 = "1";
            this.gridView3.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition2});
            this.gridView3.GridControl = this.gc_comparison;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsCustomization.AllowColumnMoving = false;
            this.gridView3.OptionsCustomization.AllowFilter = false;
            this.gridView3.OptionsCustomization.AllowGroup = false;
            this.gridView3.OptionsCustomization.AllowSort = false;
            this.gridView3.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView3.OptionsFilter.AllowFilterEditor = false;
            this.gridView3.OptionsFilter.AllowMRUFilterList = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.DoubleClick += new System.EventHandler(this.gridView3_DoubleClick);
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.Caption = "本地字段";
            this.gridColumn10.FieldName = "FIELD";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "本地代码";
            this.gridColumn7.FieldName = "FIELD_CODE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.Caption = "对照字段";
            this.gridColumn8.FieldName = "COMPARE_NAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "对照代码";
            this.gridColumn9.FieldName = "COMPARE_CODE";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.Caption = "TARGET_ID";
            this.gridColumn11.FieldName = "ID";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // btn_cancel_compared
            // 
            this.btn_cancel_compared.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel_compared.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_cancel_compared.Location = new System.Drawing.Point(911, 657);
            this.btn_cancel_compared.Name = "btn_cancel_compared";
            this.btn_cancel_compared.Size = new System.Drawing.Size(90, 28);
            this.btn_cancel_compared.TabIndex = 1;
            this.btn_cancel_compared.Text = "撤销";
            this.btn_cancel_compared.UseVisualStyleBackColor = true;
            this.btn_cancel_compared.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_cancel.Location = new System.Drawing.Point(815, 657);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(90, 28);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click_1);
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_save.Location = new System.Drawing.Point(719, 657);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(90, 28);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_auto_compare
            // 
            this.btn_auto_compare.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_auto_compare.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_auto_compare.Location = new System.Drawing.Point(623, 657);
            this.btn_auto_compare.Name = "btn_auto_compare";
            this.btn_auto_compare.Size = new System.Drawing.Size(90, 28);
            this.btn_auto_compare.TabIndex = 2;
            this.btn_auto_compare.Text = "自动对照";
            this.btn_auto_compare.UseVisualStyleBackColor = true;
            this.btn_auto_compare.Click += new System.EventHandler(this.btn_auto_compare_Click);
            // 
            // btn_dellocaldict
            // 
            this.btn_dellocaldict.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_dellocaldict.Location = new System.Drawing.Point(527, 35);
            this.btn_dellocaldict.Name = "btn_dellocaldict";
            this.btn_dellocaldict.Size = new System.Drawing.Size(90, 28);
            this.btn_dellocaldict.TabIndex = 7;
            this.btn_dellocaldict.Text = "清除对照字典";
            this.btn_dellocaldict.UseVisualStyleBackColor = true;
            this.btn_dellocaldict.Click += new System.EventHandler(this.btn_dellocaldict_Click);
            // 
            // btn_deltargetdict
            // 
            this.btn_deltargetdict.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_deltargetdict.Location = new System.Drawing.Point(435, 35);
            this.btn_deltargetdict.Name = "btn_deltargetdict";
            this.btn_deltargetdict.Size = new System.Drawing.Size(90, 28);
            this.btn_deltargetdict.TabIndex = 6;
            this.btn_deltargetdict.Text = "清除本地字典";
            this.btn_deltargetdict.UseVisualStyleBackColor = true;
            this.btn_deltargetdict.Click += new System.EventHandler(this.btn_deltargetdict_Click);
            // 
            // uctlDetailComparisonDict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_dellocaldict);
            this.Controls.Add(this.btn_deltargetdict);
            this.Controls.Add(this.btn_cancel_compared);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_auto_compare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmb_target);
            this.Controls.Add(this.cmb_local);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_pt_name);
            this.Name = "uctlDetailComparisonDict";
            this.Size = new System.Drawing.Size(1010, 692);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_local)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_comparison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gc_local;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gc_comparison;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel_compared;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_target;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_local;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private System.Windows.Forms.Button btn_auto_compare;
        private System.Windows.Forms.Button btn_cancel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private System.Windows.Forms.ComboBox cmb_pt_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_itemname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb_yange;
        private System.Windows.Forms.RadioButton rb_mohu;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rb_code;
        private System.Windows.Forms.RadioButton rb_field;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_dellocaldict;
        private System.Windows.Forms.Button btn_deltargetdict;

    }
}
