using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repThuChi_2lien_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repThuChi_2lien_A4()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                PageFooter.Visible = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a=_data.CanBoes.Where(p=>p.MaCB== (DungChung.Bien.MaCB)).ToList();
            if (a.Count > 0)
            { 
                
                colNguoiThu.Text = a.First().TenCB;
                colNguoiThul2.Text = a.First().TenCB;
                
            }
        }

        private void repThuChi_2lien_A4_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrLabel59_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void labngaygio_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            labngaygio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

    }
}
