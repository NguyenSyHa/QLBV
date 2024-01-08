using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_10_11ct : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_10_11ct()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colNhom.DataBindings.Add("Text", DataSource, "Nhom");
            txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            colSLuot.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSThe.DataBindings.Add("Text", DataSource, "SThe");
            colMaICD.DataBindings.Add("Text", DataSource, "MaICD");
            colTenCSBD.DataBindings.Add("Text", DataSource, "TenCSBD");
            colMaCSBD.DataBindings.Add("Text", DataSource, "MaCS");
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString="{0:dd/MM/yyyy}";
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";
            colNgoaiTru.DataBindings.Add("Text", DataSource, "NgoaiTru").FormatString = DungChung.Bien.FormatString[1];
            colNgoaiTruTN.DataBindings.Add("Text", DataSource, "NgoaiTru").FormatString = DungChung.Bien.FormatString[1];
            colNgoaiTruTC.DataBindings.Add("Text", DataSource, "NgoaiTru").FormatString = DungChung.Bien.FormatString[1];
            colNoiTru.DataBindings.Add("Text", DataSource, "NoiTru").FormatString = DungChung.Bien.FormatString[1];
            colNoiTruTN.DataBindings.Add("Text", DataSource, "NoiTru").FormatString = DungChung.Bien.FormatString[1];
            colNoiTruTC.DataBindings.Add("Text", DataSource, "NoiTru").FormatString = DungChung.Bien.FormatString[1];
             GroupHeader1.GroupFields.Add(new GroupField("Nhom"));
  
        }

        private void colMaICD_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext= new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int mabn=0;
            if (GetCurrentColumnValue("MaBNhan") != null)
                mabn = Convert.ToInt32( GetCurrentColumnValue("MaBNhan"));
                  var sql = dataContext.BNKBs.Where(p => p.MaBNhan== (mabn)).OrderByDescending(p=>p.IDKB).Select(p=>p.MaICD).ToList();
                 string maicd="";
            if(sql.Count>0)
                maicd=sql.First();
            colMaICD.Text = maicd;
        }

     


        private void colTongSoLuot_BeforePrint(object sender, CancelEventArgs e)
        {
            //int sl = 0;
            //if (GetCurrentColumnValue("TongSoLuot") != null)
            //    sl = Convert.ToInt32(GetCurrentColumnValue("TongSoLuot"));
            //{
            //    colTongSoLuot.Text = "Tổng số lượt: " + sl;
            //}
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            //colTruongphongGDC.Text = DungChung.Bien.TruongKhoaLS;
            //colTruongPhongHCTH.Text = DungChung.Bien.TruongPhongKT;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    }
}
