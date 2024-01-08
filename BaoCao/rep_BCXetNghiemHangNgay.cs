using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCXetNghiemHangNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCXetNghiemHangNgay()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celSoBN.DataBindings.Add("Text", DataSource, "SoBN").FormatString = "{0:#,#}";
            celXNSinhHoa.DataBindings.Add("Text", DataSource, "SinhHoa").FormatString = "{0:#,#}";
            celXNHuyetHoc.DataBindings.Add("Text", DataSource, "HuyetHoc").FormatString = "{0:#,#}";
            celXNNuocTieu.DataBindings.Add("Text", DataSource, "NuocTieu").FormatString = "{0:#,#}";
            celXNViSinh.DataBindings.Add("Text", DataSource, "ViSinh").FormatString = "{0:#,#}";
            celXNMienDich.DataBindings.Add("Text", DataSource, "MienDich").FormatString = "{0:#,#}";
            celXNDongMau.DataBindings.Add("Text", DataSource, "DongMau").FormatString = "{0:#,#}";
            celDienGiai.DataBindings.Add("Text", DataSource, "DienGiai").FormatString = "{0:#,#}";
            celSoTieuBan.DataBindings.Add("Text", DataSource, "SoTieuBan").FormatString = "{0:#,#}";

            celSoBNT.DataBindings.Add("Text", DataSource, "SoBN").FormatString = "{0:#,#}";
            celXNSinhHoaT.DataBindings.Add("Text", DataSource, "SinhHoa").FormatString = "{0:#,#}";
            celXNHuyetHocT.DataBindings.Add("Text", DataSource, "HuyetHoc").FormatString = "{0:#,#}";
            celXNNuocTieuT.DataBindings.Add("Text", DataSource, "NuocTieu").FormatString = "{0:#,#}";
            celXNViSinhT.DataBindings.Add("Text", DataSource, "ViSinh").FormatString = "{0:#,#}";
            celXNMienDichT.DataBindings.Add("Text", DataSource, "MienDich").FormatString = "{0:#,#}";
            celXNDongMauT.DataBindings.Add("Text", DataSource, "DongMau").FormatString = "{0:#,#}";
            celDienGiaiT.DataBindings.Add("Text", DataSource, "DienGiai").FormatString = "{0:#,#}";
            celSoTieuBanT.DataBindings.Add("Text", DataSource, "SoTieuBan").FormatString = "{0:#,#}";

            celSoBNT.Summary.FormatString = "{0:#,#}";
            celXNSinhHoaT.Summary.FormatString = "{0:#,#}";
            celXNHuyetHocT.Summary.FormatString = "{0:#,#}";
            celXNNuocTieuT.Summary.FormatString = "{0:#,#}";
            celXNViSinhT.Summary.FormatString = "{0:#,#}";
            celXNMienDichT.Summary.FormatString = "{0:#,#}";
            celXNDongMauT.Summary.FormatString = "{0:#,#}";
            celDienGiaiT.Summary.FormatString = "{0:#,#}";
            celSoTieuBanT.Summary.FormatString = "{0:#,#}";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
