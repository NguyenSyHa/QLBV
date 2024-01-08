using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChiDinh_CDHA_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChiDinh_CDHA_A4()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                lab3.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
                colSo.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
                lab2.Visible = false;
            }

        }
        public void BindingData()
        {
            colYCKT.DataBindings.Add("Text", DataSource, "_lIdCLS");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                colTenCQ.Font = new Font(colTenCQ.Font, FontStyle.Regular);
                xrLabel6.Font = new Font(xrLabel6.Font, FontStyle.Regular);
                colSo.Font = new Font(colSo.Font, FontStyle.Regular);
                lab1.Font = new Font(lab1.Font, FontStyle.Regular);
                xrLabel5.Font = new Font(xrLabel5.Font, FontStyle.Regular);
                lab2.Font = new Font(lab2.Font, FontStyle.Regular);
                lab3.Font = new Font(lab3.Font, FontStyle.Regular);
                lab_STT.Font = new Font(lab_STT.Font, FontStyle.Regular);
                colTenBSDT.Font = new Font(colTenBSDT.Font, FontStyle.Regular);
                par_STT.Font = new Font(par_STT.Font, FontStyle.Regular);
                labsovv.Font = new Font(labsovv.Font, FontStyle.Regular);
                xrLabel4.Font = new Font(xrLabel4.Font, FontStyle.Regular);
                colYCKT.Font = new Font(colYCKT.Font, FontStyle.Regular);
                lab54.Font = new Font(lab54.Font, FontStyle.Regular);
                colTenBSDT.Font = new Font(colTenBSDT.Font, FontStyle.Regular);
                labmabn.Font = new Font(labmabn.Font, FontStyle.Regular);
            }


            if (DungChung.Bien.MaBV.Substring(0, 2) == "12")
                colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper() + "\n" + DungChung.Bien.TenCQ.ToUpper();
            else
                colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //colTenCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "30009")
            {
                colSo.Visible = false;
                lab_STT.Visible = false;
                par_STT.Visible = false;
                this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(578.9722F, 41.79169F);
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                xrLabel16.Visible = true;
                xrLabel13.Visible = true;
                xrLabel14.Visible = true;
                xrLabel15.Visible = true;
                labsovv.Visible = true;
                labmabn.Visible = true;
            }
            else
            {
                xrLabel16.Visible = false;
                xrLabel13.Visible = false;
                xrLabel14.Visible = false;
                xrLabel15.Visible = false;
            }

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                labmabn.LocationF = lab2.LocationF;
                colSo.Visible = lab3.Visible = lab2.Visible = lab_STT.Visible = par_STT.Visible = labsovv.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (BSDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, BSDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

        

    }
}
