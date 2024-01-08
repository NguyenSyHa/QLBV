using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_BNThanhToanKCB_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_BNThanhToanKCB_30009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celSoTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celBS.DataBindings.Add("Text", DataSource, "TenCB");

            celTongTien.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cellapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celThuQuy.Text = DungChung.Bien.ThuKho;
            celKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}
