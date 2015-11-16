namespace DataExport
{
    partial class uctlUploadDataToDatabase
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
            this.label2 = new System.Windows.Forms.Label();
            this.txt_connstr = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_sqlconn = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_databasetable = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_database = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_targetbale = new System.Windows.Forms.TextBox();
            this.mubiao = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gc_target_table = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_upload = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_target_table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_upload)).BeginInit();
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
            // 
            // cmb_pt
            // 
            this.cmb_pt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_pt.FormattingEnabled = true;
            this.cmb_pt.Location = new System.Drawing.Point(64, 6);
            this.cmb_pt.Name = "cmb_pt";
            this.cmb_pt.Size = new System.Drawing.Size(416, 25);
            this.cmb_pt.TabIndex = 1;
            this.cmb_pt.SelectedIndexChanged += new System.EventHandler(this.cmb_pt_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(171, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据库连接串:";
            // 
            // txt_connstr
            // 
            this.txt_connstr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_connstr.Location = new System.Drawing.Point(260, 37);
            this.txt_connstr.Name = "txt_connstr";
            this.txt_connstr.Size = new System.Drawing.Size(102, 23);
            this.txt_connstr.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_sqlconn);
            this.splitContainer1.Panel1.Controls.Add(this.btn_del);
            this.splitContainer1.Panel1.Controls.Add(this.btn_save);
            this.splitContainer1.Panel1.Controls.Add(this.txt_databasetable);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txt_database);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txt_targetbale);
            this.splitContainer1.Panel1.Controls.Add(this.mubiao);
            this.splitContainer1.Panel1.Controls.Add(this.txt_connstr);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cmb_pt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 710);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 5;
            // 
            // btn_sqlconn
            // 
            this.btn_sqlconn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sqlconn.Location = new System.Drawing.Point(704, 34);
            this.btn_sqlconn.Name = "btn_sqlconn";
            this.btn_sqlconn.Size = new System.Drawing.Size(90, 28);
            this.btn_sqlconn.TabIndex = 13;
            this.btn_sqlconn.Text = "保存连接串";
            this.btn_sqlconn.UseVisualStyleBackColor = true;
            this.btn_sqlconn.Click += new System.EventHandler(this.btn_sqlconn_Click);
            // 
            // btn_del
            // 
            this.btn_del.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_del.Location = new System.Drawing.Point(896, 34);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(90, 28);
            this.btn_del.TabIndex = 12;
            this.btn_del.Text = "删除对应关系";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(800, 34);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(90, 28);
            this.btn_save.TabIndex = 11;
            this.btn_save.Text = "保存表应关系";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_databasetable
            // 
            this.txt_databasetable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_databasetable.Location = new System.Drawing.Point(594, 37);
            this.txt_databasetable.Name = "txt_databasetable";
            this.txt_databasetable.Size = new System.Drawing.Size(102, 23);
            this.txt_databasetable.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(529, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "数据库表:";
            // 
            // txt_database
            // 
            this.txt_database.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_database.Location = new System.Drawing.Point(63, 37);
            this.txt_database.Name = "txt_database";
            this.txt_database.Size = new System.Drawing.Size(102, 23);
            this.txt_database.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(10, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "数据库:";
            // 
            // txt_targetbale
            // 
            this.txt_targetbale.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_targetbale.Location = new System.Drawing.Point(421, 37);
            this.txt_targetbale.Name = "txt_targetbale";
            this.txt_targetbale.Size = new System.Drawing.Size(102, 23);
            this.txt_targetbale.TabIndex = 6;
            // 
            // mubiao
            // 
            this.mubiao.AutoSize = true;
            this.mubiao.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mubiao.Location = new System.Drawing.Point(368, 40);
            this.mubiao.Name = "mubiao";
            this.mubiao.Size = new System.Drawing.Size(47, 17);
            this.mubiao.TabIndex = 5;
            this.mubiao.Text = "目标表:";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gc_target_table);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gc_upload);
            this.splitContainer2.Size = new System.Drawing.Size(1024, 639);
            this.splitContainer2.SplitterDistance = 347;
            this.splitContainer2.TabIndex = 1;
            // 
            // gc_target_table
            // 
            this.gc_target_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_target_table.EmbeddedNavigator.Name = "";
            this.gc_target_table.Location = new System.Drawing.Point(0, 0);
            this.gc_target_table.MainView = this.gridView1;
            this.gc_target_table.Name = "gc_target_table";
            this.gc_target_table.Size = new System.Drawing.Size(347, 639);
            this.gc_target_table.TabIndex = 0;
            this.gc_target_table.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridView1.GridControl = this.gc_target_table;
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
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "表名";
            this.gridColumn1.FieldName = "TABLE_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gc_upload
            // 
            this.gc_upload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_upload.EmbeddedNavigator.Name = "";
            this.gc_upload.Location = new System.Drawing.Point(0, 0);
            this.gc_upload.MainView = this.gridView2;
            this.gc_upload.Name = "gc_upload";
            this.gc_upload.Size = new System.Drawing.Size(673, 639);
            this.gc_upload.TabIndex = 0;
            this.gc_upload.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView2.GridControl = this.gc_upload;
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
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "目标表";
            this.gridColumn3.FieldName = "PT_TABLE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "数据库";
            this.gridColumn4.FieldName = "PT_DATA";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "数据库表";
            this.gridColumn5.FieldName = "PT_DATA_TABLE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // uctlUploadDataToDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "uctlUploadDataToDatabase";
            this.Size = new System.Drawing.Size(1024, 710);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_target_table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_upload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_pt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_connstr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txt_databasetable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_database;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_targetbale;
        private System.Windows.Forms.Label mubiao;
        private DevExpress.XtraGrid.GridControl gc_target_table;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraGrid.GridControl gc_upload;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_save;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Button btn_sqlconn;
    }
}
