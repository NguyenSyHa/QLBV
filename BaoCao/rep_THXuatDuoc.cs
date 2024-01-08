using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_THXuatDuoc : DevExpress.XtraReports.UI.XtraReport
    {
        bool _hienct = false, _hientong = false;
        public rep_THXuatDuoc()
        {
            InitializeComponent();
        }
        public rep_THXuatDuoc(bool _hien)
        {
            InitializeComponent();
            _hienct = _hien;
        }
        public rep_THXuatDuoc(bool _hien, bool htong)
        {
            InitializeComponent();
            _hienct = _hien;
            _hientong = htong;
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            colNgay.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yy}";
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            colSoLuongGF.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGF.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGH1.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienCong.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];

            colTenCC.DataBindings.Add("Text", DataSource, "TenKP");

            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            if (_hienct == false)
            {
                colSo2.DataBindings.Add("Text", DataSource, "SoCT");
                colNgay2.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yy}";
                colTen.DataBindings.Add("Text", DataSource, "TenKP");

            }
            else
            {
                if (DungChung.Bien.MaBV == "12122")
                {
                    GroupHeader1.GroupFields.Add(new GroupField("NgayNhap"));
                    GroupHeader1.GroupFields.Add(new GroupField("SoCT"));
                }
                else
                    GroupHeader1.GroupFields.Add(new GroupField("SoCT"));

            }
            if (_hientong)
            {
                GroupHeader1.GroupFields.Add(new GroupField("NgayNhap"));
            }
            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
            if ((_hienct && _hientong) || (!_hienct && !_hientong))
            {
                GroupHeader1.GroupFields.Add(new GroupField("SoCT"));
            }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }



        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            if (_hienct == true)
            {
                PageHeader.Visible = true;
                GroupHeader1.Visible = true;

                Detail.Visible = true;
                xrLine2.Visible = true;
                xrLine3.Visible = false;
            }
            else
            {
                xrLine2.Visible = false;
                PageHeader.Visible = false;
                GroupHeader1.Visible = false;
                Detail.Visible = false;
                xrLine3.Visible = true;
            }

        }



    }
}
