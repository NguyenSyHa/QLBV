using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCRaVien_BN139 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCRaVien_BN139()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData()
        {
            celSoLT.DataBindings.Add("Text", DataSource, "SoLT");
            celKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            celTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoiNam.DataBindings.Add("Text", DataSource, "TuoiNam");
            celTuoiNu.DataBindings.Add("Text", DataSource, "TuoiNu");
            celBHYT.DataBindings.Add("Text", DataSource, "DTuong");
            celNgheNghiep.DataBindings.Add("Text", DataSource, "TenNN");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao");
            celNgayRa.DataBindings.Add("Text", DataSource, "NgayRa");
            celSoNgayDT.DataBindings.Add("Text", DataSource, "SoNgayDT");
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            celMaBenh.DataBindings.Add("Text", DataSource, "MaBenh");
            celKhoi.DataBindings.Add("Text", DataSource, "Khoi");
            celDo.DataBindings.Add("Text", DataSource, "Do");
            celNang.DataBindings.Add("Text", DataSource, "Nang");
            celKhongDoi.DataBindings.Add("Text", DataSource, "KhongDoi");
            celChuyenVien.DataBindings.Add("Text", DataSource, "ChuyenVien");
        }

    }
}
