
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm_Mau_1Anh_27001 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm_Mau_1Anh_27001()
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
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
         
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }

            if(DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                colTenCQCQ.Visible = false;
                colTenCQ.Visible = false;
                xrLabel4.Visible = false;
            }
            if(DungChung.Bien.MaBV=="27001")
            {
                lab16.Visible = false;
                lab17.Visible = false;
                colTenBSDT.Visible = false;
            }    

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
           

        }
        public void hienthiKetQua(string str)
        {
            if(!string.IsNullOrEmpty(str))
            colKetQua.Html = str;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009") {
                lab19.Visible = false;
                xrPictureBox1.Visible = false;
                //xrPictureBox2.Visible = false;
            }
            this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
            //if (paramDuongDan2.Value != null && !String.IsNullOrEmpty(paramDuongDan2.Value.ToString()))
            //{
            //    this.xrPictureBox2.ImageUrl = paramDuongDan2.Value.ToString();
            //}
            //else
            //{
            //    this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //}
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "27183")
            {
                lab18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel2.Text = "Mô tả: ";
                xrLabel3.Font = new System.Drawing.Font("Time New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }
    }
}
