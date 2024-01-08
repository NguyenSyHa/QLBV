using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuNguonDV_XHH : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuNguonDV_XHH()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tencqcq.Text = DungChung.Bien.TenCQCQ;
            tencq.Text = DungChung.Bien.TenCQ;
            TPKT.Text = DungChung.Bien.KeToanTruong;
            TTDV.Text = DungChung.Bien.GiamDoc;
            LapBang.Text = DungChung.Bien.NguoiLapBieu;
        }
        public void BindingData()
        {
            TenKP.DataBindings.Add("Text", DataSource, "TenKP");
            X_QuoangKTS.DataBindings.Add("Text", DataSource, "X_QuangKTS").FormatString = "{0:###,###.###}";
            X_QuoangCiti.DataBindings.Add("Text", DataSource, "X_QuangCiti").FormatString = "{0:###,###.###}";
            DoLoangXuong.DataBindings.Add("Text", DataSource, "DoLoangXuong").FormatString = "{0:###,###.###}";
            Tong.DataBindings.Add("Text", DataSource, "Tong").FormatString = "{0:###,###.###}";
            X_QuoangKTSTong.DataBindings.Add("Text", DataSource, "X_QuangKTS");
            X_QuoangCitiTong.DataBindings.Add("Text", DataSource, "X_QuangCiti");
            DoLoangXuongTong.DataBindings.Add("Text", DataSource, "DoLoangXuong");
            Tong_Tong.DataBindings.Add("Text", DataSource, "Tong").FormatString = "{0:###,###.###}";
        }
    }
}
