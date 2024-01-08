using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNVinhSinh_BaoNgoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNVinhSinh_BaoNgoc()
        {
            InitializeComponent();
            
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //List<string> _lTen = new List<string>();
        //public class BaoCao
        //{
        //    public string MaDVct { get; set; }

        //    public string KetQua { get; set; }

        //    public string TSBT { get; set; }

        //    public string TenDV { get; set; }

        //    public string MaTam { get; set; }

        //    public int MaDV { get; set; }

        //    public string DonVi { get; set; }

        //    public int? STTDV { get; set; }

        //    public int? STTdvct { get; set; }

        //    public string TenDVct { get; set; }

        //    public int STTNhomHT { get; set; }
        //}
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345"&& DungChung.Bien.MaBV != "24297")
            {
                xrLabel1.Visible = false;
                xrLabel2.Visible = false;
            }
            txtDchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            txtsdt.Text = "Số điện thoại: " + DungChung.Bien.SDTCQ;
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (Macb.Value != null)
            {
                // start HIS-1479
                //colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN.Text = (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? "" : DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                // end HIS-1479
            }
            if (DungChung.Bien.MaBV=="12345" || DungChung.Bien.MaBV == "24297")
            {
                xrPictureBox6.Image = DungChung.Ham.GetLogo();
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
                txtBSCD.Visible = true;
        }



        internal void dataBinhding()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            //celMa.DataBindings.Add("Text", DataSource, "MaDVct");
            celTenXN.DataBindings.Add("Text", DataSource, "TenDVct");
            celKQ.DataBindings.Add("Text", DataSource, "KetQua");
            //celTSBT.DataBindings.Add("Text", DataSource, "TSBT");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            GroupHeader1.GroupFields.Add(new GroupField("STTNhomHT"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void lblNgayIn_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

      
    }
}
