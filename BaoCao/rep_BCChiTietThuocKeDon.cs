using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_BCChiTietThuocKeDon : DevExpress.XtraReports.UI.XtraReport
    {
        double xtrang = 0;
        public rep_BCChiTietThuocKeDon()
        {
            InitializeComponent();
        }
        public rep_BCChiTietThuocKeDon(List<CTThuocKe> ds)
        {
            InitializeComponent();

        }
        public class CTThuocKe
        {
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            DateTime ngayKe;

            public DateTime NgayKe
            {
                get { return ngayKe; }
                set { ngayKe = value; }
            }
            int maBNhan;

            public int MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            string tenBNhan;

            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
            double donGia;

            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            double soLuong;

            public double SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            double thanhTien;

            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            string soPL;

            public string SoPL
            {
                get { return soPL; }
                set { soPL = value; }
            }
        }
        List<CTThuocKe> _lds = new List<CTThuocKe>();
        public void BindingData()
        {

            celNgayKe.DataBindings.Add("Text", DataSource, "NgayKe").FormatString = "{0:dd/MM/yyyy}";
            celSoPL.DataBindings.Add("Text", DataSource, "SoPL");
            celMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan").FormatString = "{0:#}";
            celTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            celSoLuongT.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celThanhTienT.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenDV"));
            GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
        }
        int _break = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_break == 0)
            {
                xrPageBreak1.Visible = false;
            }
            else
            {
                xrPageBreak1.Visible = Convert.ToBoolean(PhanTrang.Value);
            }
            _break++; 
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
