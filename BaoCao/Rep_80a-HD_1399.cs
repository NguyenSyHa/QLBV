using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_80a_HD_1399 : DevExpress.XtraReports.UI.XtraReport
    {
        public bool HtTuyen = true;
        int _a = 0;
        public Rep_80a_HD_1399(int a)
        {
            InitializeComponent();
            _a = a;
        }
        public bool HtMaICD = false;
        public void BindingData(string kieuNT, int a)
        {
            colHoten.DataBindings.Add("Text", DataSource, "Ho_ten");
            colNamsinhnam.DataBindings.Add("Text", DataSource, "NSinh");
            colNamsinhnu.DataBindings.Add("Text", DataSource, "NSinh");
            colMathe.DataBindings.Add("Text", DataSource, "Ma_the");
            colMacoso.DataBindings.Add("Text", DataSource, "Ma_dkbd");
            colMabenh.DataBindings.Add("Text", DataSource, "Ma_benh");
            colNgayvao.DataBindings.Add("Text", DataSource, "Ngaykham").FormatString = kieuNT;
            colNgayra.DataBindings.Add("Text", DataSource, "Ngay_ra").FormatString = kieuNT;
            colTongngayDT.DataBindings.Add("Text", DataSource, "So_ngay_dtri");
            TongNgayRF.DataBindings.Add("Text", DataSource, "So_ngay_dtri");
            TongNgayGF2.DataBindings.Add("Text", DataSource, "So_ngay_dtri");
            TongNgayGF1.DataBindings.Add("Text", DataSource, "So_ngay_dtri");
            colTongcong.DataBindings.Add("Text", DataSource, "T_tongchi").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiem.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];
            col_tiengiuong.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];// tiền giường + công khám
            col_VanChuyen.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoai.DataBindings.Add("Text", DataSource, "T_ngoaids").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiGF1.DataBindings.Add("Text", DataSource, "T_ngoaids").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenGF1.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKTgf1.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraGF1.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongGF1.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];// tiền giường + công khám
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "T_tongchi").FormatString = DungChung.Bien.FormatString[1];
            col_VTYT_tl.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF1.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF1.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF1.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiRF1.DataBindings.Add("Text", DataSource, "T_ngoaids").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenRF1.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];// tiền giường + công khám
            colDVKTCRF1.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colMauRF1.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraRF1.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
            //colTenRF1.DataBindings.Add("Text", DataSource, "Ten").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongRF1.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF11.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF.DataBindings.Add("Text", DataSource, "T_tongchi").FormatString = DungChung.Bien.FormatString[1];
            colTongchiRF1.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF1.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF1.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF1.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF1.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF1.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            colNamnu.DataBindings.Add("Text", DataSource, "Gioi_tinh").FormatString = DungChung.Bien.FormatString[1];
            colGF2CDHA.DataBindings.Add("Text", DataSource, "T_cdha").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPNgoai.DataBindings.Add("Text", DataSource, "T_ngoaids").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPVanchuyen.DataBindings.Add("Text", DataSource, "T_vchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKTgf2.DataBindings.Add("Text", DataSource, "T_dvkt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colGF2Mau.DataBindings.Add("Text", DataSource, "T_mau").FormatString = DungChung.Bien.FormatString[1];
            colGF2Nguoibenhcungchitra.DataBindings.Add("Text", DataSource, "T_bntt").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tiengiuong.DataBindings.Add("Text", DataSource, "T_giuong").FormatString = DungChung.Bien.FormatString[1];// tiền giường + công khám
            colGF2Tongcong.DataBindings.Add("Text", DataSource, "T_tongchi").FormatString = DungChung.Bien.FormatString[1];
            colGF2TongcongBHYT.DataBindings.Add("Text", DataSource, "T_bhtt").FormatString = DungChung.Bien.FormatString[1];
            //colGF2Tongchi.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGF2TTPT.DataBindings.Add("Text", DataSource, "T_pttt").FormatString = DungChung.Bien.FormatString[1];
            colGF2Thuoc.DataBindings.Add("Text", DataSource, "T_thuoc").FormatString = DungChung.Bien.FormatString[1];
            colGF2ThuocKCTG.DataBindings.Add("Text", DataSource, "T_vtyt_tyle").FormatString = DungChung.Bien.FormatString[1];
            colGF2VTYT.DataBindings.Add("Text", DataSource, "T_vtyt").FormatString = DungChung.Bien.FormatString[1];
            colGF2xetnghiem.DataBindings.Add("Text", DataSource, "T_xn").FormatString = DungChung.Bien.FormatString[1];
            colthuocgf2.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];
            colThuoctlgf1.DataBindings.Add("Text", DataSource, "T_thuoc_tyle").FormatString = DungChung.Bien.FormatString[1];
            if (a != 1)
            {
                txtNoiTinh.DataBindings.Add("Text", DataSource, "NTinh");
                GroupHeader2.GroupFields.Add(new GroupField("NTinh"));
            }
            else
                GroupHeader2.Visible = false;
            if (HtMaICD)
            {
                colTennhomGH1.DataBindings.Add("Text", DataSource, "Chandoan");
                GroupHeader1.GroupFields.Add(new GroupField("Chandoan"));
            }
            else
            if (HtTuyen)
                GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
        }
        string tongcong = " Tổng cộng ";
        int sttg2 = 1;

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NTinh").ToString();
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
            if (HtMaICD == false)
            {
                if (this.GetCurrentColumnValue("Tuyen") != null)
                {
                    sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                    if (sttg2 == 1)
                    {
                        colmuc.Text = "I";
                    }
                    if (sttg2 == 2)
                    {
                        colmuc.Text = "II";
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
                    else if (tuyen == 2)
                    {
                        colTennhomGH1.Text = " Trái tuyến";
                        colTennhomGF1.Text = " Cộng trái tuyến";
                    }
                }
            }
            else
            {
                colTennhomGF1.Text = " Cộng nhóm";
            }
        }
        string rong = "";
        private bool gt = false;
        private void colNamsinhnam_BeforePrint(object sender, CancelEventArgs e)
        {
            gt = Convert.ToBoolean(this.GetCurrentColumnValue("Gioi_tinh"));
            if (gt)
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


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtMaCQ.Text = DungChung.Bien.MaBV.ToUpper();
            if (this.Dtuong.Value != null && this.Dtuong.Value.ToString() != "BHYT")
            {
                GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
            }

            GroupHeader1.Visible = HtTuyen;
            GroupFooter1.Visible = HtTuyen;
            //if(_a!=1)
            //{
            //    GroupHeader2.Visible = false;
            //}
        }
        int stt = 0;
        private void colSTT_BeforePrint(object sender, CancelEventArgs e)
        {
            stt++;
            if (HtMaICD)
                colSTT.Text = stt.ToString();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell115.Text = "Phụ trách BHYT";
                xrTableCell116.Text = "Kế toán BHYT";
                xrTableCell117.Text = "Bệnh Xá Trưởng";
            }
        }
    }
}
