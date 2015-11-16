namespace ToolFunction
{
    partial class pe
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
            this.peicon.Location = new System.Drawing.Point(5, 5);
            this.peicon.Name = "peicon";
            this.peicon.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.peicon.Properties.Appearance.Options.UseBackColor = true;
            this.peicon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.peicon.Size = new System.Drawing.Size(88, 93);
            this.peicon.TabIndex = 0;
            this.peicon.MouseEnter += new System.EventHandler(this.peicon_MouseEnter);
            this.peicon.MouseLeave += new System.EventHandler(this.peicon_MouseLeave);
            // 
            // pe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.peicon);
            this.Name = "pe";
            this.Size = new System.Drawing.Size(97, 102);
            ((System.ComponentModel.ISupportInitialize)(this.peicon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit peicon;



    }
}
