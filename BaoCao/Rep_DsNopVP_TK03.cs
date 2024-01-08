using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DsNopVP_TK03 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsNopVP_TK03()
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
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng");
        }

    }
}
