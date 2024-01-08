using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_SoLuuTruHS_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_SoLuuTruHS_30009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }

        public void BindingData()
        {
            cel_MaYT.DataBindings.Add("Text", DataSource, "MaYTe");
            cel_SoLuuTru.DataBindings.Add("Text", DataSource, "SoLT");
            cel_KhoaDT.DataBindings.Add("Text", DataSource, "KhoaDT");
            cel_HoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            cel_TuoiNam.DataBindings.Add("Text", DataSource, "TuoiNam");
            cel_TuoiNu.DataBindings.Add("Text", DataSource, "TuoiNu");
            cel_BHYT.DataBindings.Add("Text", DataSource, "BHYT");
            cel_TreEmDuoi6.DataBindings.Add("Text", DataSource, "TreEmDuoi6");
            cel_TreEmDuoi15.DataBindings.Add("Text", DataSource, "TreEmDuoi15");
            cel_Nghe.DataBindings.Add("Text", DataSource, "TenNN");
            cel_DanToc.DataBindings.Add("Text", DataSource, "TenDT");
            cel_NgoaiKieu.DataBindings.Add("Text", DataSource, "NgoaiKieu");
            cel_DiaChi.DataBindings.Add("Text", DataSource, "DChi");
            cel_NoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            cel_NgayVV.DataBindings.Add("Text", DataSource, "NgayVV").FormatString = "{0:dd/MM/yyyy HH:mm}";
            cel_NgayRV.DataBindings.Add("Text", DataSource, "NgayRV").FormatString = "{0:dd/MM/yyyy HH:mm}";
            cel_NgayCV.DataBindings.Add("Text", DataSource, "NgayCV").FormatString = "{0:dd/MM/yyyy HH:mm}";
            cel_CDNoiGT.DataBindings.Add("Text", DataSource, "CDNoiGT");
            cel_CDKhoaKB.DataBindings.Add("Text", DataSource, "CDKhoaKB");
            cel_CDKhoaDT.DataBindings.Add("Text", DataSource, "CDKhoaDT");
            cel_Khoi.DataBindings.Add("Text", DataSource, "Khoi");
            cel_DoGiam.DataBindings.Add("Text", DataSource, "DoGiam");
            cel_NangHon.DataBindings.Add("Text", DataSource, "NangHon");
            cel_KhongTDoi.DataBindings.Add("Text", DataSource, "KhongTDoi");
            cel_NguoiThan.DataBindings.Add("Text", DataSource, "NThan");
            cel_NgayDT.DataBindings.Add("Text", DataSource, "SoNgaydt");
        }
    }
}
