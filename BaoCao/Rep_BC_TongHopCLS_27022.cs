using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_TongHopCLS_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_TongHopCLS_27022()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lblNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celQuyen.DataBindings.Add("Text", DataSource, "QuyenHD");
            celSoCT.DataBindings.Add("Text", DataSource, "SoHD");
            celNgay.DataBindings.Add("Text", DataSource, "Ngay");
            celHoTenBN.DataBindings.Add("Text", DataSource, "Hoten");
            celDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            celTamUng.DataBindings.Add("Text", DataSource, "TienUng");
            celKham.DataBindings.Add("Text", DataSource, "TTKham");
            celDV1.DataBindings.Add("Text", DataSource, "TTDV1");
            celDV2.DataBindings.Add("Text", DataSource, "TTDV2");
            celDV3.DataBindings.Add("Text", DataSource, "TTDV3");
            celDV4.DataBindings.Add("Text", DataSource, "TTDV4");
            celDV5.DataBindings.Add("Text", DataSource, "TTDV5");
            celDV6.DataBindings.Add("Text", DataSource, "TTDV6");
            celDV7.DataBindings.Add("Text", DataSource, "TTDV7");
            celDV8.DataBindings.Add("Text", DataSource, "TTDV8");
            celDV9.DataBindings.Add("Text", DataSource, "TTDV9");
            celTong.DataBindings.Add("Text", DataSource, "Tong");
            celTienThua.DataBindings.Add("Text", DataSource, "TienThua");
            celThuThem.DataBindings.Add("Text", DataSource, "ThuThem");

            celDV1_T.DataBindings.Add("Text", DataSource, "TTDV1");
            celDV2_T.DataBindings.Add("Text", DataSource, "TTDV2");
            celDV3_T.DataBindings.Add("Text", DataSource, "TTDV3");
            celDV4_T.DataBindings.Add("Text", DataSource, "TTDV4");
            celDV5_T.DataBindings.Add("Text", DataSource, "TTDV5");
            celDV6_T.DataBindings.Add("Text", DataSource, "TTDV6");
            celDV7_T.DataBindings.Add("Text", DataSource, "TTDV7");
            celDV8_T.DataBindings.Add("Text", DataSource, "TTDV8");
            celDV9_T.DataBindings.Add("Text", DataSource, "TTDV9");
            celKham_T.DataBindings.Add("Text", DataSource, "TTKham");
            celTongT.DataBindings.Add("Text", DataSource, "Tong");
            celTamUng_T.DataBindings.Add("Text", DataSource, "TienUng");
            celTienThua_T.DataBindings.Add("Text", DataSource, "TienThua");
            celThuThem_T.DataBindings.Add("Text", DataSource, "ThuThem");
        }

    }
}
