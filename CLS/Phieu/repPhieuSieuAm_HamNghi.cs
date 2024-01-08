
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm_HamNghi : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm_HamNghi()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //colKetQua.pr
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtSDT.Text = "ĐT: " + DungChung.Bien.SDTCQ;
            txtDchi.Text = "Đ/c: " + DungChung.Bien.DiaChi;
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }

        public void hienthiKQ(string str) 
        {
            colKetQua.Html = str;
            this.colKetQua.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        //private void xrPictureBox1_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
        //}

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "24009") {
            //    //lab19.Visible = false;
            //    //xrPictureBox1.Visible = false;
            //}
            //if (DungChung.Bien.MaBV == "30012")
            //{

            //    lab21.Visible = false;
            //    lab22.Visible = false;
            //    colTenTKXN.Visible = false;
            //}
        }

        private void repPhieuSieuAm_HamNghi_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
