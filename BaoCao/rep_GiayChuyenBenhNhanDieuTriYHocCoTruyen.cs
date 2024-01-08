using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using QLBV.FormNhap;

namespace QLBV.BaoCao
{
    public partial class rep_GiayChuyenBenhNhanDieuTriYHocCoTruyen : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_GiayChuyenBenhNhanDieuTriYHocCoTruyen()
        {
            InitializeComponent();
        }

        public void databindind()
        {
            lblHoTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            lblTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            lblGioiTinh.Text = frm_CVienNoiTru.GTinh;
            lblSoVaoVien.DataBindings.Add("Text", DataSource, "SoVaoVien");
            lblDiaChi.DataBindings.Add("Text", DataSource, "diaChi");
            lblSoTheBH.DataBindings.Add("Text", DataSource, "SoTheBH");
            lblNgayGioVaoKhoa.Text = Convert.ToString(frm_CVienNoiTru.GioVao.ToString("HH: mm"));
            lblNgay.Text = Convert.ToString(frm_CVienNoiTru.NgayVao.ToString("dd-MM-yyyy"));
            lblGiuongSo.DataBindings.Add("Text", DataSource, "giuong");
            lblBuongSo.DataBindings.Add("Text", DataSource, "buong");
            lblMach.DataBindings.Add("Text", DataSource, "mach");
            lblNhietDo.DataBindings.Add("Text", DataSource, "nhietDo");
            lblHuyetAp.DataBindings.Add("Text", DataSource, "huyetAp");
            lblNhipTho.DataBindings.Add("Text", DataSource, "nhipTho");
            lblCanNang.DataBindings.Add("Text", DataSource, "canNang");
            lblTrieuChung.DataBindings.Add("Text", DataSource, "trieuchung");
            lblChanDoan.DataBindings.Add("Text", DataSource, "chanDoan");
        }
    }
}
