using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repBCChiTietLSSDThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public repBCChiTietLSSDThuoc()
        {
            InitializeComponent();
        }

        string _tenBV, _tenTD;
        List<string> _txtKP = new List<string>();

        public repBCChiTietLSSDThuoc(string tenBV, string tenTD, List<string> txtkp)
        {
            InitializeComponent();
            _tenBV = tenBV;
            _tenTD = tenTD;
            _txtKP = txtkp;
        }

        public void BindingData()
        {
            txtTenBV.Text = _tenBV;
            txtTieuDe.Text = _tenTD;
            foreach (var item in _txtKP)
            {
                txtKhoaPhong.Text += item + ",";
            }
            
            colIDCT.DataBindings.Add("Text", DataSource, "ID");
            colNgayThang.DataBindings.Add("Text", DataSource, "Ngay");
            colKP.DataBindings.Add("Text", DataSource, "TenKP");
            colMaBN.DataBindings.Add("Text", DataSource, "MaBN");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            colDongia.DataBindings.Add("Text", DataSource, "DonGia");
            colSLKD.DataBindings.Add("Text", DataSource, "SoLuongKD");
            colTTKD.DataBindings.Add("Text", DataSource, "ThanhTienKD");
            colSLDX.DataBindings.Add("Text", DataSource, "SoLuongXD");
            colSoPL.DataBindings.Add("Text", DataSource, "SoPL");
            colSolo.DataBindings.Add("Text", DataSource, "SoLo");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");
            colKPXuat.DataBindings.Add("Text", DataSource, "TenKXuat");
            colNgayvao.DataBindings.Add("Text", DataSource, "NgayVao");
            colNgayra.DataBindings.Add("Text", DataSource, "NgayRa");
        }
    }
}
