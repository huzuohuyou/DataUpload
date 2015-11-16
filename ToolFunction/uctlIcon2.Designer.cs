namespace ToolFunction
{
    partial class uctlIcon2
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
            this.peicon = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.peicon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // peicon
            // 
            this.peicon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peicon.Location = new System.Drawing.Point(0, 0);
            this.peicon.Name = "peicon";
            this.peicon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.peicon.Size = new System.Drawing.Size(106, 106);
            this.peicon.TabIndex = 0;
            this.peicon.MouseEnter += new System.EventHandler(this.pictureEdit1_MouseEnter);
            this.peicon.MouseLeave += new System.EventHandler(this.pictureEdit1_MouseLeave);
            this.peicon.Click += new System.EventHandler(this.peicon_Click);
            // 
            // uctlIcon2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.peicon);
            this.Name = "uctlIcon2";
            this.Size = new System.Drawing.Size(106, 106);
            ((System.ComponentModel.ISupportInitialize)(this.peicon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit peicon;
    }
}
