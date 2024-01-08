using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_THTheoDoiBNNoiTru_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_THTheoDoiBNNoiTru_27022()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celNgayNhap.DataBindings.Add("Text", DataSource, "NNhap");
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celMaBA.DataBindings.Add("Text", DataSource, "SoBA");
            celHoTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celNgayVV.DataBindings.Add("Text", DataSource, "NgayVao");
            celNgayCV.DataBindings.Add("Text", DataSource, "NgayCV");
            celTuyen.DataBindings.Add("Text", DataSource, "Tuyen");
            celMST.DataBindings.Add("Text", DataSource, "SThe");
            celTenBenh.DataBindings.Add("Text", DataSource, "TenBenh");
            celMaBenh.DataBindings.Add("Text", DataSource, "MaBenh");
            celTienTamUng.DataBindings.Add("Text", DataSource, "TienUng").FormatString = DungChung.Bien.FormatString[1];
            celNgayRV.DataBindings.Add("Text", DataSource, "NgayRa");
            celTongTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celBNTT.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celBHTT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celNgayTT.DataBindings.Add("Text", DataSource, "NgayTT");
            celTienThua.DataBindings.Add("Text", DataSource, "ChiTienThua").FormatString = DungChung.Bien.FormatString[1];
            celThuThem.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];
            celTTNgoai.DataBindings.Add("Text", DataSource, "TTNgoai").FormatString = DungChung.Bien.FormatString[1];
            celLoaiTTT.DataBindings.Add("Text", DataSource, "TenDV");
        }
    }
}
