using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNoiTruThangct : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNoiTruThangct()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colDBBT.DataBindings.Add("Text", DataSource, "TenICD").FormatString = DungChung.Bien.FormatString[1];
            colMaBenh.DataBindings.Add("Text", DataSource, "MaICD").FormatString = DungChung.Bien.FormatString[1];
            colTongSoLK.DataBindings.Add("Text", DataSource, "TongSoLK").FormatString = DungChung.Bien.FormatString[1];
            colTongSoLKT.DataBindings.Add("Text", DataSource, "TongSoLK").FormatString = DungChung.Bien.FormatString[1];
            colTreEm.DataBindings.Add("Text", DataSource, "TongSoLKTE").FormatString = DungChung.Bien.FormatString[1];
            colTreEmT.DataBindings.Add("Text", DataSource, "TongSoLKTE").FormatString = DungChung.Bien.FormatString[1];
            colNu.DataBindings.Add("Text", DataSource, "TongSoLKNu").FormatString = DungChung.Bien.FormatString[1];
            colNuT.DataBindings.Add("Text", DataSource, "TongSoLKNu").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
      
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    }

}