using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_TheoDoiTTKCB_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_TheoDoiTTKCB_30009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoilap.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");

            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celGTinh.DataBindings.Add("Text", DataSource, "GTinh");
            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM/yyyy}";
            celNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";
            celTienTamThu.DataBindings.Add("Text", DataSource, "TienUng").FormatString = DungChung.Bien.FormatString[1];
            celTienTT.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            celTienTraLai.DataBindings.Add("Text", DataSource, "TienTra").FormatString = DungChung.Bien.FormatString[1];
            celTienPhaiNop.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];

            celTienTamThu_T.DataBindings.Add("Text", DataSource, "TienUng");
            celTienTT_T.DataBindings.Add("Text", DataSource, "TT");
            celTienTra_T.DataBindings.Add("Text", DataSource, "TienTra");
            celTienPhaiNop_T.DataBindings.Add("Text", DataSource, "ThuThem");

            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
        }
    }
}
