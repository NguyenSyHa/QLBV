using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace QLBV.ChucNang
{
    public partial class RibbonDT : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonDT()
        {
            InitializeComponent();
        }

        private void RibbonDT_Load(object sender, EventArgs e)
        {
            this.Hide();
            Point pt = this.Location;
            pt.Offset(this.Width / 2, this.Height / 2);
            radialMenuDT.ShowPopup(pt);
        }

        private void radialMenuDT_CloseUp(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}