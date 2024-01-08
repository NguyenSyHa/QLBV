using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_80a_HD_1399_BHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_80a_HD_1399_BHYT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamsinhnam.DataBindings.Add("Text", DataSource, "NSinh");
            colNamsinhnu.DataBindings.Add("Text", DataSource, "NSinh");
            colMathe.DataBindings.Add("Text", DataSource, "SThe");
            colMacoso.DataBindings.Add("Text", DataSource, "MaCS");
            colMabenh.DataBindings.Add("Text", DataSource, "MaICD");
            txtngayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            txtngayra.DataBindings.Add("Text", DataSource, "Ngayra");
            colNamnu.DataBindings.Add("Text", DataSource, "Nam").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh");
            txtMabn.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTongngayDT.DataBindings.Add("Text", DataSource, "Songay");
            TongNgayRF.DataBindings.Add("Text", DataSource, "Songay");
            TongNgayGF2.DataBindings.Add("Text", DataSource, "Songay");
            TongNgayGF1.DataBindings.Add("Text", DataSource, "Songay");

            // Xét nghiệm
            colXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF1.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colGF2xetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF1.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            //Chuẩn đoán hình ảnh
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colGF2CDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            //Thuốc
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF2.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            //Máu
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colGF2Mau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauRF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            //Thủ thuật phẫu thuật
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colGF2TTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            // VTYT thường
            colVTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colGF2VTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF1.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            // VTYT Tỷ Lệ
            colVTYTtl.DataBindings.Add("Text", DataSource, "VTTTT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTtlGF1.DataBindings.Add("Text", DataSource, "VTTTT").FormatString = DungChung.Bien.FormatString[1];
            colGF2VTYTtl.DataBindings.Add("Text", DataSource, "VTTTT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTtlRF1.DataBindings.Add("Text", DataSource, "VTTTT").FormatString = DungChung.Bien.FormatString[1];
            // Dịch vụ KTC
            colDVKTC.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colDVKTgf1.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colDVKTgf2.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCRF1.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            // Thuốc KTC
            colThuocKCTG.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKTCgf1.DataBindings.Add("Text", DataSource, "ThuocKTCG").FormatString = DungChung.Bien.FormatString[1];
            colthuocKTCgf2.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF1.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            // Tiền giường - Công khám
            colTienGiuong.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongGF1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tiengiuong.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuongRF1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            // Chi phí vận chuyển
            colCPVanchuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPVanchuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenRF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //Người bệnh chi trả
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraGF1.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colGF2Nguoibenhcungchitra.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraRF1.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            // Tổng cộng BHYT
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colGF2TongcongBHYT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF11.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            // Chi phí ngoài
            colCPNgoai.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiGF1.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPNgoai.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiRF1.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            // Tổng cộng
            colTongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
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
            int TT = Convert.ToInt32(this.GetCurrentColumnValue("Nam"));
            if (TT == 1)
            {
                colNamsinhnu.Text = rong;
            }
            else
            {
                colNamsinhnam.Text = rong;
            }
        }

        private void colNgayvao_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Ngayvao") != null)
            {
                colNgayvao.Text = txtngayvao.Text.ToString().Substring(0, 5);
                colNgayra.Text = txtngayra.Text.ToString().Substring(0, 5);
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
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            txtMaCQ.Text = DungChung.Bien.MaBV;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
