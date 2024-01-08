using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_THThuVPTheoThang_MauDoc_27021 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_THThuVPTheoThang_MauDoc_27021()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        public void BindingData()
        {
            celTuan.DataBindings.Add("Text", DataSource, "Tuan");

            celNgay.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0:dd/MM}";
            celThuNT.DataBindings.Add("Text", DataSource, "ThuNT").FormatString = DungChung.Bien.FormatString[1];
            celThuNgT.DataBindings.Add("Text", DataSource, "ThuNgT").FormatString = DungChung.Bien.FormatString[1];
            celThuTU.DataBindings.Add("Text", DataSource, "ThuTU").FormatString = DungChung.Bien.FormatString[1];
            celThuThem.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];
            celTraLai.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            celNTThuThem.DataBindings.Add("Text", DataSource, "NoiTruThuThem").FormatString = DungChung.Bien.FormatString[1];

            celThuNT_T.DataBindings.Add("Text", DataSource, "ThuNT");
            celThuNgT_T.DataBindings.Add("Text", DataSource, "ThuNgT");
            celThuTU_T.DataBindings.Add("Text", DataSource, "ThuTU");
            celThuThem_T.DataBindings.Add("Text", DataSource, "ThuThem");
            celTraLai_T.DataBindings.Add("Text", DataSource, "TraLai");
            celNTThuThem_T.DataBindings.Add("Text", DataSource, "NoiTruThuThem");

            GroupHeader1.GroupFields.Add(new GroupField("Tuan"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            lblNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
