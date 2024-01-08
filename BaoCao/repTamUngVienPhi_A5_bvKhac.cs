using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class
        repTamUngVienPhi_A5_bvKhac : DevExpress.XtraReports.UI.XtraReport
    {
        public repTamUngVienPhi_A5_bvKhac()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
                this.HTTT.Value = "Hình thức tạm thu: Tiền mặt";
            else
                this.HTTT.Value = "Hình thức thanh toán: Tiền mặt";
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtNguoiNop1.Visible = true;
            txtNguoiNop1_1.Visible = true;
            if (DungChung.Bien.MaBV == "30010")
            {
                nguoithu1.Visible = true;
                nguoithu1.Text = DungChung.Bien.NguoiLapBieu;
            }
                
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell6.Visible = false;
                xrTableCell7.Visible = false;
                xrTableCell9.Visible = false;
                xrTableCell14.Visible = false;
                xrTableCell15.Visible = false;
                txtNguoiNop1.Visible = false;
                txtNguoiNop1_1.Visible = false;
                txtNguoiNop1_3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30372") 
            {
                nguoithu1.Visible = true;
            } 
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a = _data.CanBoes.Where(p => p.MaCB == (DungChung.Bien.MaCB)).ToList();
            if (a.Count > 0)
                colNguoiThu.Text = a.First().TenCB;
        }

        private void repTamUngVienPhi_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lblNgayIn2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

    }
}
