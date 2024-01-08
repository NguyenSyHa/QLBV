using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuChupCatLopVT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuChupCatLopVT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            txtNgUyQ.Text = DungChung.Bien.TruongKhoaLS;
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
