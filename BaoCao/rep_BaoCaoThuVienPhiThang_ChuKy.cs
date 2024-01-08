using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BaoCaoThuVienPhiThang_ChuKy : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BaoCaoThuVienPhiThang_ChuKy()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celKTT.Text = DungChung.Bien.KeToanTruong;
        }

    }
}
