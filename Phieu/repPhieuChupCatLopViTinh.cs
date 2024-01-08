using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;


namespace QLBV.Nam_0994
{
    public partial class repPhieuChupCatLopViTinh : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChupCatLopViTinh()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtTenBN.DataBindings.Add("Text", DataSource, "bn.TenBNhan");
            txt1.DataBindings.Add("Text", DataSource, "Tuoi");
            txtGT.DataBindings.Add("Text", DataSource, "GTinh");
            dChitxt.DataBindings.Add("Text", DataSource, "DChi");
        }

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24009")
            {
                this.txtMoTa.Font = new System.Drawing.Font("Times New Roman", 13F);
                this.txtKL.Font = new System.Drawing.Font("Times New Roman", 13F);
                this.colYCCC.Font = new System.Drawing.Font("Times New Roman", 13F);
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
        }

        private void xrRichText1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ketluan.Value != null)
            {
                XRRichText richText = sender as XRRichText;
                RichEditDocumentServer docServer = new RichEditDocumentServer();
                docServer.HtmlText = Ketluan.Value.ToString();
                docServer.Document.DefaultParagraphProperties.LineSpacingType = ParagraphLineSpacing.Multiple;
                docServer.Document.DefaultParagraphProperties.LineSpacingMultiplier = (float)6 / 5;
                docServer.Document.DefaultParagraphProperties.KeepLinesTogether = false;
                richText.Text = docServer.RtfText;
            }
        }

        private void xrRichText2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ketluan.Value != null)
            {
                XRRichText richText = sender as XRRichText;
                richText.Html = Ketluan.Value.ToString();
                richText.HeightF = 40;

            }
        }

        private void txtKL_BeforePrint(object sender, CancelEventArgs e)
        {
            if(Ketluan.Value != null)
            {
                XRRichText richText = sender as XRRichText;

                RichEditDocumentServer docServer = new RichEditDocumentServer();
                docServer.Text = Ketluan.Value.ToString();
                docServer.Document.DefaultParagraphProperties.LineSpacingType = ParagraphLineSpacing.Multiple;
                docServer.Document.DefaultParagraphProperties.LineSpacingMultiplier = (float) 6/5;
                richText.Text = docServer.RtfText;
            }
        }

        private void txtMoTa_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Mota.Value != null)
            {
                XRRichText richText = sender as XRRichText;

                RichEditDocumentServer docServer = new RichEditDocumentServer();
                docServer.Text = Mota.Value.ToString();
                docServer.Document.DefaultParagraphProperties.LineSpacingType = ParagraphLineSpacing.Multiple;
                docServer.Document.DefaultParagraphProperties.LineSpacingMultiplier = (float)6 / 5;
                richText.Text = docServer.RtfText;
            }
        }


    }
}
