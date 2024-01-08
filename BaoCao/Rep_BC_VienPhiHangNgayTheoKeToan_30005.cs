using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_VienPhiHangNgayTheoKeToan_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        private int dem;

        public Rep_BC_VienPhiHangNgayTheoKeToan_30005()
        {
            InitializeComponent();
        }

        public Rep_BC_VienPhiHangNgayTheoKeToan_30005(int dem)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.dem = dem;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        public void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "NgayThang").FormatString = "{0: dd/MM/yyyy}";

            celSTT.DataBindings.Add("Text", DataSource, "STT");
            celHoTen.DataBindings.Add("Text", DataSource, "HoTenCB");
            colThuThang.DataBindings.Add("Text", DataSource, "ThuThang").FormatString = DungChung.Bien.FormatString[1];
            colKsk.DataBindings.Add("Text", DataSource, "Ksk").FormatString = DungChung.Bien.FormatString[1];
            colNgoaiTru.DataBindings.Add("Text", DataSource, "NgoaiTru").FormatString = DungChung.Bien.FormatString[1];
            colNoiTru.DataBindings.Add("Text", DataSource, "NoiTru").FormatString = DungChung.Bien.FormatString[1];
            colVPND.DataBindings.Add("Text", DataSource, "VienPhiNhanDan").FormatString = DungChung.Bien.FormatString[1];
            colTruc.DataBindings.Add("Text", DataSource, "Truc").FormatString = DungChung.Bien.FormatString[1];
            colTongThu.DataBindings.Add("Text", DataSource, "TongThu").FormatString = DungChung.Bien.FormatString[1];

            celTongThuThang.DataBindings.Add("Text", DataSource, "ThuThang").FormatString = DungChung.Bien.FormatString[1];
            celTongKsk.DataBindings.Add("Text", DataSource, "Ksk").FormatString = DungChung.Bien.FormatString[1];
            colTongNgoaiTru.DataBindings.Add("Text", DataSource, "NgoaiTru").FormatString = DungChung.Bien.FormatString[1];
            colTongNoiTru.DataBindings.Add("Text", DataSource, "NoiTru").FormatString = DungChung.Bien.FormatString[1];
            celTongVPND.DataBindings.Add("Text", DataSource, "VienPhiNhanDan").FormatString = DungChung.Bien.FormatString[1];
            celTongTruc.DataBindings.Add("Text", DataSource, "Truc").FormatString = DungChung.Bien.FormatString[1];
            celTongTongThu.DataBindings.Add("Text", DataSource, "TongThu").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NgayThang"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celnguoilap.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
