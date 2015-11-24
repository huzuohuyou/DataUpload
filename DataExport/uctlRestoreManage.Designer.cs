namespace DataExport
{
    partial class uctlRestoreManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.OBJECT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOG_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OBJECT_NAME,
            this.PATIENT_ID,
            this.VISIT_ID,
            this.LOG_DATE});
            this.dataGridView1.Location = new System.Drawing.Point(3, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(564, 873);
            this.dataGridView1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(573, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "导出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(573, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "全部导出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // OBJECT_NAME
            // 
            this.OBJECT_NAME.DataPropertyName = "OBJECT_NAME";
            this.OBJECT_NAME.HeaderText = "对象";
            this.OBJECT_NAME.Name = "OBJECT_NAME";
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.DataPropertyName = "PATIENT_ID";
            this.PATIENT_ID.HeaderText = "病人id";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.Width = 120;
            // 
            // VISIT_ID
            // 
            this.VISIT_ID.DataPropertyName = "VISIT_ID";
            this.VISIT_ID.HeaderText = "住院次";
            this.VISIT_ID.Name = "VISIT_ID";
            this.VISIT_ID.Width = 120;
            // 
            // LOG_DATE
            // 
            this.LOG_DATE.DataPropertyName = "LOG_DATE";
            this.LOG_DATE.HeaderText = "记录时间";
            this.LOG_DATE.Name = "LOG_DATE";
            this.LOG_DATE.Width = 200;
            // 
            // uctlRestoreManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "uctlRestoreManage";
            this.Size = new System.Drawing.Size(898, 881);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJECT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOG_DATE;
    }
}
