using DevExpress.XtraEditors;
using QLBV.Utilities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.Forms.ViewPDF
{
    public partial class FrmPDFViewer : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _fileName;

        public FrmPDFViewer()
        {
            InitializeComponent();
        }

        public FrmPDFViewer(string fileName) : this()
        {
            _fileName = fileName;
        }

        private void PDFViewer_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_fileName)
                && File.Exists(_fileName)
                && !_fileName.IsFileLocked())
            {
                pdfViewer1.LoadDocument(_fileName);
            }
        }
    }
}