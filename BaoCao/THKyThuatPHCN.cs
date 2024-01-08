using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class THKyThuatPHCN : DevExpress.XtraReports.UI.XtraReport
    {
        public THKyThuatPHCN()
        {
            InitializeComponent();
        }
        public void Binding()
        {
            colDVKT.DataBindings.Add("Text", DataSource, "TenDV");
            colNgayGio.DataBindings.Add("Text", DataSource, "NgayGio");
            colThoiGianTH.DataBindings.Add("Text", DataSource, "ThoiGianTH");
            colDienBien.DataBindings.Add("Text", DataSource, "MoTa");
            colNguoiTH.DataBindings.Add("Text", DataSource, "NguoiThucHien");
            colBsChiDinh.DataBindings.Add("Text", DataSource, "BSCD");
            colXacNhan.DataBindings.Add("Text", DataSource, "KetQua");
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
