namespace ToolFunction
{
    partial class uctlMessageBox
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
            this.components = new System.ComponentModel.Container();
            this.lab_mess = new System.Windows.Forms.Label();
            this.disappeartime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lab_mess
            // 
            this.lab_mess.BackColor = System.Drawing.Color.White;
            this.lab_mess.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_mess.ForeColor = System.Drawing.Color.Red;
            this.lab_mess.Location = new System.Drawing.Point(3, 4);
            this.lab_mess.Name = "lab_mess";
            this.lab_mess.Size = new System.Drawing.Size(337, 105);
            this.lab_mess.TabIndex = 0;
            this.lab_mess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // disappeartime
            // 
            this.disappeartime.Interval = 300;
            this.disappeartime.Tick += new System.EventHandler(this.disappeartime_Tick);
            // 
            // uctlMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.lab_mess);
            this.Name = "uctlMessageBox";
            this.Size = new System.Drawing.Size(344, 113);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lab_mess;
        private System.Windows.Forms.Timer disappeartime;

    }
}
