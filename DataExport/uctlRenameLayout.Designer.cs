namespace DataExport
{
    partial class uctlRenameLayout
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
            this.btn_close = new System.Windows.Forms.Button();
            this.txt_layoutname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.txt_layoutname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_ok);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新格式名";
            // 
            // btn_close
            // 
            this.btn_close.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_close.Location = new System.Drawing.Point(395, 23);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(86, 32);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // txt_layoutname
            // 
            this.txt_layoutname.Location = new System.Drawing.Point(98, 25);
            this.txt_layoutname.Name = "txt_layoutname";
            this.txt_layoutname.Size = new System.Drawing.Size(199, 29);
            this.txt_layoutname.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "格式名：";
            // 
            // btn_ok
            // 
            this.btn_ok.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_ok.Location = new System.Drawing.Point(305, 23);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(86, 32);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // uctlRenameLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Blue;
            this.Name = "uctlRenameLayout";
            this.Size = new System.Drawing.Size(492, 73);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_layoutname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_close;
    }
}
