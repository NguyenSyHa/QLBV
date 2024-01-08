using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DSBenhNhanCapCuu_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DSBenhNhanCapCuu_01071()
        {
            InitializeComponent();
        }
        public void Databinding()
        {
            cel_tenbn.DataBindings.Add("Text", DataSource, "TenBNhan");
            cel_tuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            cel_tchung.DataBindings.Add("Text", DataSource, "TChung");
            cel_ngaynhap.DataBindings.Add("Text", DataSource, "NNhap");
            cel_ketqua.DataBindings.Add("Text", DataSource, "KetQua");
            cel_IDC.DataBindings.Add("Text", DataSource, "MaICD");
            cel_gtinh.DataBindings.Add("Text", DataSource, "GTinh");
            cel_dtuong.DataBindings.Add("Text", DataSource, "DTuong");
            cel_dchi.DataBindings.Add("Text", DataSource, "DChi");
            cel_chandoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            cel_KhoaVV.DataBindings.Add("Text", DataSource, "KhoaVV");
            
        }
    }
}
