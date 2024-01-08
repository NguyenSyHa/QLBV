using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCCuoiNam_KinhMon : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCCuoiNam_KinhMon()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tencq.Text = DungChung.Bien.TenCQ;
        }
        public void BindingData()
        {
            celTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            celMaATC.DataBindings.Add("Text", DataSource, "MaATC");
            celMaNB.DataBindings.Add("Text", DataSource, "MaNB");
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            celSoDK.DataBindings.Add("Text", DataSource, "SoDK");
            celNoiSX.DataBindings.Add("Text", DataSource, "NoiSX");
            celND_HL.DataBindings.Add("Text", DataSource, "ND_HL");
            celDVT.DataBindings.Add("Text", DataSource, "DVT");
            celDuongDung.DataBindings.Add("Text", DataSource, "DuongDung");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celTonDK.DataBindings.Add("Text", DataSource, "TonDK");
            celNhap.DataBindings.Add("Text", DataSource, "Nhap");
            celNhapKhac.DataBindings.Add("Text", DataSource, "NhapKhac");
            celXuat.DataBindings.Add("Text", DataSource, "Xuat");
            celXuatKhac.DataBindings.Add("Text", DataSource, "XuatKhac");
            celTonCK.DataBindings.Add("Text", DataSource, "TonCK");
        }
    }
}
