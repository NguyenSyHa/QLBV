using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNoiTruThangth : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNoiTruThangth()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP").FormatString = DungChung.Bien.FormatString[1];
            colTongSo.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[1];
            colTongSoT.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[1];
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colVienPhi.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            colVienPhiT.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            colKhongThuDuoc.DataBindings.Add("Text", DataSource, "KhongThuDuoc").FormatString = DungChung.Bien.FormatString[1];
            colKhongThuDuocT.DataBindings.Add("Text", DataSource, "KhongThuDuoc").FormatString = DungChung.Bien.FormatString[1];
            colCapCuu.DataBindings.Add("Text", DataSource, "CapCuu").FormatString = DungChung.Bien.FormatString[1];
            colCapCuuT.DataBindings.Add("Text", DataSource, "CapCuu").FormatString = DungChung.Bien.FormatString[1];
            colBNVV.DataBindings.Add("Text", DataSource, "BNVV").FormatString = DungChung.Bien.FormatString[1];
            colBNVVT.DataBindings.Add("Text", DataSource, "BNVV").FormatString = DungChung.Bien.FormatString[1];
         }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
        }
    }
}
