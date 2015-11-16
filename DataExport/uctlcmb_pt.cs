using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JHEMR.EmrSysDAL;

namespace DataExport
{
    public partial class uctlcmb_pt : UserControl
    {
        public uctlcmb_pt()
        {
            InitializeComponent();
            Initcmbdatasource();
        }
        public void Initcmbdatasource()
        {
            //cmb_pt.DataSource = DALUse.Query(publicProperty.sqlGetPT).Tables[0];
            //cmb_pt.DisplayMember = "pt_name";
            //cmb_pt.ValueMember = "pt_id";
        }
    }
}
