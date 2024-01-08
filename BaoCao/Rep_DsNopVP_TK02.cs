using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DsNopVP_TK02 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsNopVP_TK02()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            colNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colSeri.DataBindings.Add("Text", DataSource, "Seri");
            colNoiDung.DataBindings.Add("Text", DataSource, "NoiDung");
            colCongKham.DataBindings.Add("Text", DataSource, "CongKham").FormatString=DungChung.Bien.FormatString[1];
            colCongKhamT.DataBindings.Add("Text", DataSource, "CongKham").FormatString = DungChung.Bien.FormatString[1];
            colXetNghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetNghiemT.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAT.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colTongSoTien.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            colTongSoTienT.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToan.Text = DungChung.Bien.KeToanVP;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            double rs;
            if(TongTien.Value != null && Double.TryParse(TongTien.Value.ToString(), out rs))
            {
                Double st = Convert.ToDouble(TongTien.Value);
                st = Math.Round(st, 0);
                txtsotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng!");
            }
        }

    }
}
