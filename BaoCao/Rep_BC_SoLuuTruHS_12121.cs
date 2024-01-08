using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_SoLuuTruHS_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_SoLuuTruHS_12121()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }

        public void BindingData()
        {
            celSoLT.DataBindings.Add("Text", DataSource, "SoLT");
            celSoVV.DataBindings.Add("Text", DataSource, "SoVV");
            celHoTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoiNam.DataBindings.Add("Text", DataSource, "TuoiNam");
            celTuoiNu.DataBindings.Add("Text", DataSource, "TuoiNu");
            celVienChuc.DataBindings.Add("Text", DataSource, "CVChuc");
            celBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            celThanhThi.DataBindings.Add("Text", DataSource, "ThanhThi");
            celNongThon.DataBindings.Add("Text", DataSource, "NongThon");
            celTreEmDuoi12Thang.DataBindings.Add("Text", DataSource, "TreEmDuoi1");
            celTreEmDuoi15T.DataBindings.Add("Text", DataSource, "TreEm1Den15");
            celNghe.DataBindings.Add("Text", DataSource, "TenNN");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            celNgayVV.DataBindings.Add("Text", DataSource, "NgayVV").FormatString = "{0:dd/MM/yyyy HH:mm}";
            celNgayRV.DataBindings.Add("Text", DataSource, "NgayRV").FormatString = "{0:dd/MM/yyyy HH:mm}";
            celNgayCV.DataBindings.Add("Text", DataSource, "NgayCV").FormatString = "{0:dd/MM/yyyy HH:mm}";
            //cel_CDNoiGT.DataBindings.Add("Text", DataSource, "CDNoiGT");
            celCDTuyenDuoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            celCDKhoaKB.DataBindings.Add("Text", DataSource, "CDKhoaKB");
            celCDKhoaDT.DataBindings.Add("Text", DataSource, "CDKhoaDT");
            //celCDKhoaGP.DataBindings.Add("Text", DataSource, "DoGiam");//chẩn đoán khoa giải phẫu bệnh chưa lấy dữ liệu
            celKhoi.DataBindings.Add("Text", DataSource, "Khoi");
            celDoGiam.DataBindings.Add("Text", DataSource, "DoGiam");
            celNangHon.DataBindings.Add("Text", DataSource, "NangHon");
            celKhongTDoi.DataBindings.Add("Text", DataSource, "KhongTDoi");
        }
    }
}
