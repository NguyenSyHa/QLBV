using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BbKiemNhap_12345 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BbKiemNhap_12345() 
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuocGh2.DataBindings.Add("Text", DataSource, "TenNhom");
            colTenDuocGh1.DataBindings.Add("Text", DataSource, "TenTN");
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDV");

            colSoCT.DataBindings.Add("Text", DataSource, "MaTam");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGiaCT.DataBindings.Add("Text", DataSource, "DonGiaCT");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colGhiChu.DataBindings.Add("Text", DataSource, "SoLo");

            // colSoLuongGh2.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            // colSoLuongGh1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[1];

            // colThanhTienGh2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString =DungChung.Bien.FormatString[1];
            // colThanhTienGh1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.Summary.FormatString = DungChung.Bien.FormatString[1];
          //  colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
            //GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDuoc"));
        }



        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader2.Visible = false;
            txtTenCQ_.Text = DungChung.Bien.TenCQ.ToUpper();
            diachi.Text = DungChung.Bien.DiaChi;

           // txtTenCQCQ_.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
                  }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BBKiemNhap_Sub repSub = (rep_BBKiemNhap_Sub)xrSubreport1.ReportSource;
            //repSub.TV1.Value = TV1.Value.ToString();
            //repSub.TV2.Value = TV2.Value.ToString();
            //repSub.TV3.Value = TV3.Value.ToString();
            //repSub.TV4.Value = TV4.Value.ToString();
            //repSub.TV5.Value = TV5.Value.ToString();
            //repSub.TV6.Value = TV6.Value.ToString();
            //repSub.TV7.Value = TV7.Value.ToString();
            //repSub.lblCKCT.Value = CD1.Value.ToString().ToUpper();
            //repSub.lblCK1.Value = CD2.Value.ToString().ToUpper();
            //repSub.lblCK2.Value = CD3.Value.ToString().ToUpper();
            //repSub.lblCK3.Value = CD4.Value.ToString().ToUpper();
            //repSub.lblCK4.Value = CD5.Value.ToString().ToUpper();
            //repSub.chutich.Text = TV1.Value.ToString();
           
            if (DungChung.Bien.MaBV == "30007")
            {
                repSub.InCD.Value = "CHỦ TỊCH HỘI ĐỒNG THUỐC";
            }
            else
                repSub.InCD.Value = InCD.Value.ToString();
            

        }

        private void colSoTien_BeforePrint(object sender, CancelEventArgs e)
        {
            if (TongTien.Value != null && TongTien.Value.ToString() != "")
            {
                Double st = Convert.ToDouble(TongTien.Value);
                st = Math.Round(st, 0);
                colSoTien.Text = DungChung.Ham.DocTienBangChu(st, " đồng.");
            }
        }
    }
}
