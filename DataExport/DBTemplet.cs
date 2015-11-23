using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using ToolFunction;
using System.Text.RegularExpressions;


namespace DataExport
{
    class DBTemplet : Form
    {
        #region 初始化
        private Button button2;
        private DataGridView dataGridView1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private TextBox textBox3;
        private Label label4;
        private Button button4;
        private Button button5;
        private Button button6;
        private ProgressBar progressBar1;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn MR_CLASS;
        private DataGridViewTextBoxColumn MR_CODE;
        private DataGridViewTextBoxColumn TOPIC;
        private SplitContainer splitContainer1;
        private TextBox textBox4;
        private DataGridViewTextBoxColumn NAME;
        private DataGridViewTextBoxColumn SQL;
        private TabPage tabPage3;
        private RichTextBox richTextBox1;
        private Button button7;
        private Button button1;

        public DBTemplet()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.MR_CLASS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MR_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOPIC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(439, 476);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "写回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(468, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 24);
            this.button2.TabIndex = 0;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NAME,
            this.SQL});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(548, 393);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "name";
            this.NAME.HeaderText = "NAME";
            this.NAME.Name = "NAME";
            this.NAME.Width = 200;
            // 
            // SQL
            // 
            this.SQL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SQL.DataPropertyName = "sql";
            this.SQL.HeaderText = "SQL";
            this.SQL.Name = "SQL";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(562, 470);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(554, 440);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DB";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(154, 209);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(246, 23);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox4);
            this.splitContainer1.Panel1.Controls.Add(this.button6);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button7);
            this.splitContainer1.Panel1.Controls.Add(this.button5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(548, 434);
            this.splitContainer1.SplitterDistance = 37;
            this.splitContainer1.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(5, 6);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(208, 23);
            this.textBox4.TabIndex = 1;
            this.textBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox4_KeyDown_1);
            this.textBox4.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox4_KeyUp);
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_KeyPress);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(229, 6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 24);
            this.button6.TabIndex = 0;
            this.button6.Text = "同步";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(310, 6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 24);
            this.button7.TabIndex = 0;
            this.button7.Text = "添加";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(391, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 24);
            this.button5.TabIndex = 0;
            this.button5.Text = "删除";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.radioButton2);
            this.tabPage2.Controls.Add(this.radioButton1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(554, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "FILE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MR_CLASS,
            this.MR_CODE,
            this.TOPIC});
            this.dataGridView2.Location = new System.Drawing.Point(116, 68);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 10;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(393, 274);
            this.dataGridView2.TabIndex = 11;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // MR_CLASS
            // 
            this.MR_CLASS.DataPropertyName = "MR_CLASS";
            this.MR_CLASS.HeaderText = "MR_CLASS";
            this.MR_CLASS.Name = "MR_CLASS";
            // 
            // MR_CODE
            // 
            this.MR_CODE.DataPropertyName = "MR_CODE";
            this.MR_CODE.HeaderText = "MR_CODE";
            this.MR_CODE.Name = "MR_CODE";
            // 
            // TOPIC
            // 
            this.TOPIC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TOPIC.DataPropertyName = "TOPIC";
            this.TOPIC.HeaderText = "TOPIC";
            this.TOPIC.Name = "TOPIC";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(434, 410);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 24);
            this.button4.TabIndex = 10;
            this.button4.Text = "确定";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(116, 380);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(393, 23);
            this.textBox3.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(116, 349);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(393, 23);
            this.textBox2.TabIndex = 8;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(116, 38);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(393, 23);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(32, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "RESULT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "ELEMENT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "CLASS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "FILE_NAME";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(272, 9);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 21);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "元素";
            this.radioButton2.Text = "元素";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(116, 7);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 21);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Tag = "层次号";
            this.radioButton1.Text = "层次号";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(554, 444);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TABLE";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(548, 434);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(39, 476);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 24);
            this.button3.TabIndex = 4;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DBTemplet
            // 
            this.ClientSize = new System.Drawing.Size(562, 508);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBTemplet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "章节选取";
            this.Load += new System.EventHandler(this.DBTemplet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public string m_strChapter = string.Empty;
        public string m_strDataDetail = string.Empty;
        //public string m_strDataDetail = string.Empty;
        public string m_strClass = string.Empty;
        public bool m_bSave = false;
        public DataTable m_dtSQL = new DataTable();
        public DataTable m_dtMrSet = new DataTable();
        DataTable _dtSQL = new DataTable();

        public DBTemplet(string p_strChapter)
        {
            InitializeComponent();
            m_strChapter = p_strChapter;
            InitData();
        }
        
        public DBTemplet(string p_strChapter,string p_strDataLDetail)
        {
            InitializeComponent();
            m_strChapter = p_strChapter;
            m_strDataDetail = p_strDataLDetail;
            InitData();
        }

        public void InitData()
        {
            m_dtSQL.Columns.Add("NAME");
            m_dtSQL.Columns.Add("SQL");
        }

        public string GetTableColumns()
        {
            List<string> _lField = ExportXml.GetFiledFromXml(m_strChapter);
            string _strField = "SELECT \nPATIENT_ID,\nVISIT_ID,";
            foreach (string var in _lField)
            {
                _strField += "\n" + var.Replace("[","").Replace("]","").Trim() + ",";
            }
            _strField = _strField.Trim(',');
            _strField += " \nFROM 表名 \nWHERE PATIENT_ID = '@PATIENT_ID' \nAND VISIT_ID = @VISIT_ID";
            return _strField;
        }

        public void SetSQL()
        {
            DataSet _dsXml = GetSQL();
            m_dtSQL = _dsXml.Tables[0];
            this.dataGridView1.DataSource = m_dtSQL.DefaultView;

        }

        public void SetMrSet()
        {
            GetMrDict();
            dataGridView2.DataSource = m_dtMrSet.DefaultView;
            //DataSet _dsXml = GetSQL();
            //m_dtSQL = _dsXml.Tables[0];
            //this.dataGridView1.DataSource = m_dtSQL.DefaultView;
        }

        public static DataSet GetSQL()
        {
            string _strIniPath = Application.StartupPath + "\\sqltemplet.xml";
            return CommonFunction.ConvertXMLFileToDataSet(_strIniPath);
        }

        public void GetMrDict()
        {

            string _strSQL = "select  distinct(mr_code),topic,mr_class from mr_templet_index";
            DataTable _dtMrTemplet = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            m_dtMrSet = _dtMrTemplet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_dtSQL.Rows.Clear();
            string _strIniPath = Application.StartupPath + "\\sqltemplet.xml";
            //foreach (DataGridViewRow var in dataGridView1.Rows)
            //{
            //    m_dtSQL.Rows.Add(var.Cells["NAME"].Value.ToString(), var.Cells["SQL"].Value.ToString());
            //}
            DataSet _ds = new DataSet();
            _ds.Tables.Add(m_dtSQL.Copy());
            CommonFunction.ConvertDataSetToXMLFile(_ds, _strIniPath);
        }

        /// <summary>
        /// 判断表中是否有此字段
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strFieldName"></param>
        /// <returns></returns>
        public bool IsTableField(string p_strTableName, string p_strFieldName)
        {
            string _strSQL = string.Format(@"select {0} from {1} where 1=0", p_strFieldName, p_strTableName);
            DataTable _dt = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            if (_dt == null)
            {
                return false;
            }
            return true;
        }

        public void WriteBack()
        {
            int _nTablIndex = tabControl1.SelectedIndex;
            switch (_nTablIndex)
            {
                case 0:
                    {
                        //string _strTableName = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
                        //string _strFieldName = m_strChapter.Replace("[", "").Replace("]", "");
                        //if (!IsTableField(_strTableName, _strFieldName))
                        //{
                        //    MessageBox.Show("表" + _strTableName + "不存在字段" + _strFieldName);
                        //    return;
                        //}
                        //m_strDataDetail = _strTableName + "|" + _strFieldName;
                        m_strDataDetail = dataGridView1.CurrentRow.Cells["sql"].Value.ToString();
                        m_strClass = "DB";
                    }
                    break;
                case 1:
                    {
                        if (textBox2.Text=="")
                        {
                            MessageBox.Show("元素名称为空");
                            return;
                        }
                        m_strDataDetail = textBox3.Text;
                        m_strClass = "FILE";
                    }
                    break;
                case 2:
                    {
                        m_strDataDetail = richTextBox1.Text;
                        if (!CheckSQL(m_strDataDetail))
                        {
                            return;
                        }
                        m_strClass = "TABLE";
                    }
                    break;
                case 4: { }
                    break;
                default:
                    break;

            }
            m_bSave = true;
            this.FindForm().Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            WriteBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Format(@"{0}|{1}|{2}", ((RadioButton)sender).Text, textBox1.Text, textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string _strClass = string.Empty;
            if (radioButton1.Checked)
            {
                _strClass = "层次号";
            }
            else
            {
                _strClass = "元素";
            }
            textBox3.Text = string.Format(@"{0}|{1}|{2}", _strClass, textBox1.Text, textBox2.Text);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (13 == e.KeyValue)
            {
                string _strClass = string.Empty;
                if (radioButton1.Checked)
                {
                    _strClass = "层次号";
                }
                else
                {
                    _strClass = "元素";
                }
                textBox1.Text = dataGridView2.CurrentRow.Cells["topic"].Value.ToString();
                textBox2.Focus();
                textBox3.Text = string.Format(@"{0}|{1}|{2}", _strClass, textBox1.Text, textBox2.Text);
                if (((TextBox)sender).Name == "textBox2")
                {
                    WriteBack();
                }
            }
            else
            {
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    dgvr.Selected = false;
                }
            }
        }

        private void DBTemplet_Load(object sender, EventArgs e)
        {
            SetSQL();
            SetMrSet();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            m_dtSQL.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            dataGridView1.DataSource = m_dtSQL.DefaultView;
            //dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SynValueDict();
        }

        /// <summary>
        /// 同步值字典
        /// </summary>
        public void SynValueDict()
        {
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            string _strSQL = "select * from pat_visit where 1=0";
            DataTable _dtPatVisit = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            progressBar1.Maximum = _dtPatVisit.Columns.Count;
            foreach (DataColumn _dc in _dtPatVisit.Columns)
            {
                progressBar1.Value++;
                string _strName = _dc.Caption;
                string _strValue = string.Format(@"{0}|{1}", "PAT_VISIT",_dc.Caption);
                DataRow[] _arrTemp = m_dtSQL.Select(string.Format("name = '{0}'", _strName));
                if (_arrTemp.Length > 0)
                {
                    foreach (DataRow _drTemp in _arrTemp)
                    {
                        m_dtSQL.Rows.Remove(_drTemp);
                    }
                }
                m_dtSQL.Rows.Add(_strName, _strValue.ToUpper());
            }
            m_dtSQL.Rows.Add("#PAT_VISIT", "SELECT * FROM PAT_VISIT WHERE PATIENT_ID = '@PATIENT_ID' AND VISIT_ID = @VISIT_ID");
            progressBar1.Value = 0;
            _strSQL = "select * from pat_master_index where 1=0";
            DataTable _dtMasterIndex = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            progressBar1.Maximum = _dtMasterIndex.Columns.Count;
            foreach (DataColumn _dc in _dtMasterIndex.Columns)
            {
                progressBar1.Value++;
                string _strName = _dc.Caption;
                string _strValue = string.Format(@"{0}|{1}","PAT_MASTER_INDEX", _dc.Caption);
                DataRow[] _arrTemp = m_dtSQL.Select(string.Format("name = '{0}'", _strName));
                if (_arrTemp.Length > 0)
                {
                    foreach (DataRow _drTemp in _arrTemp)
                    {
                        m_dtSQL.Rows.Remove(_drTemp);
                    }
                }
                m_dtSQL.Rows.Add(_strName, _strValue.ToUpper());
            }
            m_dtSQL.Rows.Add("#PAT_MASTER_INDEX", "SELECT * FROM PAT_MASTER_INDEX WHERE PATIENT_ID = '@PATIENT_ID'");
            //m_dtSQL = ((DataView)dataGridView1.DataSource).Table;
            //((DataView)dataGridView1.DataSource).Table.DefaultView.Sort = "NAME";
            dataGridView1.DataSource = m_dtSQL.DefaultView;
            progressBar1.Visible = false;
            //uctlMessageBox.frmDisappearShow("同步完成!");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells["TOPIC"].Value.ToString();
            string _strClass = string.Empty;
            if (radioButton1.Checked)
            {
                _strClass = "层次号";
            }
            else
            {
                _strClass = "元素";
            }
            textBox3.Text = string.Format(@"{0}|{1}|{2}", _strClass, textBox1.Text, textBox2.Text);
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            //foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            //{
            //    string _strName = dgvr.Cells["name"].Value.ToString();
            //    string _strRegx = textBox4.Text;
            //    if (Regex.IsMatch(_strName, string.Format(@"^{0}",_strRegx)))
            //    {
            //         dgvr.Selected = true;
            //    }
            //}
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox4_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                WriteBack();
            }
            else
            {
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    dgvr.Selected = false;
                }
            }

        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox4.Text == "")
            {
                SetSQL();
            }
            else
            {
                GetFitSql();
            }

        }

        /// <summary>
        /// 获取符合正则的sql并选中，其他移除。
        /// </summary>
        public void GetRegexSql()
        {
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if (dgvr.Cells["NAME"].Value == null)
                {
                    return;
                }
                string _strName = dgvr.Cells["NAME"].Value.ToString();
                string _strRegx = textBox4.Text;
                if (Regex.IsMatch(_strName, string.Format(@"^{0}", _strRegx)))
                {

                    dgvr.Selected = true;
                }
                else
                {
                    dataGridView1.Rows.Remove(dgvr);
                }
            }
        }

        public void GetFitSql()
        {
            DataRow[] _arrFit = this.m_dtSQL.Select(string.Format("name like '{0}%'", textBox4.Text));
            DataTable _dtFit = new DataTable();
            _dtFit.Columns.Add("NAME");
            _dtFit.Columns.Add("SQL");
            foreach (DataRow var in _arrFit)
            {
                _dtFit.Rows.Add(var["NAME"].ToString(), var["SQL"].ToString());
            }
            dataGridView1.DataSource = _dtFit.DefaultView;
        }

        public void GetFitMrSet()
        {
            DataRow[] _arrFit = this.m_dtMrSet.Select(string.Format("topic like '%{0}%'or mr_code like '{0}%'", textBox1.Text));
            DataTable _dtFit = new DataTable();
            _dtFit.Columns.Add("mr_class");
            _dtFit.Columns.Add("mr_code");
            _dtFit.Columns.Add("topic");
            foreach (DataRow var in _arrFit)
            {
                _dtFit.Rows.Add(var["mr_class"].ToString(), var["mr_code"].ToString(), var["topic"].ToString());
            }
            dataGridView2.DataSource = _dtFit.DefaultView;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //WriteBack();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                if (m_strDataDetail != "")
                {
                    richTextBox1.Text = m_strDataDetail;
                }
                else
                {
                    richTextBox1.Text = GetTableColumns();
                }
            }
        }

        public bool CheckSQL(string p_strSQL)
        {
            string _strSQL = p_strSQL.Replace("@PATIENT_ID", "1").Replace("@VISIT_ID", "1");
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            if (_dtTemp==null)
            {
                MessageBox.Show("SQL语句有误请检查");
                return false;
            }
            return true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
                SetMrSet();
            }
            else
            {
                GetFitMrSet();
            }
        }

        public void GetMrTempletName() { }

        private void button7_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Add();
            m_dtSQL.Rows.Add("", "");
            dataGridView1.DataSource = m_dtSQL.DefaultView;
        }
    }
}
