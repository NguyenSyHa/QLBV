using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_TheoNhomTuoivaGTinh_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_TheoNhomTuoivaGTinh_12122()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            lblNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        public void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celTS.DataBindings.Add("Text", DataSource, "TS");
            celTSTE6.DataBindings.Add("Text", DataSource, "TE6");
            celTSTE15.DataBindings.Add("Text", DataSource, "TE15");
            celTSTren60.DataBindings.Add("Text", DataSource, "Tren60");
            celNam.DataBindings.Add("Text", DataSource, "Nam");
            celNu.DataBindings.Add("Text", DataSource, "Nu");

            celTS_T.DataBindings.Add("Text", DataSource, "TS");
            celTSTE6_T.DataBindings.Add("Text", DataSource, "TE6");
            celTSTE15_T.DataBindings.Add("Text", DataSource, "TE15");
            celTSTren60_T.DataBindings.Add("Text", DataSource, "Tren60");
            celNam_T.DataBindings.Add("Text", DataSource, "Nam");
            celNu_T.DataBindings.Add("Text", DataSource, "Nu");
        }
    }
}
