using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BaoCaoBenhNhanBHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BaoCaoBenhNhanBHYT()
        {
            InitializeComponent();
        }

        public void binhding()
        {
            Col1.DataBindings.Add("Text", DataSource, "Ngay_Thong_Ke");
            Col2.DataBindings.Add("Text", DataSource, "Count_BN1");
            Col3.DataBindings.Add("Text", DataSource, "Count_BN2");
            Col4.DataBindings.Add("Text", DataSource, "Count_NgoaiTru");
            Col5.DataBindings.Add("Text", DataSource, "Count_NoiTru");
            Col6.DataBindings.Add("Text", DataSource, "Count_BN_RaVien");
            Col7.DataBindings.Add("Text", DataSource, "Count_NoiTru_TaiThoiDiemBaoCao");
            Col8.DataBindings.Add("Text", DataSource, "Count_NgoaiTru_DYCK");

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = DungChung.Bien.TenCQ;
            //duc 222222
        }
    }
}
