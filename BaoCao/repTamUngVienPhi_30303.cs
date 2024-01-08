using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repTamUngVienPhi_30303 : DevExpress.XtraReports.UI.XtraReport
    {
        public repTamUngVienPhi_30303()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "30009")
                this.HTTT.Value = "Hình thức tạm thu: Tiền mặt";
            else
                this.HTTT.Value = "Hình thức thanh toán: Tiền mặt";
            txtTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
            {
                labNgayGiol1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            }
            if (DungChung.Bien.MaBV == "04018" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
            {
                txtNguoiNop1.Visible = true;
                txtNguoiNop1_1.Visible = true;
               
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a=_data.CanBoes.Where(p=>p.MaCB== (DungChung.Bien.MaCB)).ToList();
            if (a.Count > 0)
                colNguoiThu.Text = a.First().TenCB;
        }

    }
}
