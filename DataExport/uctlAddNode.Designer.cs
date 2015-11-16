namespace DataExport
{
    partial class uctlAddNode
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
            this.txt_node_name = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckb_banding = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rb_no = new System.Windows.Forms.RadioButton();
            this.rb_yes = new System.Windows.Forms.RadioButton();
            this.lab_parientid = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gc_nodelist = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_nodelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(126, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "节点名：";
            // 
            // txt_node_name
            // 
            this.txt_node_name.Location = new System.Drawing.Point(202, 21);
            this.txt_node_name.Margin = new System.Windows.Forms.Padding(5);
            this.txt_node_name.Name = "txt_node_name";
            this.txt_node_name.Size = new System.Drawing.Size(270, 23);
            this.txt_node_name.TabIndex = 1;
            // 
            // btn_save
            // 
            this.btn_save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_save.Location = new System.Drawing.Point(482, 19);
            this.btn_save.Margin = new System.Windows.Forms.Padding(5);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(64, 26);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "确定";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ckb_banding);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rb_no);
            this.groupBox1.Controls.Add(this.rb_yes);
            this.groupBox1.Controls.Add(this.lab_parientid);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_node_name);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(636, 86);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "节点信息";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "是否绑定字段";
            // 
            // ckb_banding
            // 
            this.ckb_banding.AutoSize = true;
            this.ckb_banding.Checked = true;
            this.ckb_banding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_banding.Location = new System.Drawing.Point(401, 59);
            this.ckb_banding.Name = "ckb_banding";
            this.ckb_banding.Size = new System.Drawing.Size(75, 21);
            this.ckb_banding.TabIndex = 8;
            this.ckb_banding.Text = "绑定字段";
            this.ckb_banding.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "多重节点：";
            // 
            // rb_no
            // 
            this.rb_no.AutoSize = true;
            this.rb_no.Checked = true;
            this.rb_no.Location = new System.Drawing.Point(76, 58);
            this.rb_no.Name = "rb_no";
            this.rb_no.Size = new System.Drawing.Size(38, 21);
            this.rb_no.TabIndex = 6;
            this.rb_no.TabStop = true;
            this.rb_no.Text = "否";
            this.rb_no.UseVisualStyleBackColor = true;
            // 
            // rb_yes
            // 
            this.rb_yes.AutoSize = true;
            this.rb_yes.Location = new System.Drawing.Point(132, 58);
            this.rb_yes.Name = "rb_yes";
            this.rb_yes.Size = new System.Drawing.Size(38, 21);
            this.rb_yes.TabIndex = 5;
            this.rb_yes.Text = "是";
            this.rb_yes.UseVisualStyleBackColor = true;
            // 
            // lab_parientid
            // 
            this.lab_parientid.Location = new System.Drawing.Point(92, 26);
            this.lab_parientid.Name = "lab_parientid";
            this.lab_parientid.Size = new System.Drawing.Size(35, 21);
            this.lab_parientid.TabIndex = 4;
            this.lab_parientid.Text = "id";
            // 
            // btn_close
            // 
            this.btn_close.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_close.Location = new System.Drawing.Point(562, 19);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(64, 26);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(2, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "父节点ID：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(636, 471);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.gc_nodelist);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(636, 383);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "准确选择";
            // 
            // gc_nodelist
            // 
            this.gc_nodelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_nodelist.EmbeddedNavigator.Name = "";
            this.gc_nodelist.Location = new System.Drawing.Point(3, 19);
            this.gc_nodelist.MainView = this.gridView1;
            this.gc_nodelist.Name = "gc_nodelist";
            this.gc_nodelist.Size = new System.Drawing.Size(630, 361);
            this.gc_nodelist.TabIndex = 0;
            this.gc_nodelist.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gc_nodelist;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.BackColor = System.Drawing.Color.White;
            this.gridColumn1.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn1.Caption = "表名";
            this.gridColumn1.FieldName = "TABLE_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.BackColor = System.Drawing.Color.White;
            this.gridColumn2.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn2.Caption = "字段名";
            this.gridColumn2.FieldName = "FIELD_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.Red;
            this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn3.Caption = "字段";
            this.gridColumn3.FieldName = "FIELD";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "字段类型";
            this.gridColumn4.FieldName = "FIELD_TYPE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "对照名";
            this.gridColumn5.FieldName = "COMPARE_NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // uctlAddNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "uctlAddNode";
            this.Size = new System.Drawing.Size(640, 475);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_nodelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_node_name;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_parientid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gc_nodelist;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rb_no;
        private System.Windows.Forms.RadioButton rb_yes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckb_banding;
    }
}
