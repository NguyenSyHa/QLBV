using DevExpress.Pdf;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_ViewPDF : Form
    {
        string path;
        bool checkPrint = false;
        Action afterPrint;

        public frm_ViewPDF()
        {
            InitializeComponent();
        }

        public frm_ViewPDF(string _path, bool _checkPrint = false, Action _afterPrint = null)
        {
            InitializeComponent();
            this.path = _path;
            checkPrint = _checkPrint;
            afterPrint = _afterPrint;
        }

        private void frm_ViewPDF_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.path))
            {
                pdfViewer1.LoadDocument(this.path);
            }
        }

        private void bbiOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "PDF Files (*.pdf*)|*.pdf*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
                pdfViewer1.LoadDocument(choofdlog.FileName);
        }

        private void bbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pdfViewer1.IsDocumentOpened)
            {
                if (checkPrint && MessageBox.Show("Hóa đơn đã được in chuyển đổi. Bạn có muốn tiếp tục in?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                pdfViewer1.ShowPrintStatusDialog = false;
                PdfPrinterSettings pps = new PdfPrinterSettings();
                pdfViewer1.Print(pps);
                if (afterPrint != null)
                    afterPrint();
                //pdfViewer1.Print();
                this.Close();
            }
        }

        private void bbiExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void pdfViewer1_DocumentChanged(object sender, DevExpress.XtraPdfViewer.PdfDocumentChangedEventArgs e)
        {
            if (pdfViewer1.IsDocumentOpened)
            {
                this.Text = pdfViewer1.DocumentProperties.FileName;
            }
            else
            {
                this.Text = "PDF Viewer";
            }
        }

        private void CloseDocument()
        {
            if (pdfViewer1.IsDocumentOpened)
                pdfViewer1.CloseDocument();
        }

        private void frm_ViewPDF_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            CloseDocument();
        }
    }
}
