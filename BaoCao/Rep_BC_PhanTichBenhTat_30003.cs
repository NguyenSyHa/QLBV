using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_PhanTichBenhTat_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_PhanTichBenhTat_30003()
        {
            InitializeComponent();
        }


        public void Bindingdata()
        {
            cel_TenBenh.DataBindings.Add("Text", DataSource, "TenBenh");
            cel_MaBenh.DataBindings.Add("Text", DataSource, "MaBenh");
            cel_TSMacBenh.DataBindings.Add("Text", DataSource, "TongSoMacBenh");
            cel_TSTuVong.DataBindings.Add("Text", DataSource, "TongTuVong");
            cel_TSNgayDT.DataBindings.Add("Text", DataSource, "TongNgayDT");
            cel_TSTreEmDuoi15MacBenh.DataBindings.Add("Text", DataSource, "TreEmDuoi15MacBenh");
            cel_TSTreEmDuoi4MacBenh.DataBindings.Add("Text", DataSource, "TreEmDuoi4MacBenh");
            cel_TSTreEmDuoi15TV.DataBindings.Add("Text", DataSource, "TreEmTuVongDuoi15");
            cel_TSTreEmDuoi4TV.DataBindings.Add("Text", DataSource, "TreEmTuVongDuoi4");
            cel_TSNgayDTTreEmDuoi15.DataBindings.Add("Text", DataSource, "TongNgayDTTreEmDuoi15");
            cel_TSNgayDTTrenEmDuoi4.DataBindings.Add("Text", DataSource, "TongNgayDTTreEmDuoi4");
            cel_TSMacBenhTren60.DataBindings.Add("Text", DataSource, "TongMacBenhTren60");
            cel_TSNuTren60MB.DataBindings.Add("Text", DataSource, "TongNuMacBenhTren60");
            cel_TSTuVongTren60.DataBindings.Add("Text", DataSource, "TongTuVongTren60");
            cel_TSNuTuVongTren60.DataBindings.Add("Text", DataSource, "TongNuTuVongTren60");

            gr_TSMacBenh.DataBindings.Add("Text", DataSource, "TongSoMacBenh");
            gr_TSTuVong.DataBindings.Add("Text", DataSource, "TongTuVong");
            gr_TongNgayDT.DataBindings.Add("Text", DataSource, "TongNgayDT");
            gr_TSTreEmDuoi15MB.DataBindings.Add("Text", DataSource, "TreEmDuoi15MacBenh");
            gr_TSTreEmDuoi4MB.DataBindings.Add("Text", DataSource, "TreEmDuoi4MacBenh");
            gr_TSTreEMDuoi15TV.DataBindings.Add("Text", DataSource, "TreEmTuVongDuoi15");
            gr_TSTreEmDuoi4TV.DataBindings.Add("Text", DataSource, "TreEmTuVongDuoi4");
            gr_TSNgayDTTreEmDuoi15.DataBindings.Add("Text", DataSource, "TongNgayDTTreEmDuoi15");
            gr_TSNgayDTTreEmDuoi4.DataBindings.Add("Text", DataSource, "TongNgayDTTreEmDuoi4");
            gr_TSTren60MB.DataBindings.Add("Text", DataSource, "TongMacBenhTren60");
            gr_TSNuTren60MB.DataBindings.Add("Text", DataSource, "TongNuMacBenhTren60");
            gr_TSTren60TV.DataBindings.Add("Text", DataSource, "TongTuVongTren60");
            gr_TSNuTren60TV.DataBindings.Add("Text", DataSource, "TongNuTuVongTren60");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
