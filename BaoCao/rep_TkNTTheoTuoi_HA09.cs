using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TkNTTheoTuoi_HA09 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TkNTTheoTuoi_HA09()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

            colTongCongNu.DataBindings.Add("Text", DataSource, "TongCongNu").FormatString = DungChung.Bien.FormatString[1];
            colTongCongNam.DataBindings.Add("Text", DataSource, "TongCongNam").FormatString = DungChung.Bien.FormatString[1];
            colTongCongTong.DataBindings.Add("Text", DataSource, "TongCongTong").FormatString = DungChung.Bien.FormatString[1];

            col6TNu.DataBindings.Add("Text", DataSource, "T6Nu").FormatString = DungChung.Bien.FormatString[1];
            col6TNam.DataBindings.Add("Text", DataSource, "T6Nam").FormatString = DungChung.Bien.FormatString[1];
            col6TTong.DataBindings.Add("Text", DataSource, "T6Tong").FormatString = DungChung.Bien.FormatString[1];

            col15TNu.DataBindings.Add("Text", DataSource, "T15Nu").FormatString = DungChung.Bien.FormatString[1];
            col15TNam.DataBindings.Add("Text", DataSource, "T15Nam").FormatString = DungChung.Bien.FormatString[1];
            col15TTong.DataBindings.Add("Text", DataSource, "T15Tong").FormatString = DungChung.Bien.FormatString[1];

            col55TNu.DataBindings.Add("Text", DataSource, "T55Nu").FormatString = DungChung.Bien.FormatString[1];
            col55TNam.DataBindings.Add("Text", DataSource, "T55Nam").FormatString = DungChung.Bien.FormatString[1];
            col55TTong.DataBindings.Add("Text", DataSource, "T55Tong").FormatString = DungChung.Bien.FormatString[1];

            col60TNu.DataBindings.Add("Text", DataSource, "T60Nu").FormatString = DungChung.Bien.FormatString[1];
            col60TNam.DataBindings.Add("Text", DataSource, "T60Nam").FormatString = DungChung.Bien.FormatString[1];
            col60TTong.DataBindings.Add("Text", DataSource, "T60Tong").FormatString = DungChung.Bien.FormatString[1];

            col80TNu.DataBindings.Add("Text", DataSource, "T80Nu").FormatString = DungChung.Bien.FormatString[1];
            col80TNam.DataBindings.Add("Text", DataSource, "T80Nam").FormatString = DungChung.Bien.FormatString[1];
            col80TTong.DataBindings.Add("Text", DataSource, "T80Tong").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQCQ.Text = DungChung.Bien.TenCQCQ;
            colCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
