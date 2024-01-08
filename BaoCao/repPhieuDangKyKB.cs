using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repPhieuDangKyKB : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuDangKyKB()
        {
            InitializeComponent();
            if(DungChung.Bien.MaBV == "24012")
            {
                xrLabel16.WordWrap = false;
                xrLabel16.WidthF = 70F;
                xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
        }

        private void repPhieuDangKyKB_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tencb = _data.CanBoes.Where(p => p.MaCB== (DungChung.Bien.MaCB)).ToList();
            if (tencb.Count > 0)
            {
                //colCBTNhan.Text = tencb.First().TenCB.ToString();
                colCBTNhan1.Text = tencb.First().TenCB.ToString();
            }
            if (SThe.Value != null && SThe.Value.ToString().Length == 15)
            {
                colSthe.Text = SThe.Value.ToString().Substring(0, 2) + "-" + SThe.Value.ToString().Substring(2, 1) + "-" + SThe.Value.ToString().Substring(3, 2) + "-" + SThe.Value.ToString().Substring(5, 2) + "-" + SThe.Value.ToString().Substring(7, 3) + "-" + SThe.Value.ToString().Substring(10, 5);
                colSthe1.Text = SThe.Value.ToString().Substring(0, 2) + "-" + SThe.Value.ToString().Substring(2, 1) + "-" + SThe.Value.ToString().Substring(3, 2) + "-" + SThe.Value.ToString().Substring(5, 2) + "-" + SThe.Value.ToString().Substring(7, 3) + "-" + SThe.Value.ToString().Substring(10, 5);
            }
            xrPictureBox1.Image = xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30012")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
            {
                SubBand3.Visible = true;
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                SubBand4.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand3.Visible = false;
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                SubBand4.Visible = true;
            }
            else
            {

                SubBand3.Visible = false;
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                SubBand4.Visible = false;
            }
        }
        
    }
}
