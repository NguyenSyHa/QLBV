using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_SoDe_30004 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_SoDe_30004()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celNghe.DataBindings.Add("Text", DataSource, "TenNN");
            celLanDe.DataBindings.Add("Text", DataSource, "SoLD");
            celNgayDe.DataBindings.Add("Text", DataSource, "NgaySinh").FormatString = "{0:dd/MM/yyyy}";
            celNoiDe1.DataBindings.Add("Text", DataSource, "NoiDe");
            celTSuNaoThai.DataBindings.Add("Text", DataSource, "TSNaoThai");
            celDeThuong.DataBindings.Add("Text", DataSource, "DeThuong");
            celDeKho.DataBindings.Add("Text", DataSource, "DeKho");
            celMoDe.DataBindings.Add("Text", DataSource, "MoDe");
            celDeChet.DataBindings.Add("Text", DataSource, "Chet");
            celBangHuyet.DataBindings.Add("Text", DataSource, "BangHuyet");
            celVoTC.DataBindings.Add("Text", DataSource, "VoTuCung");
            celSanGiat.DataBindings.Add("Text", DataSource, "SanGiat");
            celNhiemTrung.DataBindings.Add("Text", DataSource, "NhiemTrung");
            celUonVan.DataBindings.Add("Text", DataSource, "UonVan");
            celTraiCN.DataBindings.Add("Text", DataSource, "Trai");
            celGaiCN.DataBindings.Add("Text", DataSource, "Gai");
            celChetLuu.DataBindings.Add("Text", DataSource, "ChetLuu");
            celChetKhiDe.DataBindings.Add("Text", DataSource, "ChetKhiDe");
            celChetDuoi7Ngay.DataBindings.Add("Text", DataSource, "ChetDuoi7Ngay");
            celChetSau28Ngay.DataBindings.Add("Text", DataSource, "ChetSau28Ngay");
            celNguoiDoDe.DataBindings.Add("Text", DataSource, "CBTH");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
