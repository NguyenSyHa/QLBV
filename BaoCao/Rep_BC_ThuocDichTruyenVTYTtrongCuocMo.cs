using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ThuocDichTruyenVTYTtrongCuocMo : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ThuocDichTruyenVTYTtrongCuocMo()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lbltenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNgayThang.Text = "Thanh Hà, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            cel_TenBN.DataBindings.Add("Text", DataSource, "benhnhan");
            cel_ChanDoan.DataBindings.Add("Text", DataSource, "chandoan");
            cel_Thuoc.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
            cel_DTruyen.DataBindings.Add("Text", DataSource, "TienDTruyen").FormatString = DungChung.Bien.FormatString[1];
            cel_VTYT.DataBindings.Add("Text", DataSource, "TienVTYT").FormatString = DungChung.Bien.FormatString[1];
            cel_TTien.DataBindings.Add("Text", DataSource, "TTien").FormatString = DungChung.Bien.FormatString[1];

            celThuocT.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
            celDtruyenT.DataBindings.Add("Text", DataSource, "TienDTruyen").FormatString = DungChung.Bien.FormatString[1];
            celVTYTT.DataBindings.Add("Text", DataSource, "TienVTYT").FormatString = DungChung.Bien.FormatString[1];
            celTongT.DataBindings.Add("Text", DataSource, "TTien").FormatString = DungChung.Bien.FormatString[1];

            gr_Khoa.DataBindings.Add("Text", DataSource, "khoa");
            gr_TongThuoc.DataBindings.Add("Text", DataSource, "TienThuoc");
            gr_TongDTruyen.DataBindings.Add("Text", DataSource, "TienDTruyen");
            gr_TongVTYT.DataBindings.Add("Text", DataSource, "TienVTYT");
            gr_TTien.DataBindings.Add("Text", DataSource, "TTien");

            GroupHeader1.GroupFields.Add(new GroupField("khoa"));
        }
    }
}
