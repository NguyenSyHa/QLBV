using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DSBNPTTT_PPVoCam : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DSBNPTTT_PPVoCam()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNam.DataBindings.Add("Text", DataSource, "Nam");
            colNu.DataBindings.Add("Text", DataSource, "Nu");
            colSThe.DataBindings.Add("Text", DataSource, "SThe");
            colMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colPPVoCam.DataBindings.Add("Text", DataSource, "LoiDan");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            colTenKP.DataBindings.Add("Text", DataSource, "tenkp");
            //colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa");
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao");
            colChanDoan.DataBindings.Add("Text", DataSource, "chandoan");
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQCQ.Text = DungChung.Bien.TenCQCQ;
            txtCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

    }
}
