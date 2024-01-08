using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BBKiemNhap_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BBKiemNhap_20001()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

            cel_tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_hangsx.DataBindings.Add("Text", DataSource, "NhaSX");
            cel_nuocsx.DataBindings.Add("Text", DataSource, "NuocSX");
            cel_dvt.DataBindings.Add("Text", DataSource, "DonVi");
            cel_slct.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            cel_sltt.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            cel_dongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            cel_tt.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            cel_sodk.DataBindings.Add("Text", DataSource, "SoDK");
            //cel_solo.DataBindings.Add("Text", DataSource, "SoLo");
            cel_handung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "20001")
                lblTruongKhoa.Text = "TRƯỞNG KHOA DƯỢC";
        }

    }
}
