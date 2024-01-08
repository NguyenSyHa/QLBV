using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCKeThuocTheoBSi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCKeThuocTheoBSi()
        {
            InitializeComponent();
        }
        public void Binding()
        {
            colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
            colMaTam.DataBindings.Add("Text", DataSource, "MaTam");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongTT.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = string.Format("{0:#,##0.##}", 0);

        }

    }
}
