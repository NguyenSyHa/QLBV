using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_80a_HD_1399_SP : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_80a_HD_1399_SP()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamsinhnam.DataBindings.Add("Text", DataSource, "NamSinh");
            colNamsinhnu.DataBindings.Add("Text", DataSource, "NamSinh");
            colMathe.DataBindings.Add("Text", DataSource, "SThe");
            colMacoso.DataBindings.Add("Text", DataSource, "MaCS");
            colMabenh.DataBindings.Add("Text", DataSource, "MaICD");
            colNgayvao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString=this.DangNgay.Value.ToString();
            colNgayra.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = this.DangNgay.Value.ToString();
            colTongngayDT.DataBindings.Add("Text", DataSource, "SoNgaydt");
            TongNgayRF.DataBindings.Add("Text", DataSource, "SoNgaydt");
            TongNgayGF2.DataBindings.Add("Text", DataSource, "SoNgaydt");
            TongNgayGF1.DataBindings.Add("Text", DataSource, "SoNgaydt");
            colTongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuong.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colCPVanChuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoai.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKTgf1.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraGF1.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongGF1.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colgf1_VTYT_tl.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF1.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiRF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTong_TienGiuong.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCRF1.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colMauRF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraRF1.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //colTenRF1.DataBindings.Add("Text", DataSource, "Ten").FormatString = DungChung.Bien.FormatString[1];
            colTong_VTYT_tl.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF11.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTong_CPVanChuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_RF1.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF1.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF1.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colNamnu.DataBindings.Add("Text", DataSource, "Nam").FormatString = DungChung.Bien.FormatString[1]; 
            colGF2CDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPNgoai.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPVanchuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKTgf2.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colGF2Mau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colGF2Nguoibenhcungchitra.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tiengiuong.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colGF2TongcongBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colGF2TTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colGF2Thuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colGF2_VTYT_tl.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colGF2VTYT.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colGF2xetnghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colthuocgf2.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoctlgf1.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh");
            txtMabn.DataBindings.Add("Text", DataSource, "MaBNhan");
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
        }
        string tongcong = " Tổng cộng ";
        int sttg2 = 1;

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        colGH2Ten.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        colGH2Muc.Text = "A";
                        tongcong += "A";
                        colGF2Ten.Text = " Cộng: A";
                        break;
                    case "2":
                        colGH2Ten.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        colGH2Muc.Text = "B";
                        tongcong += "+B";
                        colGF2Ten.Text = " Cộng: B";
                        break;
                    case "3":
                        colGH2Ten.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        colGH2Muc.Text = "C";
                        tongcong += "+C";
                        colGF2Ten.Text = " Cộng: C";
                        break;
                }

            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (sttg2 == 1)
                {
                    colmuc.Text = "I";
                }
                else
                {
                    if (sttg2 == 2)
                    {
                        colmuc.Text = "II";
                    }
                }
            }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (tuyen == 1)
                {
                    colTennhomGH1.Text = " Đúng tuyến";
                    colTennhomGF1.Text = " Cộng đúng tuyến";
                }
                if (tuyen == 2)
                {
                    colTennhomGH1.Text = " Trái tuyến";
                    colTennhomGF1.Text = " Cộng trái tuyến";
                }
            }
        }
        string rong = "";
        private void colNamsinhnam_BeforePrint(object sender, CancelEventArgs e)
        {
            int TT = Convert.ToInt32(this.GetCurrentColumnValue("GTinh"));
            if (TT == 1)
            {
                colNamsinhnu.Text = rong;
            }
            else
            {
                colNamsinhnam.Text = rong;
            }
        }

        private void xrLabel33_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(colTongcongBHYTRF11.Text))
            //{

            //    Double st = Convert.ToDouble(Sotien.Value.ToString());
            //    st = Math.Round(st, 0);
            //   // TT.Text = DungChung.Ham.DocTienBangChu(st, " đồng/");
            //}
        }
        int st = 1;

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtMaCQ.Text = DungChung.Bien.MaBV.ToUpper();
        }

    

    }
}
