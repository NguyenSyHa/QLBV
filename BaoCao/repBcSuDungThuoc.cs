using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcSuDungThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcSuDungThuoc()
        {
            InitializeComponent();
        }
        string fm = DungChung.Bien.FormatString[1];
        public repBcSuDungThuoc(string maCQCQ)
        {
            if(maCQCQ == "12001" || DungChung.Bien.MaBV == "12001")
           // fm = "{0:0,0}";
            InitializeComponent();
        }
        public void BindingData()
        {
           
            colMa.DataBindings.Add("Text", DataSource, "MaDV");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colNoiTruSL.DataBindings.Add("Text", DataSource, "NoiTruSL");
            colNoiTruTGh1.DataBindings.Add("Text", DataSource, "NoiTruT").FormatString = fm;
            colNoiTruT.DataBindings.Add("Text", DataSource, "NoiTruT").FormatString = fm;
            colNoiTruTGf1.DataBindings.Add("Text", DataSource, "NoiTruT").FormatString =  fm;
           
            colNgoaiTruSL.DataBindings.Add("Text", DataSource, "NgoaiTruSL");
            colNgoaiTruTGh1.DataBindings.Add("Text", DataSource, "NgoaiTruT").FormatString = fm;
            colNgoaiTruT.DataBindings.Add("Text", DataSource, "NgoaiTruT").FormatString =  fm;
            colNgoaiTruTGf1.DataBindings.Add("Text", DataSource, "NgoaiTruT").FormatString =  fm;

            colKhacSL.DataBindings.Add("Text", DataSource, "KhacSL");
            colKhacTGh1.DataBindings.Add("Text", DataSource, "KhacT").FormatString = fm;
            colKhacT.DataBindings.Add("Text", DataSource, "KhacT").FormatString =  fm;
            colKhacTGf1.DataBindings.Add("Text", DataSource, "KhacT").FormatString =  fm;
            colHuySL.DataBindings.Add("Text", DataSource, "HuySL");
            colHuyTGh1.DataBindings.Add("Text", DataSource, "HuyT").FormatString = fm;
            colHuyT.DataBindings.Add("Text", DataSource, "HuyT").FormatString = fm;
            colHuyTGf1.DataBindings.Add("Text", DataSource, "HuyT").FormatString =  fm;
            colTongCongSL.DataBindings.Add("Text", DataSource, "TongCongSL");
            colTongCongTGh1.DataBindings.Add("Text", DataSource, "TongCongT").FormatString = fm;
            colTongCongT.DataBindings.Add("Text", DataSource, "TongCongT").FormatString = fm;
            colTongCongTGf1.DataBindings.Add("Text", DataSource, "TongCongT").FormatString =  fm;

            colNoiTruTGh1.Summary.FormatString = fm;              
            colNoiTruTGf1.Summary.FormatString =  fm;
            colNgoaiTruTGh1.Summary.FormatString = fm;               
            colNgoaiTruTGf1.Summary.FormatString =  fm;
            colKhacTGh1.Summary.FormatString = fm;               
            colKhacTGf1.Summary.FormatString =  fm;
            colHuyTGh1.Summary.FormatString = fm;                
            colHuyTGf1.Summary.FormatString =  fm;
            colTongCongTGh1.Summary.FormatString = fm;               
            colTongCongTGf1.Summary.FormatString =  fm;
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "14017")
            {
                colTenThuocGh1.DataBindings.Add("Text", DataSource, "Tentn");
                GroupHeader1.GroupFields.Add(new GroupField("Tentn"));
            }
            else
            {
                colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenNhomThuoc");
                GroupHeader1.GroupFields.Add(new GroupField("TenNhomThuoc"));
            }
           

        }

        private void repBcSuDungThuoc_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBC.Text = DungChung.Bien.NguoiLapBieu;
            colTruongPhongTCKT.Text = DungChung.Bien.KeToanTruong;
            colTruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        double _slsd = 0; double _ttsd = 0; double _slh = 0; double _tth = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
    }
}
