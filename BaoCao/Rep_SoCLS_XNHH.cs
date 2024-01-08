using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoCLS_XNHH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoCLS_XNHH()
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
            colRBC.DataBindings.Add("Text", DataSource, "RBC");
            colHGB.DataBindings.Add("Text", DataSource, "HGB");
            colHCT.DataBindings.Add("Text", DataSource, "HCT");
            colMCV.DataBindings.Add("Text", DataSource, "MCV");
            colMCH.DataBindings.Add("Text", DataSource, "MCH");
            colMCHC.DataBindings.Add("Text", DataSource, "MCHC");
            colHCNhan.DataBindings.Add("Text", DataSource, "HCconhan");
            colLuoi.DataBindings.Add("Text", DataSource, "HCluoi");
            colRDWCV.DataBindings.Add("Text", DataSource, "RDWCV");
            colRDWSD.DataBindings.Add("Text", DataSource, "RDWSD");
            colTeBaoBatThuong.DataBindings.Add("Text", DataSource, "Tebaobatthuong");
            colWBC.DataBindings.Add("Text", DataSource, "WBC");
            colNEUT.DataBindings.Add("Text", DataSource, "NEUT");
            colNEUTP.DataBindings.Add("Text", DataSource, "NEUTP");
            colLYMP.DataBindings.Add("Text", DataSource, "LYMP");
            colLYMPP.DataBindings.Add("Text", DataSource, "LYMPP");
            colMONO.DataBindings.Add("Text", DataSource, "MONO");
            colMONOP.DataBindings.Add("Text", DataSource, "MONOP");
            colEO.DataBindings.Add("Text", DataSource, "EO");
            colEOP.DataBindings.Add("Text", DataSource, "EOP");
            colBSAO.DataBindings.Add("Text", DataSource, "BSAO");
            colBSAOP.DataBindings.Add("Text", DataSource, "BSAOP");
            colPLT.DataBindings.Add("Text", DataSource, "PLT");
            colPDW.DataBindings.Add("Text", DataSource, "PDW");
            colMPV.DataBindings.Add("Text", DataSource, "MPV");
            colPCT.DataBindings.Add("Text", DataSource, "PCT");
            colMauLang1h.DataBindings.Add("Text", DataSource, "Maulang1h");
            colMauLang2h.DataBindings.Add("Text", DataSource, "Maulang2h");
            colThoiGianMauChay.DataBindings.Add("Text", DataSource, "Thoigianmauchay");
            colThoiGianMauDong.DataBindings.Add("Text", DataSource, "Thoigianmaudong");
            colNhomMauABO.DataBindings.Add("Text", DataSource, "NhommauABO");
            colNhomMauRH.DataBindings.Add("Text", DataSource, "NhommauRH");
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

        private void colTenBN_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
