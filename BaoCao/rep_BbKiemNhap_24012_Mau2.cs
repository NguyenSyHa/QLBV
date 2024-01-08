using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BbKiemNhap_24012_Mau2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BbKiemNhap_24012_Mau2()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoDK.DataBindings.Add("Text", DataSource, "SoDK");
            colSoKS.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.Summary.FormatString = DungChung.Bien.FormatString[1];
        }



        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BBKiemNhap_Sub_24012 repSub = new rep_BBKiemNhap_Sub_24012(2);
            repSub.CB1.Value = TV1.Value.ToString();
            repSub.CB2.Value = TV2.Value.ToString();
            repSub.CB3.Value = TV3.Value.ToString();
            repSub.CB4.Value = TV4.Value.ToString();
            repSub.CB5.Value = TV5.Value.ToString();
            repSub.CB6.Value = TV6.Value.ToString();
            repSub.CB7.Value = TV7.Value.ToString();
            repSub.CVCB1.Value = CVCB1.Value.ToString().ToUpper();
            repSub.CVCB2.Value = CVCB2.Value.ToString().ToUpper();
            repSub.CVCB3.Value = CVCB3.Value.ToString().ToUpper();
            repSub.CVCB4.Value = CVCB4.Value.ToString().ToUpper();
            repSub.CVCB5.Value = CVCB5.Value.ToString().ToUpper();
            repSub.CVCB6.Value = CVCB6.Value.ToString().ToUpper();
            repSub.CVCB7.Value = CVCB7.Value.ToString().ToUpper();
            repSub.lblCKCT.Value = CD1.Value.ToString().ToUpper();
            repSub.lblCK1.Value = CD2.Value.ToString().ToUpper();
            repSub.lblCK2.Value = CD3.Value.ToString().ToUpper();
            repSub.lblCK3.Value = CD4.Value.ToString().ToUpper();
            repSub.lblCK4.Value = CD5.Value.ToString().ToUpper();
            repSub.InCD.Value = InCD.Value.ToString();
            xrSubreport1.ReportSource = repSub;
        }
    }
}
