using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNKhac_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNKhac_A5()
        {
            InitializeComponent();
        }
        bool hienthiGF = false;
        public Rep_PhieuXNKhac_A5(bool ht)
        {
            InitializeComponent();
            hienthiGF = ht;
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30003") {
                pan30003.Visible = true;
                panChung.Visible = false;
                //xrPageBreak1.Visible = true;
                //xrTable5.Visible = true;
                //if (this.Nam.Value != null && this.Nam.ToString().Length > 0)
                //{
                //    txtNam.Text = "X";
                //    txtNu.Text = "";
                //}
                //else {
                //    txtNam.Text = "";
                //    txtNu.Text = "X";
                
                //}

            }
            if (DungChung.Bien.MaBV == "02005")
            {
                xrLabel4.Visible = false;
                colTKXN.Visible = false;
            }
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = hienthiGF;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04012")
                xrTableCell4.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (MaCBDT.Value!=null)
            {
                colBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (Macb.Value!=null)
            {
                colTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
           
            if (DungChung.Bien.MaBV == "30004")
            {
                colBSDT.Visible = false;
                colTKXN.Visible = false;
            }
        }
        public void BindingData()
        {
            colYC.DataBindings.Add("Text", DataSource, "YC");
            colKQ.DataBindings.Add("Text", DataSource, "KQ");
            colYC1.DataBindings.Add("Text", DataSource, "YC");
            colKQ1.DataBindings.Add("Text", DataSource, "KQ");
            if (DungChung.Bien.MaBV=="04018")
            colDV.DataBindings.Add("Text", DataSource, "DonVi");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            GroupHeader1.GroupFields.Add(new GroupField("TenDV"));

        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                xrTableCell4.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
        }

    }
}
