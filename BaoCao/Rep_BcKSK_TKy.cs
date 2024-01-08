using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKSK_TKy : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKSK_TKy()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colGKSK.DataBindings.Add("Text", DataSource, "SoTo");
            txtNK.DataBindings.Add("Text", DataSource, "NNhap");
            colHT.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDC.DataBindings.Add("Text", DataSource, "DChi");
            txtGT.DataBindings.Add("Text", DataSource, "GTinh");
            colTuoi.DataBindings.Add("Text",DataSource,"Tuoi");
            colPLKSK.DataBindings.Add("Text", DataSource, "CDNoiGT");
            colKL.DataBindings.Add("Text", DataSource, "ChuyenKhoa");

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colNK_BeforePrint(object sender, CancelEventArgs e)
        {
            string _nk = "";
            if (this.GetCurrentColumnValue("NNhap") != null && this.GetCurrentColumnValue("NNhap").ToString().Length > 10)
            {
                _nk = this.GetCurrentColumnValue("NNhap").ToString();
                colNK.Text = _nk.ToString().Substring(0, 10);
            }
            else { colNK.Text = ""; }
        }

        private void colGT_BeforePrint(object sender, CancelEventArgs e)
        {
            int _gt = 0;
            if (this.GetCurrentColumnValue("GTinh") != null)
            {
                _gt = Convert.ToInt32(this.GetCurrentColumnValue("GTinh"));
                if (_gt == 0)
                {
                    colGT.Text = "Nữ";
                }
                if (_gt == 1)
                {
                    colGT.Text = "Nam";
                }
            }
        }
    }
}
