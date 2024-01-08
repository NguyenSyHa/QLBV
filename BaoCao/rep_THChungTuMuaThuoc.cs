using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_THChungTuMuaThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_THChungTuMuaThuoc()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "30002")
            {
                SubBand1.Visible = false;
            }
            else
            {
                SubBand2.Visible = false;
            }
        }


        internal void BindingData()
        {
            colNgayNhap.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yyyy}";
            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            colTongTien.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            colTenNCC.DataBindings.Add("Text", DataSource, "TenCC");
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            GroupHeader1.GroupFields.Add(new GroupField("TenCC"));
            colTongTienGr.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            colTongTienRpt.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
