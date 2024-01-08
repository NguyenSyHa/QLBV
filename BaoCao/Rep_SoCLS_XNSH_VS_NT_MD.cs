using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoCLS_XNSH_VS_NT_MD : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoCLS_XNSH_VS_NT_MD()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            txt_MaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            txtGT.DataBindings.Add("Text", DataSource, "GTinh");
            txt_Tuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            txtBHYT.DataBindings.Add("Text", DataSource, "DTuong");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            txt_MaKP.DataBindings.Add("Text", DataSource, "MaKP");
            txt_MaCB.DataBindings.Add("Text", DataSource, "MaCB");
            txt_MaCBth.DataBindings.Add("Text", DataSource, "MaCBth");
            colGlucose.DataBindings.Add("Text", DataSource, "Glucose");
            colUrea.DataBindings.Add("Text", DataSource, "Urea");
            colCreatinin.DataBindings.Add("Text", DataSource, "Creatinin");
            colCholesterolTP.DataBindings.Add("Text", DataSource, "CholesterolTP");
            colTriglycorid.DataBindings.Add("Text", DataSource, "Triglycorid");
            colHDLC.DataBindings.Add("Text", DataSource, "HDLC");
            colLDLC.DataBindings.Add("Text", DataSource, "LDLC");
            colASTGOT.DataBindings.Add("Text", DataSource, "ASTGOT");
            colALTGPT.DataBindings.Add("Text", DataSource, "ALTGPT");
            colGGT.DataBindings.Add("Text", DataSource, "GGT");
            colAcidUric.DataBindings.Add("Text", DataSource, "AcidUric");
            colBilirubinTP.DataBindings.Add("Text", DataSource, "BilirubinTP");
            colBilirubinTT.DataBindings.Add("Text", DataSource, "BilirubinTT");
            colBilirubinGT.DataBindings.Add("Text", DataSource, "BilirubinGT");
            colProteinTP.DataBindings.Add("Text", DataSource, "ProteinTP");
            colAlbumin.DataBindings.Add("Text", DataSource, "Albumin");
            colAmylase.DataBindings.Add("Text", DataSource, "Amylase");
            colFe.DataBindings.Add("Text", DataSource, "Fe");
            colCalciTP.DataBindings.Add("Text", DataSource, "CalciTP");
            colCRPCReactiveProtein.DataBindings.Add("Text", DataSource, "CRPCReactiveProtein");
            colHbA1c.DataBindings.Add("Text", DataSource, "HbA1c");
            colNa.DataBindings.Add("Text", DataSource, "Na");
            colK.DataBindings.Add("Text", DataSource, "K");
            colCL.DataBindings.Add("Text", DataSource, "CL");
        }

        private void colTenBN_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txtGT.Text == "1")
            {
                colNam.Text = txt_Tuoi.Text;
            }
            else
            {
                colNam.Text = "";
            }
        }

        private void colNu_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txtGT.Text == "0")
            {
                colNu.Text = txt_Tuoi.Text;
            }
            else
            {
                colNu.Text = "";
            }
        }

        private void colBHYT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txtBHYT.Text == "BHYT")
            {
                colBHYT.Text = "X";
            }
            else
            {
                colBHYT.Text = "";
            }
        }

        private void colNoiGui_BeforePrint(object sender, CancelEventArgs e)
        {
            int _makp = Convert.ToInt32(txt_MaKP.Text);
            var khoaphong = data.KPhongs.Where(p => p.MaKP == _makp).ToList();
            if (khoaphong.Count() > 0)
            {
                colNoiGui.Text = khoaphong.First().TenKP;
            }
            else
            {
                colNoiGui.Text = "";
            }    
        }

        private void colYeuCau_BeforePrint(object sender, CancelEventArgs e)
        {
            var tencb = data.CanBoes.Where(p => p.MaCB == txt_MaCB.Text).ToList();
            if (tencb.Count() > 0)
            {
                colYeuCau.Text = tencb.First().TenCB;
            }
            else
            {
                colYeuCau.Text = "";
            }    
        }

        private void colNguoiDoc_BeforePrint(object sender, CancelEventArgs e)
        {
            var tencb = data.CanBoes.Where(p => p.MaCB == txt_MaCBth.Text).ToList();
            if (tencb.Count() > 0)
            {
                colNguoiDoc.Text = tencb.First().TenCB;
            }
            else
            {
                colNguoiDoc.Text = "";
            }
        }
    }
}
