using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuGluco : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuGluco()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {


            xrLabel3.Text  = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel4.Text = DungChung.Bien.TenCQ.ToUpper();
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04012")
                xrTableCell4.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (MaCBDT.Value != null)
            {
                if (DungChung.Bien.MaBV == "27023")
                    colTenBSDT.Text = "Họ tên: " + DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                else if(DungChung.Bien.MaBV == "30007")
                {
                    var tencbdt = DataContect.CanBoes.Where(p => p.MaCB == MaCBDT.Value).Select(p => p.TenCB).FirstOrDefault();
                    colTenBSDT.Text = tencbdt;
                }
                else
                    colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (Macb.Value != null)
            {
                var tencb = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.TenCB).FirstOrDefault();
                var chucVu = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.CapBac).FirstOrDefault();
                if (DungChung.Bien.MaBV == "27023")
                    colTenBSDT.Text = "Họ tên: " + DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                else if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30012")
                {

                    if (tencb != null && tencb != "")
                    {
                        colTenTKXN.Text = tencb;
                    }
                }
                else if (DungChung.Bien.MaBV == "27001")
                    colTenTKXN.Text = tencb;
                else
                    colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "20001")
                xrTableCell4.Text = "TL. TRƯỞNG KHOA XÉT NGHIỆM";
            
        }
        public void BindingData()
        {
            //colYC.DataBindings.Add("Text", DataSource, "YC");
            //colKQ.DataBindings.Add("Text", DataSource, "KQ");
        }



    }
}
