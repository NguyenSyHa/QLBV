using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class progess : DevExpress.XtraEditors.XtraForm
    {
        public progess()
        {
            InitializeComponent();
        }
        static progess progress = null;
        private void progess_Load(object sender, EventArgs e)
        {

        }
        public static void DisplayProgress(IWin32Window owner, string title)
        {
            progress = new progess ();
            progress.Text = title;
            progress.marqueeProgressBarControl1 = new MarqueeProgressBarControl();
            progress.marqueeProgressBarControl1.Visible = true;
            progress.marqueeProgressBarControl1.Enabled = true;
            progress.marqueeProgressBarControl1.Properties.Stopped = false;
            progress.Show(owner);
        }
        public static DialogResult UpdateProgress(IWin32Window owner, string title)
        {
            if (progress != null)
            {

            }
            return progress.ShowDialog(owner);
        }

        public static void CloseProgress()
        {
            if (progress != null)
                progress.Close();
            progress = null;
        }
    }
}