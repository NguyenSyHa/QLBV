using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcXetNghiem_YM : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcXetNghiem_YM()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtDTuong.DataBindings.Add("Text",DataSource,"DTuong");
            txtNNgTru.DataBindings.Add("Text",DataSource,"NNgTru");
            colSH.DataBindings.Add("Text", DataSource, "SLSH").FormatString = DungChung.Bien.FormatString[1];
            colHH.DataBindings.Add("Text", DataSource, "SLHH").FormatString = DungChung.Bien.FormatString[1];
            colVS1.DataBindings.Add("Text", DataSource, "SLVS1").FormatString = DungChung.Bien.FormatString[1];
            colVS2.DataBindings.Add("Text", DataSource, "SLVS2").FormatString = DungChung.Bien.FormatString[1];
            colVS.DataBindings.Add("Text", DataSource, "VS").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            colSHT.DataBindings.Add("Text", DataSource, "SLSH").FormatString = DungChung.Bien.FormatString[1];
            colHHT.DataBindings.Add("Text", DataSource, "SLHH").FormatString = DungChung.Bien.FormatString[1];
            colVS1T.DataBindings.Add("Text", DataSource, "SLVS1").FormatString = DungChung.Bien.FormatString[1];
            colVS2T.DataBindings.Add("Text", DataSource, "SLVS2").FormatString = DungChung.Bien.FormatString[1];
            colVST.DataBindings.Add("Text", DataSource, "VS").FormatString = DungChung.Bien.FormatString[1];
            colMauT.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colTongT.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("DTuong"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("DTuong") != null)
            {
                string _dt = this.GetCurrentColumnValue("DTuong").ToString();
                if (_dt == "BHYT") { colDTuong.Text = ("Bảo hiểm").ToUpper(); } //else { colDTuong.Text = " "; }
                if (_dt == "Dịch vụ") { colDTuong.Text = ("thu phí").ToUpper(); } //else { colDTuong.Text = " "; }
                if (_dt == "KSK") { colDTuong.Text = ("Khám sức khỏe").ToUpper(); } //else { colDTuong.Text = " "; }
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NNgTru") != null)
            {
                int _nt =Convert.ToInt32(this.GetCurrentColumnValue("NNgTru"));
                if (_nt == 1) { colNNgTru.Text = ("Nội trú"); }// else { colNNgTru.Text = " "; }
                if (_nt == 0) { colNNgTru.Text = ("Ngoại trú"); } //else { colNNgTru.Text = " "; }
             }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTTDV.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
