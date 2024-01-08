using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChiDinh_SL_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChiDinh_SL_A4()
        {
            InitializeComponent();
        }
        bool HienThiGH = false;
        public repPhieuChiDinh_SL_A4(bool hienthi)
        {
            InitializeComponent();
            HienThiGH = hienthi;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
            TN.DataBindings.Add("Text", DataSource, "TenTN");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
  
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (MaCB.Value!=null && DungChung.Bien.MaBV!="14017")
            {
                colTenBSDTs.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
            }
        }

        private void ReportFooter_BeforePrint_1(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (MaCB.Value != null)
            {
                colTenBSDTs.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDTs.Visible = false;
            if (DungChung.Bien.MaBV == "14018")
            {
                SubBand1.Visible = true;
            }
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV.Substring(0, 2) == "12")
                colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper() + "\n" + DungChung.Bien.TenCQ.ToUpper();
            else
                colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = HienThiGH;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                lab2.Visible = false;

            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                xrLabel12.Visible = true;
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                xrTable5.Visible = true;
                xrTableCell6.Text = "THÀNH TIỀN";
                this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
          | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //this.colTenNhomDV.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //this.colYCKT.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            }
        }

    }
}
