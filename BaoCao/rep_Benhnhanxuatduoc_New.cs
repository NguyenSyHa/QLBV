using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class rep_Benhnhanxuatduoc_New : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_Benhnhanxuatduoc_New()
        {
            InitializeComponent();
          
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colDichvu.DataBindings.Add("Text", DataSource, "Dichvu").FormatString = DungChung.Bien.FormatString[1];
            colRFDichvu.DataBindings.Add("Text", DataSource, "Dichvu").FormatString = DungChung.Bien.FormatString[1];
            colRFTTD.DataBindings.Add("Text", DataSource, "TTD").FormatString = DungChung.Bien.FormatString[1];
            colRFTTTT.DataBindings.Add("Text", DataSource, "TTTT").FormatString = DungChung.Bien.FormatString[1];
            colRFThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colRFVTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colTenbn.DataBindings.Add("Text",DataSource,"Tenbn");
            colTTD.DataBindings.Add("Text", DataSource, "TTD").FormatString = DungChung.Bien.FormatString[1];
            colTTTT.DataBindings.Add("Text", DataSource, "TTTT").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colTienChenh.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTienChenhRF.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongHXuat.DataBindings.Add("Text", DataSource, "TTD").FormatString = DungChung.Bien.FormatString[1];
            colNgayXuat.DataBindings.Add("Text", DataSource, "Ngayxuat").FormatString = "{0:dd/MM/yyyy}";
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            GroupHeader1.GroupFields.Add(new GroupField("Ngayxuat"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            txtDiaChi.Text = DungChung.Bien.MaBV;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoilapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }


    }
}
