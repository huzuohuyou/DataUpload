namespace DataExport
{
    partial class uctlAddTempField
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_hong = new System.Windows.Forms.RadioButton();
            this.rb_cengci = new System.Windows.Forms.RadioButton();
            this.rb_element = new System.Windows.Forms.RadioButton();
            this.txt_cengci_code = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_field = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_close = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cengci_code.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_field.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.rb_hong);
            this.groupBox1.Controls.Add(this.rb_cengci);
            this.groupBox1.Controls.Add(this.rb_element);
            this.groupBox1.Controls.Add(this.txt_cengci_code);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txt_field);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加模板";
            // 
            // rb_hong
            // 
            this.rb_hong.AutoSize = true;
            this.rb_hong.Location = new System.Drawing.Point(160, 28);
            this.rb_hong.Name = "rb_hong";
            this.rb_hong.Size = new System.Drawing.Size(38, 21);
            this.rb_hong.TabIndex = 6;
            this.rb_hong.Text = "宏";
            this.rb_hong.UseVisualStyleBackColor = true;
            this.rb_hong.Click += new System.EventHandler(this.rb_hong_Click);
            // 
            // rb_cengci
            // 
            this.rb_cengci.AutoSize = true;
            this.rb_cengci.Location = new System.Drawing.Point(286, 28);
            this.rb_cengci.Name = "rb_cengci";
            this.rb_cengci.Size = new System.Drawing.Size(62, 21);
            this.rb_cengci.TabIndex = 5;
            this.rb_cengci.Text = "层次号";
            this.rb_cengci.UseVisualStyleBackColor = true;
            this.rb_cengci.Click += new System.EventHandler(this.rb_cengci_Click);
            // 
            // rb_element
            // 
            this.rb_element.AutoSize = true;
            this.rb_element.Checked = true;
            this.rb_element.Location = new System.Drawing.Point(18, 28);
            this.rb_element.Name = "rb_element";
            this.rb_element.Size = new System.Drawing.Size(50, 21);
            this.rb_element.TabIndex = 4;
            this.rb_element.TabStop = true;
            this.rb_element.Text = "元素";
            this.rb_element.UseVisualStyleBackColor = true;
            this.rb_element.Click += new System.EventHandler(this.rb_element_Click);
            // 
            // txt_cengci_code
            // 
            this.txt_cengci_code.Enabled = false;
            this.txt_cengci_code.Location = new System.Drawing.Point(106, 99);
            this.txt_cengci_code.Name = "txt_cengci_code";
            this.txt_cengci_code.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_cengci_code.Properties.Appearance.Options.UseFont = true;
            this.txt_cengci_code.Size = new System.Drawing.Size(234, 23);
            this.txt_cengci_code.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(16, 101);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 17);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "层次号：";
            // 
            // txt_field
            // 
            this.txt_field.Location = new System.Drawing.Point(106, 65);
            this.txt_field.Name = "txt_field";
            this.txt_field.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_field.Properties.Appearance.Options.UseFont = true;
            this.txt_field.Size = new System.Drawing.Size(234, 23);
            this.txt_field.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(16, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 17);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "名称：";
            // 
            // btn_close
            // 
            this.btn_close.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_close.Appearance.Options.UseFont = true;
            this.btn_close.Location = new System.Drawing.Point(351, 97);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 26);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Appearance.Options.UseFont = true;
            this.btn_save.Location = new System.Drawing.Point(351, 64);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 26);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "保存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // uctlAddTempField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.Controls.Add(this.groupBox1);
            this.Name = "uctlAddTempField";
            this.Size = new System.Drawing.Size(440, 147);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cengci_code.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_field.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txt_field;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_close;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private System.Windows.Forms.RadioButton rb_hong;
        private System.Windows.Forms.RadioButton rb_cengci;
        private System.Windows.Forms.RadioButton rb_element;
        private DevExpress.XtraEditors.TextEdit txt_cengci_code;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
