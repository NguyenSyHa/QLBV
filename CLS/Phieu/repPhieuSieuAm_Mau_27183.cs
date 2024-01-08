
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm_Mau_27183 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm_Mau_27183()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];           
           
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
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
           
            this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
            if (paramDuongDan2.Value != null && !String.IsNullOrEmpty(paramDuongDan2.Value.ToString()))
            {
                this.xrPictureBox2.ImageUrl = paramDuongDan2.Value.ToString();
            }
            
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
           
        }
    }
}
