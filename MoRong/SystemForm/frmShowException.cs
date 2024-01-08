using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.MoRong.SystemForm
{
    public partial class frmShowException : Form
    {
        Exception ex;
        string dbEntityExMessage;
        public frmShowException(Exception _ex)
        {
            InitializeComponent();
            this.ex = _ex;
        }
        public frmShowException(string _exMessage)
        {
            InitializeComponent();
            this.dbEntityExMessage = _exMessage;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowException_Load(object sender, EventArgs e)
        {
            if (ex != null)
            {
                int line = 0;
                line = DungChung.Ham.GetLineNumberError(ex);
                txtException.Text = string.Format("ErrorLine: {0}", line) + Environment.NewLine + ex.Message + (ex.InnerException != null ? (Environment.NewLine + "InnerException: " + ex.InnerException.Message) : "") + Environment.NewLine + ex.StackTrace;
                LibraryStore.WriteLog.Error(ex);
            }
            if (!string.IsNullOrWhiteSpace(this.dbEntityExMessage))
            {
                txtException.Text = this.dbEntityExMessage;
            }
        }
    }
}
