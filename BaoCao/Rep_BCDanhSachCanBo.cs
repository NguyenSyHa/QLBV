using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCDanhSachCanBo : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCDanhSachCanBo()
        {
            InitializeComponent();
        }

        public void BindingData() 
        {
            celHoTen.DataBindings.Add("Text", DataSource, "TenCB");
            celNgaySinh.DataBindings.Add("Text", DataSource, "NgaySinh");
            celChuyenMon.DataBindings.Add("Text", DataSource, "BangCap");
            celCCHN.DataBindings.Add("Text", DataSource, "MaCCHN");
            celNgayCCHN.DataBindings.Add("Text", DataSource, "NgayCapCCHN").FormatString = "{0:dd/MM/yyyy}";
            celHSLuong.DataBindings.Add("Text", DataSource, "HSLuong");
            celNgayTangLuong.DataBindings.Add("Text", DataSource, "NgayTangLuong").FormatString = "{0:dd/MM/yyyy}";
            celBienChe.DataBindings.Add("Text", DataSource, "BienChe");
            colMaNgach.DataBindings.Add("Text", DataSource, "MaNgach");
            celTinHoc.DataBindings.Add("Text", DataSource, "TinHoc");
            celChucVu.DataBindings.Add("Text", DataSource, "ChucVu");
            celNgoaiNgu.DataBindings.Add("Text", DataSource, "NgoaiNgu");
            celTenKP.DataBindings.Add("Text", DataSource, "tenkp");
            colSL.DataBindings.Add("Text", DataSource, "SL");
            GroupHeader1.GroupFields.Add(new GroupField("stt"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
